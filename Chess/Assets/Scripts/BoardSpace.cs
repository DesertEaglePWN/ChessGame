using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enumeration of BoardSpace states
/// (Default = 0, Active = 1, Contested = 2)
/// </summary>
public enum SpaceState {Default, Open, Contested};


public class BoardSpace : MonoBehaviour {
	public MaterialLibrary materialLibrary;
	public GameManager gameManager;

	public char spaceColumn;
	public char spaceRow;
	//public char spaceColor;
	public SpaceState spaceState = SpaceState.Default;



	void Awake(){
		//Initialize Board
		if (gameObject.name.Length == 2) {
				spaceColumn = gameObject.name [0];
				spaceRow = gameObject.name [1];
		} else {
				Debug.Log ("Non-BoardSpace object running BoardSpace.cs");
		}
		//Debug.Log (gameObject.name);
		//Debug.Log (spaceColumn.ToString());
		//Debug.Log (spaceRow.ToString());
	}

	void OnMouseEnter(){
		switch (spaceState) {
			case(SpaceState.Open):
				gameObject.renderer.material = materialLibrary.materialSpaceOpenHover;
				break;
			case(SpaceState.Contested):
				gameObject.renderer.material = materialLibrary.materialSpaceContestedHover;
				break;
		}
		return;
	}

	void OnMouseExit(){
		switch (spaceState) {
			case(SpaceState.Open):
				gameObject.renderer.material = materialLibrary.materialSpaceOpen;
				break;
			case(SpaceState.Contested):
				gameObject.renderer.material = materialLibrary.materialSpaceContested;
				break;
		}
		
		return;
	}

	void OnMouseDown(){
        if (((spaceState == SpaceState.Open) || (spaceState == SpaceState.Contested))){
            gameManager.activePiece.Move(this);
            gameManager.goToState(GameState.Select, gameManager.activePiece.teamColor);
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