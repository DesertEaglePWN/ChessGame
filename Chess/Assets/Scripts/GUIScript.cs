using UnityEngine;
using System.Collections;

public class GUIScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
   public void QuickPlay() 
    {
        Application.LoadLevel("Game");
    }

   public void MainMenu() 
   {
       Application.LoadLevel("Main");
   }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
