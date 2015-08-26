using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]  
public class PlayerInventory : MonoBehaviour 
{
    public List<Item> inventory = new List<Item>();

    //Local Inventory
    private List<GameObject> containers = new List<GameObject>();    //List of all Containers within the Local Bubble.
    private List<GameObject> looseItems = new List<GameObject>();    //List of all Loose Items within the Local Bubble.

    public List<Item> localInventory;   //List of all Loose Objects and the Content of all Containers within the Local Bubble.


    void Start ()
	{
        StarterGearBasic();
    }

    void OnTriggerEnter(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Container":
                containers.Add(other.gameObject);   //Adds the Container to the list of all Containerss within the Local Bubble.
                UpdateLocalInventory();
                UpdateUI();
                break;

            case "Loose Item":
                looseItems.Add(other.gameObject);   //Adds the Item to the list of all Loose Items in the Local Bubble.
                UpdateUI();
                break;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.transform.tag)
        {
            case "Container":
                containers.Remove(other.gameObject);    //Removes the Container for the List of Container within the Local Bubble.
                UpdateLocalInventory();     //Refreshes the Local Inventory
                UpdateUI();
                break;

            case "Loose Item":
                looseItems.Remove(other.gameObject);    //Removes the Item to the list of all Loose Items in the Local Bubble.
                UpdateUI();
                //Add the Item to the Local Inventory. (This is a Gameobject, so needs to be added as Scriptable Object!!!)
                break;
        }
    }

    void UpdateUI()
    {
        if (this.GetComponent<Interact>().localInvOpen)
        {
            this.GetComponent<Interact>().UIToggle(this.GetComponent<Interact>().LocalInvUI, true, false);
            //Remove Item Prefabs
            foreach (Transform child in this.GetComponent<Interact>().LocalInvUI.transform.FindChild("Inventory"))
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in this.GetComponent<Interact>().PlayerInvUI.transform.FindChild("Inventory"))
            {
                Destroy(child.gameObject);
            }
            this.GetComponent<Interact>().ShowLocalUI();
        }
    }

    void UpdateLocalInventory()    //Refreshes the Local Inventory List.
    {
        localInventory = new List<Item>();  //Refresh the List to Remove Items that are no longer within the Local Bubble.

        foreach (GameObject container in containers)    //Foreach Loop that goes through every Container within the Local Bubble 
        {
            for (int i = 0; i < container.GetComponent<Container>().content.Count; i++)  //For Loop that goes through each item in the current Container Contents.
            {
                localInventory.Add(container.GetComponent<Container>().content[i]);     //Adds each item from the current Containers Content to the Local Inventory List.
            }
        }
    }

    void StarterGearBasic()
    {
        //
        //		GUNS
        //
        Gun M1911 = GameObject.Find("World Manager").GetComponent<CreateGun>().M1911(75, 7);
        inventory.Add(M1911);
        //
        //		Food
        //
        Food Beans = GameObject.Find("World Manager").GetComponent<CreateFood>().Beans(100);
        inventory.Add(Beans);
        //
        //		Drink
        //
        Drink Water = GameObject.Find("World Manager").GetComponent<CreateDrink>().Water(100);
        inventory.Add(Water);

        Debug.Log("Basic Starter Gear Added.");
    }

    void StarterGearRifleman ()
	{
		//
		//		GUNS
		//
		Gun AR15 = GameObject.Find ("World Manager").GetComponent<CreateGun>().AR15(57, 53);		//Create the Scritable Object
		inventory.Add (AR15);																//Adds the Scritable Object to the Inventory
		
		Gun M1911 = GameObject.Find ("World Manager").GetComponent<CreateGun>().M1911(75, 7);
		inventory.Add (M1911);
		//
		//		Food
		//
		Food Roll = GameObject.Find ("World Manager").GetComponent<CreateFood>().CrustyRoll(90);
		inventory.Add (Roll);

		Food Bread = GameObject.Find ("World Manager").GetComponent<CreateFood>().Bread(100);
		inventory.Add (Bread);
		
		Food Bacon = GameObject.Find ("World Manager").GetComponent<CreateFood>().BaconSlice(100);
		inventory.Add (Bacon);

		Food Beans = GameObject.Find ("World Manager").GetComponent<CreateFood>().Beans(100);
		inventory.Add (Beans);
		//
		//		Drink
		//
		Drink Water = GameObject.Find ("World Manager").GetComponent<CreateDrink>().Water(100);
		inventory.Add (Water);
		
		Drink Beer = GameObject.Find ("World Manager").GetComponent<CreateDrink>().Beer(100);
		inventory.Add (Beer);

		Debug.Log ("Rifleman Starter Gear Added.");
	}
}
