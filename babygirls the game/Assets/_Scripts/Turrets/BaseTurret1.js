#pragma strict

var isIdle : boolean;
var isMoving : boolean;
var isRaised : boolean;
var target : Transform;
var childSphere : GameObject;

function Start()
{
	childSphere = GameObject.Find("TurretSphere");
}

function Update()
{
	if(target)
	{
		if(!isRaised)
			raiseGun();
	}
}

function OnTriggerEnter(other : Collider)
{
	if(other.gameObject.tag == "Player")
		target = other.gameObject.transform;
}

function OnTriggerExit(other : Collider)
{
	if(other.gameObject.transform == target)
	{
		target = null;
	}
}

function fireGun()
{
}

function raiseGun()
{
	if(isIdle && !isMoving)
	{
		Vector3.Lerp(Vector3(transform.position.x, transform.position.y, transform.position.z), 
					 Vector3(transform.position.x, transform.position.y - 0.8, transform.position.z),
					 5.0);
		isRaised = true;
	}
}