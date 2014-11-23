
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight: ChessPiece{

    private bool islastSpace;
    public bool IsLastSpace { get; set; }

    public override BoardSpace[] GetAvailableSpaces()
    {
        List<BoardSpace> possibleSpaces = new List<BoardSpace>();
        BoardSpace tempSpace;
        Debug.Log(currentSpace);
        //MOVE FRONT LEFT / FRONT RIGHT
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Front, PieceColor);
        Debug.Log(tempSpace);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, PieceColor);
        Debug.Log(tempSpace);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, PieceColor));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, PieceColor));

        //MOVE LEFT FRONT / LEFT BACK
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Left, PieceColor);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, PieceColor);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, PieceColor));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, PieceColor));

        //MOVE RIGHT FRONT / RIGHT BACK
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Right, PieceColor);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, PieceColor);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, PieceColor));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, PieceColor));

        //MOVE BACK LEFT / BACK RIGHT
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Back, PieceColor);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, PieceColor);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, PieceColor));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, PieceColor));
        foreach (BoardSpace space in possibleSpaces)
        {
            Debug.Log(space == null);
        }
        return possibleSpaces.ToArray();
    }
}


