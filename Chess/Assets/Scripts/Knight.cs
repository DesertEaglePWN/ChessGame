
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
        //Debug.Log(currentSpace);
        //MOVE FRONT LEFT / FRONT RIGHT
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Front, PieceColor, IsLastSpace);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, PieceColor, IsLastSpace);
        //Debug.Log(tempSpace);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, PieceColor, IsLastSpace));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, PieceColor, IsLastSpace));

        //MOVE LEFT FRONT / LEFT BACK
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Left, PieceColor, IsLastSpace);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, PieceColor, IsLastSpace);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, PieceColor, IsLastSpace));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, PieceColor, IsLastSpace));

        //MOVE RIGHT FRONT / RIGHT BACK
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Right, PieceColor, IsLastSpace);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, PieceColor, IsLastSpace);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Front, PieceColor, IsLastSpace));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, PieceColor, IsLastSpace));

        //MOVE BACK LEFT / BACK RIGHT
        IsLastSpace = false;
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Back, PieceColor, IsLastSpace);
        tempSpace = GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Back, PieceColor, IsLastSpace);
        IsLastSpace = true;
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Left, PieceColor, IsLastSpace));
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(tempSpace, SpaceDirection.Right, PieceColor, IsLastSpace));
        return possibleSpaces.ToArray();
    }
}


