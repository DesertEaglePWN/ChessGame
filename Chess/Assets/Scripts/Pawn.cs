//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : ChessPiece{

    public override BoardSpace[] GetAvailableSpaces()
    {
        List<BoardSpace> possibleSpaces = new List<BoardSpace>();
        possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(currentSpace, SpaceDirection.Front, PieceColor));
        
        if (!bHasMoved)
        {
            possibleSpaces.Add(GameManager.currentInstance.Board.getAdjacentSpace(possibleSpaces[0], SpaceDirection.Front, PieceColor));
        }
        return possibleSpaces.ToArray();
    }




}


		

