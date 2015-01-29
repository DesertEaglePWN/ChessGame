using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration of the team colors
/// (None = 0, Black = 1, White = 2).
/// </summary>
public enum TeamColor {None, Black, White};

public abstract class ChessPiece : MonoBehaviour
{
    /// <summary>
    /// The piece's team color.
    /// (Defaults to 'None')
    /// </summary>
    public TeamColor PieceColor { get; private set; }

    /// <summary>
    /// True if the piece has been moved this game.
    /// </summary>
    public bool bHasMoved = false;

    /// <summary>
    /// Bool that tells whether or not this piece is putting an opposing king in check
    /// </summary>
    public bool isChecking {get; set;}

    /// <summary>
    /// The current space that the piece resides on.
    /// </summary>
    public BoardSpace currentSpace;

    ///// <summary>
    ///// Holds the available spaces the piece can move to.
    ///// </summary>
    //private BoardSpace[] availableSpaces;

    // Use this for initialization
    void Start()
    {
        InitPieceColor();
        this.transform.position = new Vector3(currentSpace.transform.position.x,this.transform.position.y,currentSpace.transform.position.z); //set position to match the currentSpace
        isChecking = false;
    }


    void OnMouseEnter()
    {
        if (!GameManager.currentInstance.isGamePaused)
        {
            if (PieceColor == GameManager.currentInstance.turnTeamColor) {
               GameManager.currentInstance.enablePieceHalo(this, true);
            }
        }
    }

    void OnMouseExit()
    {
            GameManager.currentInstance.enablePieceHalo(this, false);
    }

    void OnMouseDown()
    {
        if (!GameManager.currentInstance.isGamePaused) { 
            if (GameManager.currentInstance.turnTeamColor == PieceColor)
            {
                if ((GameManager.currentInstance.activePiece == this))
                {
                    GameManager.currentInstance.DeselectPiece(this);
                }
                else 
                {
                    GameManager.currentInstance.SelectPiece(this);
                }
            
            }
        }
    }

    /// <summary>
    /// Finds and returns an array of BoardSpace objects that correspond to all spaces available for the piece to move to.
    /// (Unique to each piece type)
    /// </summary>
    /// <returns>BoarSpace[]</returns>
    public abstract BoardSpace[] GetAvailableSpaces();

    /// <summary>
    /// Checks and initializes piece color. Applies appropriate starting materials.
    /// </summary>
    private void InitPieceColor()
        {
            //On Start, check and update piece color and position
            if ((currentSpace.spaceRow == '1') || (currentSpace.spaceRow == '2'))
            {
                PieceColor = TeamColor.White;
                this.renderer.material = GameManager.currentInstance.materialLibrary.materialWhite;
            }
            else if ((currentSpace.spaceRow == '7') || (currentSpace.spaceRow == '8'))
            {
                PieceColor = TeamColor.Black;
                this.renderer.material = GameManager.currentInstance.materialLibrary.materialBlack;
            }
            else
            {
                PieceColor = TeamColor.None;
            }
            return;
        }

    protected List<BoardSpace> GetAvailableInDirection(SpaceDirection direction)
    {
        List<BoardSpace> availableSpaces = new List<BoardSpace>();

        BoardSpace nextSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, direction, PieceColor, true);

        while (GameManager.currentInstance.Board.checkSpace(nextSpace) != null)
        {
            availableSpaces.Add(nextSpace);
            if (nextSpace.spaceState == SpaceState.Contested)
            {
                break;
            }
            nextSpace = GameManager.currentInstance.Board.getAdjacentSpace(nextSpace, direction, PieceColor, true);
        }

        return availableSpaces;
    }

}
