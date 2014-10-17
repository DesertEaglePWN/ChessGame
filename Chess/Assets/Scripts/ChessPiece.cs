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
    /// The piece's team color.  SET PRIVATE WHEN DONE.
    /// (Defaults to "None")
    /// </summary>
    public TeamColor teamColor = TeamColor.None;

    /// <summary>
    /// The position of the piece.
    /// </summary>
    protected Vector3 Position;

    public bool bHasMoved = false;

    /// <summary>
    /// The current space that the piece resides on. SET PRIVATE WHEN DONE.
    /// </summary>
    public BoardSpace currentSpace;

    private BoardSpace[] availableSpaces;

    // Use this for initialization
    void Start()
    {
        //On Start, check and update piece color and position
        if (teamColor == TeamColor.Black)
        {
            gameObject.renderer.material = materialLibrary.materialBlack;
        }
        else if (teamColor == TeamColor.White)
        {
            gameObject.renderer.material = materialLibrary.materialWhite;
        }
        Position.x = currentSpace.transform.position.x;
        Position.z = currentSpace.transform.position.z;
        Position.y = gameObject.transform.position.y;
        gameObject.transform.position = Position;
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

    public void Move(BoardSpace targetSpace)
    {
        Position.x = targetSpace.transform.position.x;
        Position.z = targetSpace.transform.position.z;
        gameObject.transform.position = Position;
        currentSpace = targetSpace;
        bHasMoved = true;
        gameManager.ChangeTurn();
    }
}
