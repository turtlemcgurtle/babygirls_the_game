using UnityEngine;
using System.Collections;

/// <summary>
/// Attached to player
/// Fires a rocket to crosshair
/// </summary>

public class FireRocket : MonoBehaviour
{
	//Variables Start----------------------------------
	public GameObject rocket; //the projectile
	
	private Transform myTransform;
	private Transform cameraHeadTransform;
	
	private Vector3 launchPosition = new Vector3(); //position rocket will instantiate from
	
	public float fireRate = 0.5f;
	private float nextFire = 0;
	
	private bool iAmRed = false;
	private bool iAmBlue = false;
	//Variables End------------------------------------

	void Start ()
	{
		if(networkView.isMine)
		{
			myTransform = transform;
			
			cameraHeadTransform = myTransform.FindChild("CameraHead");
			
			//Find the SpawnManager and access the SpawnScript to find out which team the player is on.
			GameObject spawnManager = GameObject.Find("SpawnManager");
			SpawnScript spawnScript = spawnManager.GetComponent<SpawnScript>();
			
			if(spawnScript.amIRed == true)
				iAmRed = true;	
			
			if(spawnScript.amIBlue == true)
				iAmBlue = true;	
		}
		else
			enabled = false;
	}
	
	void Update ()
	{
		if(Input.GetButton("Fire") && Time.time > nextFire)
		{	
			nextFire = Time.time + fireRate;
			
			//The launch position of the projectile will be just in front of the CameraHead.
			launchPosition = cameraHeadTransform.TransformPoint(0, 0, 0.2f);	
			
			
			//Create the blaster projectile across the network 
			//at the launchPosition and tilt its angle
			//so that its horizontal using the angle eulerAngles.x + 90.
			//Also make it team specific.
			if(iAmRed == true)
			{
				networkView.RPC("SpawnProjectile", RPCMode.All,launchPosition,
			                Quaternion.Euler(cameraHeadTransform.eulerAngles.x + 90,
			                                                    myTransform.eulerAngles.y, 0),
				                myTransform.name, "red");
			}
			
			
			if(iAmBlue == true)
			{
				networkView.RPC("SpawnProjectile", RPCMode.All,launchPosition,
			                Quaternion.Euler(cameraHeadTransform.eulerAngles.x + 90,
			                                                    myTransform.eulerAngles.y, 0),
				                myTransform.name, "blue");
			}
		}
	}
	
	[RPC]
	void SpawnProjectile (Vector3 position, Quaternion rotation, 
	                      string originatorName, string team)
	{
		//Access the BlasterScript of the newly instantiated blaster
		//projectile and supply the player's name and team.
		GameObject go = Instantiate(rocket, position, rotation) as GameObject;
		
		RocketTemp rScript = go.GetComponent<RocketTemp>();
		rScript.myOriginator = originatorName;
		rScript.team = team;
	}
}
