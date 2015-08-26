using UnityEngine;
using System.Collections;

public class cameraController : MonoBehaviour 
{
	private GameObject cam;

	void Start () 
	{
		cam = transform.FindChild ("Camera").gameObject;
	}
	
	void Update () 
	{
		//Horizontal Rotations
		var hRotation = Input.GetAxis ("Look Horizontal");
		transform.Rotate (0, hRotation, 0);

		//Vertical Rotations
		var vRotation = Input.GetAxis ("Look Vertical");
		cam.transform.Rotate (vRotation, 0, 0);
	}
}