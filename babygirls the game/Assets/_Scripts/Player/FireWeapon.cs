using UnityEngine;
using System.Collections;

/// <summary>
/// Figures out what his equipped weapon is and fires it.
/// </summary>

public class FireWeapon : MonoBehaviour
{
	public string weaponName;
	public float nextFire = 0.0f;
	
	public class Rocket
	{
		public static string name = "Rocket";
		public static int id = 1;
		public static float fireRate = 0.5f;
	}
	
	void Start()
	{
		weaponName = Rocket.name;
	}
	
	void Update()
	{
		if(Input.GetButton("Fire") && Time.time > nextFire)
		{
			nextFire = Time.time + Rocket.fireRate;
		}
	}
}
