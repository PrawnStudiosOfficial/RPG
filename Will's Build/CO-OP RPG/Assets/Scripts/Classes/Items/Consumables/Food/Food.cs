using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Food : Item 
{
	[SerializeField]
	private string _foodType; // Pistol, Rifle etc.
	[SerializeField]
	private int _hungerValue; //How Much ammo is in the gun.
	[SerializeField]
	private int _thirstValue; // the maximum ammo the gun can hold.
	
	
	public string FoodType { get{return _foodType;} set{_foodType = value;} }
	public int HungerValue { get{return _hungerValue;} set{_hungerValue = value;} }
	public int ThirstValue { get{return _thirstValue;} set{_thirstValue = value;} }
}