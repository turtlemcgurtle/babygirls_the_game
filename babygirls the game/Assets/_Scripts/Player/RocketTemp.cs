using UnityEngine;
using System.Collections;

/// <summary>
/// Makes the rocket projectile detonate on impact or after X time.
/// </summary>

public class RocketTemp : MonoBehaviour
{
	
	//Variables Start-------------------------------------------
	public GameObject rocketExplosion;
	
	private Transform myTransform;
	
	private float projectileSpeed = 10;
	private bool expended = false;
	private float range = 1.5f;
	private float expireTime = 5;
	
	private RaycastHit hit;
	
	//hit detection
	public string team;
	public string myOriginator;
	//Variables End-------------------------------------------
	
	void Start()
	{
		myTransform = transform;
		
		StartCoroutine(DestroyMyselfAfterSomeTime()); //basically threading a timer for self destruction
	}
	
	void Update()
	{
		myTransform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
		
		if(Physics.Raycast(myTransform.position,myTransform.up, out hit, range) &&
		   expended == false)
		{
			if(hit.transform.tag == "Map" || hit.transform.tag == "Floor")
			{
				expended = true;
				
				Instantiate(rocketExplosion, hit.point, Quaternion.identity);
				
				myTransform.renderer.enabled = false;
				myTransform.light.enabled = false;
			}
			
			if(hit.transform.tag == "BlueTeamTrigger" || hit.transform.tag == "RedTeamTrigger")
			{
				
				expended = true;
				
				Instantiate(rocketExplosion, hit.point, Quaternion.identity);
				
				myTransform.renderer.enabled = false;
				myTransform.light.enabled = false;
				
				
				//Access the HealthAndDamage script of the enemy player
				//and inform them that they have been attacked and by who.
				if(hit.transform.tag == "BlueTeamTrigger" && team == "red")
				{
					HealthAndDamage HDscript = hit.transform.GetComponent<HealthAndDamage>();
					HDscript.iWasJustAttacked = true;
					HDscript.myAttacker = myOriginator;
					HDscript.hitByRocket = true;
				}
				
				if(hit.transform.tag == "RedTeamTrigger" && team == "blue")
				{
					HealthAndDamage HDscript = hit.transform.GetComponent<HealthAndDamage>();
					HDscript.iWasJustAttacked = true;
					HDscript.myAttacker = myOriginator;
					HDscript.hitByRocket = true;
				}
			}
		}
	}
	
	IEnumerator DestroyMyselfAfterSomeTime()
	{
		//Wait for the timer to count up to the expireTime and then destroy the projectile.
		
		yield return new WaitForSeconds(expireTime);
		
		Destroy(myTransform.gameObject);
	}
}
