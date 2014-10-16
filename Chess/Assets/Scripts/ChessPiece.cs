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
        board = gameManager.Board;
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
        if ((gameManager.gameState == GameState.Select) && (this.teamColor == gameManager.turnTeamColor))   //Check if piece can be selected
        {
            (gameObject.GetComponent("Halo") as Behaviour).enabled = true;  //Enable mouseover Halo effect
        }
    }

    void OnMouseExit()
    {
        (gameObject.GetComponent("Halo") as Behaviour).enabled = false;     //Disable mouseover Halo effect
    }

    void OnMouseDown()
    {
        if ((gameManager.gameState == GameState.Select) && (this.teamColor == gameManager.turnTeamColor))   //Check if piece can be selected
        {
            SelectPiece();
            BoardSpace[] availableSpaces = GetAvailableSpaces();    //Get all available spaces     
            DisplaySpaces(availableSpaces);                         //Display returned spaces
            gameManager.gameState = GameState.Action;               //Change the game state
        }
        else if (                                                   // Is this piece currently selected?
                 (gameManager.gameState == GameState.Action)
                 && (this.teamColor == gameManager.turnTeamColor) 
                 && (this == gameManager.activePiece)
                ) 
        {
            DeselectPiece(); 
        }
    }

    public void SelectPiece()
    {
        availableSpaces = GetAvailableSpaces();
        DisplaySpaces(availableSpaces);
        gameManager.activePiece = this;
        gameManager.gameState = GameState.Action;
    }

    public void DeselectPiece()
    {
        availableSpaces = GetAvailableSpaces();
        HideSpaces(availableSpaces);
        gameManager.gameState = GameState.Select;
        HideSpaces(availableSpaces);
    }

    public abstract BoardSpace[] GetAvailableSpaces();

    public void DisplaySpaces(BoardSpace[] spacesToDisplay){
        foreach (BoardSpace space in spacesToDisplay)
        {
          if (space != null){
            space.spaceState = SpaceState.Open;
            Renderer meshRenderer = space.GetComponent<Renderer>();
            meshRenderer.enabled = true;
            gameManager.gameState = GameState.Action;
          }
        }

    }

    public void HideSpaces(BoardSpace[] spacesToHide){
        foreach (BoardSpace space in spacesToHide)
        {
            if (space != null)
            {
                space.spaceState = SpaceState.Default;
                Renderer meshRenderer = space.GetComponent<Renderer>();
                meshRenderer.enabled = false;
                gameManager.gameState = GameState.Select;
            }
        }
    }

    public void Move(BoardSpace targetSpace)
    {
        Position.x = targetSpace.transform.position.x;
        Position.z = targetSpace.transform.position.z;
        gameObject.transform.position = Position;
        currentSpace = targetSpace;
        bHasMoved = true;
        gameManager.changeTurn();
    }
}
