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

	public BoardSpace[] getCurrentRow(BoardSpace currentSpace){
        int[] indexArray = getIndex(currentSpace);
        int rowIndex = indexArray[1];
        List<BoardSpace> currentRow = new List<BoardSpace>();
        for(int i = 0; i <= 7; i++)
        currentRow.Add(spaces[i,rowIndex]);
		return currentRow.ToArray();
	}

	public BoardSpace[] getCurrentColumn(BoardSpace currentSpace){
        int[] indexArray = getIndex(currentSpace);
        int columnIndex = indexArray[0];
        List<BoardSpace> currentColumn = new List<BoardSpace>();
        for(int i =0; i <= 7; i++)
        currentColumn.Add(spaces[columnIndex,i]);
		return currentColumn.ToArray();
	}

	public BoardSpace[] getDiagonals(BoardSpace currentSpace){
         List<BoardSpace> Diagonals = new List<BoardSpace>();

         int[] indexArray = getIndex(currentSpace);
         int[] currentIndex = getIndex(currentSpace);
         indexArray.CopyTo(currentIndex,0);
         while ((currentIndex[0] >= 0) && (currentIndex[1] >= 0) && (currentIndex[0] <= 7) && (currentIndex[1] <= 7))
         {
             Diagonals.Add(spaces[currentIndex[0], currentIndex[1]]);
             currentIndex[0]++;
             currentIndex[1]++;
         }

         indexArray.CopyTo(currentIndex, 0);
         while ((currentIndex[0] >= 0) && (currentIndex[1] >= 0) && (currentIndex[0] <= 7) && (currentIndex[1] <= 7))
         {
             Diagonals.Add(spaces[currentIndex[0], currentIndex[1]]);
             currentIndex[0]++;
             currentIndex[1]--;         
         }

         indexArray.CopyTo(currentIndex, 0);
         while ((currentIndex[0] >= 0) && (currentIndex[1] >= 0) && (currentIndex[0] <= 7) && (currentIndex[1] <= 7))
         {
             Diagonals.Add(spaces[currentIndex[0], currentIndex[1]]);
             currentIndex[0]--;
             currentIndex[1]++;
         }

         indexArray.CopyTo(currentIndex, 0);
         while ((currentIndex[0] >= 0) && (currentIndex[1] >= 0) && (currentIndex[0] <= 7) && (currentIndex[1] <= 7))
         {
             Diagonals.Add(spaces[currentIndex[0], currentIndex[1]]);
             currentIndex[0]--;
             currentIndex[1]--;
         }

		return Diagonals.ToArray();
	}

    public BoardSpace[] getKnightLs(BoardSpace currentSpace){
        List<BoardSpace> KnightL = new List<BoardSpace>();
        int[] indexArray = getIndex(currentSpace);
        int[] currentIndex = getIndex(currentSpace);
       
        for (int i = 0; i < 4; i++) { 
            switch (i){
                case 0:     //columnIndex + 2
                    for (int j = 0; j < 4;j++){
                    
                    }
                    break;
                case 1:     //columnIndex - 2
                    break;
                case 2:     //rowIndex - 2
                    break;
                case 3:     //rowIndex - 2
                    break;
                default:
                    break;
            }
            KnightL.Add(verifySpace(currentIndex));
        }
        return KnightL.ToArray();
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


   

	public BoardSpace getAdjacentSpace(BoardSpace currentSpace, SpaceDirection direction, TeamColor teamColor){
	    int[] indexArray = getIndex (currentSpace);
		switch (direction) {				//Determine newSpaceRow and newSpaceColumn by checking arguments
			case (SpaceDirection.FrontLeft):
				if (teamColor == TeamColor.Black){
					indexArray[0] += 1; //Column
					indexArray[1] -= 1; //Row
				}
				else if (teamColor == TeamColor.White){
					indexArray[0] -= 1; //Column
					indexArray[1] += 1; //Row
				}
				break;	

			case (SpaceDirection.Front):
				if (teamColor == TeamColor.Black){
					indexArray[1] -= 1; //Row
				}
				else if (teamColor == TeamColor.White){
					indexArray[1] += 1; //Row
				}
				break;	

			case (SpaceDirection.FrontRight):
				if (teamColor == TeamColor.Black){
					indexArray[0] -= 1; //Column
					indexArray[1] -= 1; //Row
				}
				else if (teamColor == TeamColor.White){
					indexArray[0] += 1; //Column
					indexArray[1] += 1; //Row
				}
				break;	

			case (SpaceDirection.Left):
				if (teamColor == TeamColor.Black){
					indexArray[0] += 1; //Column
				}
				else if (teamColor == TeamColor.White){
					indexArray[0] -= 1; //Column
				}
				break;	

			case (SpaceDirection.Right):
				if (teamColor == TeamColor.Black){
					indexArray[0] -= 1; //Column
				}
				else if (teamColor == TeamColor.White){
					indexArray[0] += 1; //Column
				}
				break;	

			case (SpaceDirection.BackLeft):
				if (teamColor == TeamColor.Black){
					indexArray[0] += 1; //Column
					indexArray[1] += 1; //Row
				}
				else if (teamColor == TeamColor.White){
					indexArray[0] -= 1; //Column
					indexArray[1] -= 1; //Row
				}
				break;	

			case (SpaceDirection.Back):
                if (teamColor == TeamColor.Black){
					indexArray[1] += 1; //Row
				}
				else if (teamColor == TeamColor.White){
					indexArray[1] -= 1; //Row
				}
				break;	

			case (SpaceDirection.BackRight):
				if (teamColor == TeamColor.Black){
					indexArray[0] -= 1; //Column
					indexArray[1] += 1; //Row
				}
				else if (teamColor == TeamColor.White){
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