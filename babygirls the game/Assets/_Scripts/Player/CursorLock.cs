using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and it controls
/// whether the cursor is locked or unlocked.
/// 
/// This script accesses the Multiplayer script.
/// </summary>


public class CursorLock : MonoBehaviour {
	
	//Variables Start--------------------------------
	
	private GameObject multiplayerManager;
	
	private MultiplayerScript multiScript;
	
	//Variables End----------------------------------
	
	void Start() 
	{
		if(networkView.isMine == true)
		{
			multiplayerManager = GameObject.Find("MultiplayerManager");	
			
			multiScript = multiplayerManager.GetComponent<MultiplayerScript>();
		}
		
		else
			enabled = false;	
	}
	
	void Update() 
	{
		if(multiScript.showDisconnectWindow == false)
			Screen.lockCursor = true;	
		
		if(multiScript.showDisconnectWindow == true)
			Screen.lockCursor = false;	
	}
}
