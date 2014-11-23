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
    /// (Defaults to "None")
    /// </summary>
    private TeamColor pieceColor = TeamColor.None;

    public TeamColor PieceColor { get; private set; }

    public bool bHasMoved = false;

    /// <summary>
    /// The current space that the piece resides on.
    /// </summary>
    public BoardSpace currentSpace;

    private BoardSpace[] availableSpaces;

    // Use this for initialization
    void Start()
    {
        InitPieceColor();
        if (PieceColor == TeamColor.Black)
        {
            this.renderer.material = GameManager.currentInstance.materialLibrary.materialBlack;
        }
        else if (PieceColor == TeamColor.White)
        {
            this.renderer.material = GameManager.currentInstance.materialLibrary.materialWhite;
        }
        this.transform.position = new Vector3(currentSpace.transform.position.x,this.transform.position.y,currentSpace.transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnMouseEnter()
    {
        GameManager.currentInstance.PieceHover(this);
    }

    void OnMouseExit()
    {
        GameManager.currentInstance.PieceHover(this);
    }

    void OnMouseDown()
    {
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

    public abstract BoardSpace[] GetAvailableSpaces();

    private void InitPieceColor()
        {
            //On Start, check and update piece color and position
            if ((currentSpace.spaceRow == '1') || (currentSpace.spaceRow == '2'))
            {
                PieceColor = TeamColor.White;
            }
            else if ((currentSpace.spaceRow == '7') || (currentSpace.spaceRow == '8'))
            {
                PieceColor = TeamColor.Black;
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

        BoardSpace nextSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, direction, PieceColor);

        while (nextSpace != null && GameManager.currentInstance.Board.isSpaceAvailable(nextSpace, PieceColor))
        {
            availableSpaces.Add(nextSpace);
            if (nextSpace.spaceState == SpaceState.Contested)
            {
                break;
            }
            nextSpace = GameManager.currentInstance.Board.getAdjacentSpace(nextSpace, direction, PieceColor);
        }

        return availableSpaces;
    }

}
