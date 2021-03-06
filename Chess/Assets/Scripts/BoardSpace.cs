using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enumeration of BoardSpace states
/// (Default = 0, Active = 1, Contested = 2)
/// </summary>
public enum SpaceState {Default, Open, Blocked, Contested};


public class BoardSpace : MonoBehaviour {
	public char spaceColumn;
	public char spaceRow;
	//public char spaceColor;
	public SpaceState spaceState = SpaceState.Default;

   [SerializeField]
   private ChessPiece occupyingPiece;
   public ChessPiece OccupyingPiece { get { return occupyingPiece; } set { occupyingPiece = value; } }

    public bool bOccupied = false;

	void Awake(){
		//Initialize Board
		if (gameObject.name.Length == 2) {
				spaceColumn = gameObject.name [0];
				spaceRow = gameObject.name [1];
		} else {
				Debug.Log ("Non-BoardSpace object running BoardSpace.cs");
		}
	}

	void OnMouseEnter(){
		switch (spaceState) {
			case(SpaceState.Open):
				this.renderer.material = GameManager.currentInstance.materialLibrary.materialSpaceOpenHover;
				break;
			case(SpaceState.Contested):
                this.renderer.material = GameManager.currentInstance.materialLibrary.materialSpaceContestedHover;
				break;
		}
		return;
	}

	void OnMouseExit(){
		switch (spaceState) {
			case(SpaceState.Open):
                this.renderer.material = GameManager.currentInstance.materialLibrary.materialSpaceOpen;
				break;
			case(SpaceState.Contested):
                this.renderer.material = GameManager.currentInstance.materialLibrary.materialSpaceContested;
				break;
		}
		
		return;
	}

	void OnMouseDown(){
        if (spaceState == SpaceState.Open)
        {
            GameManager.currentInstance.MovePiece(this);
            GameManager.currentInstance.AdvanceGameState();
        }
        else if (spaceState == SpaceState.Contested)
        {
            if (this.OccupyingPiece != null) {  
                GameManager.currentInstance.RemovePiece(this.OccupyingPiece);   //default capture case
            }
            else if (Pawn.EnPassantPossible) {
                 BoardSpace enPassantSpace = GameManager.currentInstance.Board.getAdjacentSpace(this, SpaceDirection.Back, GameManager.currentInstance.activePiece.PieceColor,false);
                 GameManager.currentInstance.RemovePiece(enPassantSpace.OccupyingPiece);
            }
            GameManager.currentInstance.MovePiece(this);
            OccupyingPiece = GameManager.currentInstance.activePiece;
            GameManager.currentInstance.AdvanceGameState();
        }
     
	}

//Use to test for overlaping colliders
//	void Update(){
//		RaycastHit hitInfo;
//		if( Physics.Raycast( Camera.main.ScreenPointToRay( Input.mousePosition ), out hitInfo ) )
//		{
//			Debug.Log("Hit: " + hitInfo.collider.name);
//		} 
//	}
}