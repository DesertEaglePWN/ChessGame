using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enumeration of Board Columns
/// (A = 0, B = 1, C = 2, D = 3, E = 4, F = 5, G = 6, H = 7)
/// </summary>
/// 
public enum Column {A,B,C,D,E,F,G,H};

/// <summary>
/// Enumeration of relative space locations
/// (FrontLeft = 0, Front = 1, FrontRight = 2, Left = 3, Right = 4, BackLeft = 5, Back = 6, BackRight =7)
/// </summary>
public enum SpaceDirection {FrontLeft, Front, FrontRight, Left, Right, BackLeft, Back, BackRight};

public class Board {

	
	public BoardSpace[,] spaces = new BoardSpace[8,8];

	public Board(BoardSpace[] spaceCollection){
		foreach (BoardSpace space in spaceCollection) {
			int[] indexArray = getIndex(space);
			spaces[indexArray[0],indexArray[1]] = space;
			//Debug.Log(spaceColumn);
		}
	}
	
    /// <summary>
	/// Gets a BoardSpace game object by name.
	/// </summary>
	/// <returns>The space as BoardSpace.</returns>
	/// <param name="spaceName">Space name.</param>
	public BoardSpace getSpace(string spaceName){
		int spaceColumn = (int) Enum.Parse( typeof(Column), spaceName[0].ToString());
		int spaceRow = int.Parse(spaceName[1].ToString());

		return spaces [spaceColumn, spaceRow];
	}

	public int[] getIndex(BoardSpace space){
		return new int[]
        {
			(int) Enum.Parse (typeof(Column), space.spaceColumn.ToString ()),
			(int) (int.Parse (space.spaceRow.ToString ()) - 1)
		};
	}

    private BoardSpace verifySpace(int[] indexArray){
            if ((indexArray[0] < 0) || (indexArray[1] < 0) || (indexArray[0] > 7) || (indexArray[1] > 7)) {
                return null;
            }
            else
            {
                return spaces[indexArray[0], indexArray[1]];
            }
        }

    /// <summary>
    /// Returns true if the adjacent space in the specified direction of the current space for the specified team color.
    /// </summary>
    /// <param name="currentSpace"></param>
    /// <param name="direction"></param>
    /// <param name="PieceColor"></param>
    /// <returns></returns>
    public bool isSpaceAvailable(BoardSpace spaceToCheck, TeamColor pieceColor)
    {
        int[] indexArray = getIndex(spaceToCheck);
        if (spaceToCheck.OccupyingPiece != null)
        {
            if (spaceToCheck.OccupyingPiece.PieceColor == pieceColor)
            {
                return false;
            }
            else
            {
                spaceToCheck.spaceState = SpaceState.Contested;
                return true;
            }
        }
        return true;
    }

	public BoardSpace getAdjacentSpace(BoardSpace currentSpace, SpaceDirection direction, TeamColor PieceColor){
        Debug.Log(currentSpace == null);
	    int[] indexArray = getIndex (currentSpace);
		switch (direction) {				//Determine newSpaceRow and newSpaceColumn by checking arguments
			case (SpaceDirection.FrontLeft):
				if (PieceColor == TeamColor.Black){
					indexArray[0] += 1; //Column
					indexArray[1] -= 1; //Row
				}
				else if (PieceColor == TeamColor.White){
					indexArray[0] -= 1; //Column
					indexArray[1] += 1; //Row
				}
				break;	

			case (SpaceDirection.Front):
				if (PieceColor == TeamColor.Black){
					indexArray[1] -= 1; //Row
				}
				else if (PieceColor == TeamColor.White){
					indexArray[1] += 1; //Row
				}
				break;	

			case (SpaceDirection.FrontRight):
				if (PieceColor == TeamColor.Black){
					indexArray[0] -= 1; //Column
					indexArray[1] -= 1; //Row
				}
				else if (PieceColor == TeamColor.White){
					indexArray[0] += 1; //Column
					indexArray[1] += 1; //Row
				}
				break;	

			case (SpaceDirection.Left):
				if (PieceColor == TeamColor.Black){
					indexArray[0] += 1; //Column
				}
				else if (PieceColor == TeamColor.White){
					indexArray[0] -= 1; //Column
				}
				break;	

			case (SpaceDirection.Right):
				if (PieceColor == TeamColor.Black){
					indexArray[0] -= 1; //Column
				}
				else if (PieceColor == TeamColor.White){
					indexArray[0] += 1; //Column
				}
				break;	

			case (SpaceDirection.BackLeft):
				if (PieceColor == TeamColor.Black){
					indexArray[0] += 1; //Column
					indexArray[1] += 1; //Row
				}
				else if (PieceColor == TeamColor.White){
					indexArray[0] -= 1; //Column
					indexArray[1] -= 1; //Row
				}
				break;	

			case (SpaceDirection.Back):
                if (PieceColor == TeamColor.Black){
					indexArray[1] += 1; //Row
				}
				else if (PieceColor == TeamColor.White){
					indexArray[1] -= 1; //Row
				}
				break;	

			case (SpaceDirection.BackRight):
				if (PieceColor == TeamColor.Black){
					indexArray[0] -= 1; //Column
					indexArray[1] += 1; //Row
				}
				else if (PieceColor == TeamColor.White){
					indexArray[0] += 1; //Column
					indexArray[1] -= 1; //Row
				}
				break;

			default:
				break;
		}
        if ((indexArray[0] < 0) || (indexArray[1] < 0) || (indexArray[0] > 7) || (indexArray[1] > 7)) {
               return null;
        }
        BoardSpace newSpace = spaces[indexArray[0], indexArray[1]];
        if ((newSpace.OccupyingPiece != null) && (newSpace.OccupyingPiece.PieceColor == PieceColor))
        {
             newSpace.spaceState = SpaceState.Blocked;
             return null;
        }
        if ((newSpace.OccupyingPiece != null) && (newSpace.OccupyingPiece.PieceColor != PieceColor))
        {
            newSpace.spaceState = SpaceState.Contested;
            return spaces[indexArray[0],indexArray[1]];
        }
            newSpace.spaceState = SpaceState.Open;
            return spaces[indexArray[0],indexArray[1]];
	}

    public void clearAvailableSpaces() {
        foreach (BoardSpace space in spaces) {
            space.renderer.enabled = false;
            space.spaceState = SpaceState.Default;
        }
        return;
    }

}