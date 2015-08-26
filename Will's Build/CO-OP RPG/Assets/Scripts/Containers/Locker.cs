using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Locker : MonoBehaviour 
{
	public List<Item> content = new List<Item>();
	private GameObject inventoryUI;
	public int contentSize = 15;
	
	private GUIStyle boxColor;
	public ScriptableObject selected;
	[HideInInspector]
	public string filter;
	[HideInInspector]
	public Vector2 scrollArea;
	public bool show = false;

	void Start () 
	{
		inventoryUI = GameObject.Find ("Inventory UI");
		SpawnContent ();
	}

	void SpawnContent ()
	{
		Gun M1911 = GameObject.Find ("World Manager").GetComponent<CreateGun>().M1911(75, 7);
		content.Add (M1911);

		Food Bread = GameObject.Find ("World Manager").GetComponent<CreateFood>().Bread(100);
		content.Add (Bread);

		Drink Water = GameObject.Find ("World Manager").GetComponent<CreateDrink>().Water(100);
		content.Add (Water);
	}
	
	void Update () 
	{
		if(Input.GetKeyUp(KeyCode.E)) 
		{
			ToggleInventory();
		}
	}

	void ToggleInventory()
	{
		if (inventoryUI.GetActive () == false) 
		{
			inventoryUI.SetActive (true);
			show = true;
		}
		else
		{
			inventoryUI.SetActive(false); 
			selected = null;
			show = false;
		}
	}

	void OnGUI ()
	{
		if (show == true) 
		{
			boxColor = new GUIStyle(GUI.skin.box);
			
			GUILayout.BeginArea (new Rect (0, 0, Screen.width, Screen.height));
			GUILayout.BeginHorizontal ();
			GUILayout.FlexibleSpace ();
			GUILayout.BeginVertical ();
			GUILayout.FlexibleSpace ();
			//
			GUILayout.Label("Filter: ");
			filter = GUILayout.TextArea(filter, 25, GUILayout.Width(200));
			filter = filter.ToLower();
			
			//scrollArea = GUILayout.BeginScrollView (scrollArea, GUILayout.Width (900), GUILayout.Height (750));
			for (int i = 0; i < content.Count; i++) 
			{
				if(content[i].Name.ToLower().Contains(filter))
				{
					if(selected == content[i])
					{
						boxColor.normal.textColor = Color.green;
						boxColor.normal.background = content[i].Icon;
					}
					else
					{
						boxColor.normal.textColor = Color.white;
						boxColor.normal.background = content[i].Icon;
					}
					
					GUILayout.Box(content[i].name, boxColor, GUILayout.Height(128), GUILayout.Width(128));
					
					if (Event.current.type == EventType.MouseUp && GUILayoutUtility.GetLastRect().Contains(Event.current.mousePosition))
					{
						selected = content[i];
					}
				}
			}
			
			//GUILayout.EndScrollView();
			//
			GUILayout.FlexibleSpace ();
			GUILayout.EndVertical ();
			GUILayout.FlexibleSpace ();
			GUILayout.EndHorizontal ();
			GUILayout.EndArea ();
		}
	}
}
