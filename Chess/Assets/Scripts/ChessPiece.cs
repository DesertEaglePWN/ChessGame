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
    /// The Game Manager.
    /// </summary>
    public GameManager gameManager;

    /// <summary>
    /// The Material Library.
    /// </summary>
    public MaterialLibrary materialLibrary;

    /// <summary>
    /// The piece's team color.
    /// (Defaults to "None")
    /// </summary>
    private TeamColor pieceColor = TeamColor.None;

    public TeamColor PieceColor { get; private set; }


    /// <summary>
    /// The position of the piece.
    /// </summary>
    protected Vector3 Position;

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
            gameObject.renderer.material = materialLibrary.materialBlack;
        }
        else if (PieceColor == TeamColor.White)
        {
            gameObject.renderer.material = materialLibrary.materialWhite;
        }
        Position.x = currentSpace.transform.position.x;
        Position.z = currentSpace.transform.position.z;
        Position.y = this.transform.position.y;
        this.transform.position = Position;
    }

    // Update is called once per frame
    void Update()
    {


    }

    void OnMouseEnter()
    {
        gameManager.PieceHover(this);
    }

    void OnMouseExit()
    {
        gameManager.PieceHover(this);
    }

    void OnMouseDown()
    {
        gameManager.SelectPiece(this);
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
}
