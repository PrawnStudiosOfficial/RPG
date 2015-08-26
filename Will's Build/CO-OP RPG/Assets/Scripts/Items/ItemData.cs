using UnityEngine;
using System.Collections;

public class ItemData : MonoBehaviour 
{

    public Item item;

    void Start () 
	{
        switch (transform.tag)
        {
            case "Food":
                Food _food = GameObject.Find("World Manager").GetComponent<CreateFood>().CrustyRoll(100);
                item = _food;
                break;

            case "Drink":
                Drink _drink = GameObject.Find("World Manager").GetComponent<CreateDrink>().Water(100);
                item = _drink;
                break;

            case "Gun":
                break;
        }
	}
	
	void Update () 
	{
	
	}
}
