using UnityEngine;
using System.Collections;

/// <summary>
/// The plan for this weapon is it will be how the player deploys deployables, be it a Cube/Sphere/ect.
/// 
/// ATM they only need to left click once to place it down, plan on using mouse wheel or mouse2 to cycle through
/// the available deployables.
/// </summary>

public class FireDeployer : MonoBehaviour
{
	#region Variables
	public int currentItem;
	private bool canDeploy;
	
	public float deployRate = 0.5f;
	private float nextFire = 0.0f;
	
	private Vector3 deployPosition;
	
	public GameObject cube;
	public GameObject cylinder;
	public GameObject sphere;
	
	enum item
	{
		Cube = 1, //so the enum starts at 1 instead of zurro
		Cylinder,
		Sphere
	}
	#endregion
	
	
	void Start()
	{
		currentItem = (int)item.Cube;
	}
	
	void Update()
	{
		if(Input.GetButton("Fire"))
		{	
			if(Time.time > nextFire && !canDeploy) //if time has passed and we're not in deploy mode
			{
				nextFire = Time.time + deployRate;
				canDeploy = true;
			}
			
			if(canDeploy) //so they want to deploy the object at this position
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast (ray, out hit, 30.0f))
				{
					deployPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
					
					switch(currentItem)
					{
						case 1:
							Instantiate(cube, hit.point, Quaternion.identity);
							break;
					}
					canDeploy = false;
				}
			}
		}
	}
}
