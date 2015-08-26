using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item : ScriptableObject
{
	[SerializeField]
	private string _name;

	[SerializeField]
	private float _weight;

	[SerializeField] [Range(0f,100f)]
	private int _condition;

	[SerializeField]
	private double _value;

	[SerializeField]
	private Texture2D _icon = Resources.Load("Icons/Missing Icon") as Texture2D;

	public string Name { get{return _name;} set{_name = value;} }
	public float Weight { get{return _weight;} set{_weight = value;} }
	public int Condition { get{return _condition;} set{_condition = value;} }
	public double Value { get{return _value;} set{_value = value;} }
	public Texture2D Icon {get {return _icon;} set { _icon = value;} }
}