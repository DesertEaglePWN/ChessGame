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

public class Knight: ChessPiece{

    public override BoardSpace[] GetAvailableSpaces()
    {
        //Debug.Log (activeSpace.getSpace(SpaceDirection.Front,teamColor))
        return board.getKnightLs(currentSpace);
    }

}

