using UnityEngine;
using System.Collections;

public class BlackMaster : MonoBehaviour {

	public bool goesFirst = true;
	
	///<summary>Array for tracking the number of captured pieces, by type.
	/// PieceCaptured: Index# 
	/// Pawn : [0];
	/// Rook: [1];
	/// Knight: [2];
	/// Bishop: [3];
	/// Queen: [4]; 
	/// </summary>		
	public int[] blackPiecesCaptured = {0,0,0,0,0};

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
