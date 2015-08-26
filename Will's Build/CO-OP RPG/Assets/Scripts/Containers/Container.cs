using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Container : MonoBehaviour 
{
    public int slots = 15;
    public List<Item> content;

    void Start ()
    {
        content = new List<Item>();
        SpawnContent ();
    }

    void SpawnContent()
    {
        int i = Random.Range(0, 10);
        if (i > 8)
        {
            Gun AR15 = GameObject.Find("World Manager").GetComponent<CreateGun>().AR15(75, 30);
            content.Add(AR15);
        }

        i = Random.Range(0, 10);
        if (i > 4)
        {
            Food Bread = GameObject.Find("World Manager").GetComponent<CreateFood>().Bread(100);
            content.Add(Bread);
        }
        
        i = Random.Range(0, 10);
        if (i > 2)
        {
            Drink Water = GameObject.Find("World Manager").GetComponent<CreateDrink>().Water(100);
            content.Add(Water);
        }
    }
}
