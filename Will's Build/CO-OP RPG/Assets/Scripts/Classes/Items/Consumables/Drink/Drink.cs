using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Drink : Item 
{
	[SerializeField]
	private string _drinkType; // Pistol, Rifle etc.
	[SerializeField]
	private int _thirstValue; // the maximum ammo the gun can hold.
	
	
	public string DrinkType { get{return _drinkType;} set{_drinkType = value;} }
	public int ThirstValue { get{return _thirstValue;} set{_thirstValue = value;} }
}