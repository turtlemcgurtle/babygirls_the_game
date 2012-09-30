using UnityEngine;
using System.Collections;

public class EditorMode : MonoBehaviour
{
	public bool inEditor = false;
	public float updateInterval = 0.2f;
	private float nextUpdate = 0.0f;
	
	private int winPropertiesXSize = 200;
	private int winPropertiesYSize = 400;
	private int boxOffset = 10;
	private int boxWidth;
	private Rect windowRectProperties;
	
	//temporarily publicsss
	public GameObject selectedObject;
	public string name = "Box";
	public string strColor;
	
	public string strColorR;
	public string strColorG;
	public string strColorB;
	public string strColorA;
	
	public string strPosX;
	public string strPosY;
	public string strPosZ;
	
	public string strSizeX;
	public string strSizeY;
	public string strSizeZ;
	
	public string strRotX;
	public string strRotY;
	public string strRotZ;
	
	public float floaRotX;
	public float floaRotY;
	public float floaRotZ;
	
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
			if(Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out objectHit, 30.0f))
			{
				if(objectHit.rigidbody && !objectHit.collider.tag.Contains("Player"))
					selectedObject = objectHit.rigidbody.gameObject;
			}
		}
		
		if(selectedObject && Time.time > nextUpdate)
		{
			nextUpdate = Time.time + updateInterval;
			
			MeshFilter mf = selectedObject.collider.GetComponent(typeof(MeshFilter)) as MeshFilter;
			if (!mf)
				return;
			Mesh mesh = mf.sharedMesh;
			if (!mesh)
				return;
			Vector3 size = mesh.bounds.size;
			Color[] color = mesh.colors;
			
			name = selectedObject.name;
			strSizeX = size.x.ToString();
			strSizeY = size.y.ToString();
			strSizeZ = size.z.ToString();
			
			strColorR = (selectedObject.collider.renderer.material.color.r * 255).ToString();
			strColorG = (selectedObject.collider.renderer.material.color.g * 255).ToString();
			strColorB = (selectedObject.collider.renderer.material.color.b * 255).ToString();
			strColorA = (selectedObject.collider.renderer.material.color.a * 255).ToString();
			
			strPosX = selectedObject.transform.position.x.ToString();
			strPosY = selectedObject.transform.position.y.ToString();
			strPosZ = selectedObject.transform.position.z.ToString();
			
			floaRotX = selectedObject.transform.eulerAngles.x;
			floaRotY = selectedObject.transform.eulerAngles.y;
			floaRotZ = selectedObject.transform.eulerAngles.z;
			strRotX = floaRotX.ToString ("#0.0000");
			strRotY = floaRotY.ToString ("#0.0000");
			strRotZ = floaRotZ.ToString ("#0.0000");
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
		
		#region Color
		GUI.Label(new Rect(5, 50, 40, 20), "Color:");
		strColorR = GUI.TextField(new Rect(45, //xpos
								50, //ypos
								30, //xsize
								20), //ysize
								strColorR, 25);
		strColorG = GUI.TextField(new Rect(80, //xpos
								50, //ypos
								30, //xsize
								20), //ysize
								strColorG, 25);
		strColorB = GUI.TextField(new Rect(115, //xpos
								50, //ypos
								30, //xsize
								20), //ysize
								strColorB, 25);
		
		
		#endregion
		
		#region Position
		GUI.Box (new Rect(5, 78, winPropertiesXSize - boxOffset, 100), "Position"); //group box for the Position values
		boxWidth = winPropertiesXSize - boxOffset;
		
		
		GUI.Label(new Rect(10, 103, 15, 20), "X:");
		strPosX = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								103, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strPosX, 25);
		
		
		GUI.Label(new Rect(10, 128, 15, 20), "Y:");
		strPosX = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								128, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strPosY, 25);
		
		
		GUI.Label(new Rect(10, 153, 15, 20), "Z:");
		strPosZ = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								153, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strPosZ, 25);
		#endregion
		
		#region Rotation
		GUI.Box (new Rect(5, 183, winPropertiesXSize - boxOffset, 100), "Rotation"); //group box for the Position values
		boxWidth = winPropertiesXSize - boxOffset;
		
		GUI.Label(new Rect(10, 208, 15, 20), "X:");
		strRotX = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								208, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strRotX, 25);
		
		
		GUI.Label(new Rect(10, 233, 15, 20), "Y:");
		strRotY = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								233, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strRotY, 25);
		
		
		GUI.Label(new Rect(10, 258, 15, 20), "Z:");
		strRotZ = GUI.TextField(new Rect((boxWidth / 10) + 10, //xpos
								258, //ypos
								boxWidth - 30, //xsize
								20), //ysize
								strRotZ, 25);
		#endregion
		
		GUI.DragWindow();
	}
}
