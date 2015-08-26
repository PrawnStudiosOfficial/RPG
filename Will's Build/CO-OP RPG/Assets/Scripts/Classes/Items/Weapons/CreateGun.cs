using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CreateGun : MonoBehaviour 
{

	public Gun M1911 (int _condition, int _ammo)
	{
		Gun gun = ScriptableObject.CreateInstance ("Gun") as Gun;
		gun.Name = "M1911";
		gun.name = gun.Name;
		gun.Weight = 5;
		gun.Condition = _condition;
		gun.Value = 350;
		gun.Icon = Resources.Load("Icons/Gun Icon") as Texture2D;
		gun.GunType = "Pistol";
		gun.Ammo = _ammo;
		gun.MaxAmmo = 7;
		gun.AmmoTypes = new string[] {".45", ".45 AP", ".45 HP"};
		return gun;
	}

	public Gun Glock17 (int _condition, int _ammo)
	{
		Gun gun = ScriptableObject.CreateInstance ("Gun") as Gun;
		gun.Name = "Glock 17";
		gun.name = gun.Name;
		gun.Weight = 5;
		gun.Condition = _condition;
		gun.Value = 250;
		gun.Icon = Resources.Load("Icons/Gun Icon") as Texture2D;
		gun.GunType = "Pistol";
		gun.Ammo = _ammo;
		gun.MaxAmmo = 17;
		gun.AmmoTypes = new string[] {"9mm", "9mm AP", "9mm HP"};
		return gun;
	}

	public Gun AR15 (int _condition, int _ammo)
	{
		Gun gun = ScriptableObject.CreateInstance ("Gun") as Gun;
		gun.Name = "AR-15";
		gun.name = gun.Name;
		gun.Weight = 15;
		gun.Condition = _condition;
		gun.Value = 1500;
		gun.Icon = Resources.Load("Icons/Gun Icon") as Texture2D;
		gun.GunType = "Rifle";
		gun.Ammo = _ammo;
		gun.MaxAmmo = 60;
		gun.AmmoTypes = new string[] {"5.56mm", "5.56mm AP", "5.56mm HP"};
		return gun;
	}
}