using UnityEngine;
using UnityEditor;
using System.Collections;

public class EditorMode : MonoBehaviour
{
	public bool inEditor = false;
	private int winPropertiesXSize = 200;
	private int winPropertiesYSize = 155;
	private int boxOffset = 10;
	private int boxWidth;
	private Rect windowRectProperties;
	
	//temporarily public
	public GameObject selectedObject;
	public string name = "Box";
	public string strPosX;
	public string strPosY;
	public string strPosZ;
	public string strSizeX;
	public string strSizeY;
	public string strSizeZ;
	
	void Start()
	{
		windowRectProperties = new Rect(20, 20, winPropertiesXSize, winPropertiesYSize);
	}
	
	void Update()
	{
		if(Input.GetButtonDown("Editor"))
			inEditor = !inEditor;
		if(Input.GetButtonDown ("Fire") && inEditor)
		{
			RaycastHit objectHit;
			if(Physics.Raycast (camera.ScreenPointToRay(Input.mousePosition), out objectHit, 30.0f))
			{
				if(objectHit.rigidbody)
				{
					MeshFilter mf = objectHit.collider.GetComponent(typeof(MeshFilter)) as MeshFilter;
					 if (!mf)
						return;
					Mesh mesh = mf.sharedMesh;
					if (!mesh)
						return;
					Vector3 size = mesh.bounds.size;
					
					selectedObject = objectHit.rigidbody.gameObject;
					name = selectedObject.name;
					strSizeX = size.x.ToString();
					strSizeY = size.y.ToString();
					strSizeZ = size.z.ToString();
					
					strPosX = objectHit.transform.position.x.ToString();
					strPosY = objectHit.transform.position.y.ToString();
					strPosZ = objectHit.transform.position.z.ToString();
				}
			}
		}
	}
	
	void OnGUI()
	{
		if(inEditor)
		{
			windowRectProperties = GUI.Window (0, windowRectProperties, showObjectProperties, "Object Properties"); //creates the property window
		}
	}
	
	void showObjectProperties(int windowID)
	{
		GUI.Label(new Rect(5, 22, 40, 20), "Name:");
		
		name = GUI.TextField(new Rect((winPropertiesXSize / 42) + 42, //xpos
								22, //ypos
								winPropertiesXSize - 55, //xsize
								20), //ysize
								name, 25);
		
		GUI.Box (new Rect(5, 50, winPropertiesXSize - boxOffset, 100), "Position"); //group box for the Position values
		boxWidth = winPropertiesXSize - boxOffset;
		
		
		GUI.Label(new Rect(10, 75, 15, 20), "X:");
		strPosX = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								75, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strPosX, 25);
		
		
		GUI.Label(new Rect(10, 100, 15, 20), "Y:");
		strPosX = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								100, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strPosY, 25);
		
		
		GUI.Label(new Rect(10, 125, 15, 20), "Z:");
		strPosZ = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								125, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strPosZ, 25);
		
		
		GUI.DragWindow();
	}
}
