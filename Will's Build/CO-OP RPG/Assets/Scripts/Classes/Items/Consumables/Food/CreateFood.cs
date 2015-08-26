using UnityEngine;
using System.Collections;

public class CreateFood : MonoBehaviour 
{

	public Food CrustyRoll (int _condition)
	{
		Food food = ScriptableObject.CreateInstance ("Food") as Food;
		food.Name = "Crusty Roll";
		food.name = food.Name;
		food.Weight = 0.2f;
		food.Condition = _condition;
		food.Value = 12;
		food.Icon = Resources.Load("Icons/Food Icon") as Texture2D;
		food.FoodType = "Bread";
		food.HungerValue = 20;
		food.ThirstValue = 0;
		return food;
	}

	public Food BaconSlice (int _condition)
	{
		Food food = ScriptableObject.CreateInstance ("Food") as Food;
		food.Name = "Bacon Slice";
		food.name = food.Name;
		food.Weight = 0.1f;
		food.Condition = _condition;
		food.Value = 25;
		food.Icon = Resources.Load("Icons/Food Icon") as Texture2D;
		food.FoodType = "Meat";
		food.HungerValue = 35;
		food.ThirstValue = 5;
		return food;
	}

	public Food Bread (int _condition)
	{
		Food food = ScriptableObject.CreateInstance ("Food") as Food;
		food.Name = "Bread";
		food.name = food.Name;
		food.Weight = 0.2f;
		food.Condition = _condition;
		food.Value = 5;
		food.Icon = Resources.Load("Icons/Food Icon") as Texture2D;
		food.FoodType = "Bread";
		food.HungerValue = 25;
		food.ThirstValue = 0;
		return food;
	}

	public Food Beans (int _condition)
	{
		Food food = ScriptableObject.CreateInstance ("Food") as Food;
		food.Name = "Beans";
		food.name = food.Name;
		food.Weight = 1f;
		food.Condition = _condition;
		food.Value = 25;
		food.Icon = Resources.Load("Icons/Food Icon") as Texture2D;
		food.FoodType = "Canned";
		food.HungerValue = 20;
		food.ThirstValue = 15;
		return food;
	}
}
