using UnityEngine;
using System.Collections;

public class CreateDrink : MonoBehaviour 
{
	public Drink Water (int _condition)
	{
		Drink drink = ScriptableObject.CreateInstance ("Drink") as Drink;
		drink.Name = "Water";
		drink.name = drink.Name;
		drink.Weight = 0.5f;
		drink.Condition = _condition;
		drink.Value = 10;
		drink.Icon = Resources.Load("Icons/Drink Icon") as Texture2D;
		drink.DrinkType = "Water";
		drink.ThirstValue = 100;
		return drink;
	}

	public Drink Beer (int _condition)
	{
		Drink drink = ScriptableObject.CreateInstance ("Drink") as Drink;
		drink.Name = "Beer";
		drink.name = drink.Name;
		drink.Weight = 0.5f;
		drink.Condition = _condition;
		drink.Value = 10;
		drink.Icon = Resources.Load("Icons/Drink Icon") as Texture2D;
		drink.DrinkType = "Alcohol";
		drink.ThirstValue = 10;
		return drink;
	}
}
