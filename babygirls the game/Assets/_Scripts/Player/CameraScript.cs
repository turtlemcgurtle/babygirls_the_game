using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to the player and causes the
/// camera to follow his CameraHead.
/// </summary>

public class CameraScript : MonoBehaviour
{
	//Variables Start----------------------------------
	private Camera myCamera;
	private Transform cameraHeadTransform;
	//Variables End------------------------------------
	
	void Start()
	{
		if(networkView.isMine)
		{
			myCamera = Camera.main;
			cameraHeadTransform = transform.FindChild ("CameraHead");
		}
		else
			enabled = false;
	}
	
	void Update() 
	{
		myCamera.transform.position = cameraHeadTransform.position;
		myCamera.transform.rotation = cameraHeadTransform.rotation;
		
	}
}
