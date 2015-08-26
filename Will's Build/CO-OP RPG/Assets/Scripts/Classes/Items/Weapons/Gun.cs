using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Gun : Item 
{
	[SerializeField]
	private string _gunType; // Pistol, Rifle etc.
	[SerializeField]
	private int _maxAmmo; // the maximum ammo the gun can hold.
	[SerializeField]
	private int _ammo; //How Much ammo is in the gun.
	[SerializeField]
	string[] _ammoTypes; // 9mm etc.

	public enum Bullets {Bullet9mm, Bullet9mmAP, Bullet9mmHP, Bullet556, Bullet556AP, Bullet556HP};
	public Bullets[] TestAmmoTypes;

	public string GunType { get{return _gunType;} set{_gunType = value;} }
	public int Ammo { get{return _ammo;} set{_ammo = value;} }
	public int MaxAmmo { get{return _maxAmmo;} set{_maxAmmo = value;} }
	public string[] AmmoTypes { get{return _ammoTypes;} set{_ammoTypes = value;} }

}

