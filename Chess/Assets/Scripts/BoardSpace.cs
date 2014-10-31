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
	public MaterialLibrary materialLibrary;

	public char spaceColumn;
	public char spaceRow;
	//public char spaceColor;
	public SpaceState spaceState = SpaceState.Open;

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
        Debug.Log(spaceState);
		switch (spaceState) {
			case(SpaceState.Open):
				this.renderer.material = materialLibrary.materialSpaceOpenHover;
				break;
			case(SpaceState.Contested):
				this.renderer.material = materialLibrary.materialSpaceContestedHover;
				break;
		}
		return;
	}

	void OnMouseExit(){
		switch (spaceState) {
			case(SpaceState.Open):
				this.renderer.material = materialLibrary.materialSpaceOpen;
				break;
			case(SpaceState.Contested):
				this.renderer.material = materialLibrary.materialSpaceContested;
				break;
		}
		
		return;
	}

	void OnMouseDown(){
        if (spaceState == SpaceState.Open)
        {
            GameManager.currentInstance.MovePiece(this);
            OccupyingPiece = GameManager.currentInstance.activePiece;
            GameManager.currentInstance.AdvanceGameState();
        }
        else if (spaceState == SpaceState.Contested)
        {
            GameManager.currentInstance.MovePiece(this);
            GameManager.currentInstance.RemovePiece(this.OccupyingPiece);
            OccupyingPiece = GameManager.currentInstance.activePiece;
            GameManager.currentInstance.AdvanceGameState();
        }
		return;
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