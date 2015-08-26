using UnityEngine;
using System.Collections;
using System.IO;

public class ClientInfo : MonoBehaviour 
{
	private string gameDataLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Prawn Studios/RPG/";
	public string clientVersion;
	public string saveFileLocation;
	private string tempSaveLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "/Temp/Prawn Studios/RPG/Temp Save/";
	INIParser gameINI = new INIParser();

	void Start () 
	{
		CheckDirectories ();
		
		PlayerPrefs.SetString ("Game Data Location", gameDataLocation);
		PlayerPrefs.SetString ("Client Version", clientVersion);
		PlayerPrefs.SetString ("Save File Location", saveFileLocation);
		PlayerPrefs.SetString ("Temp Save Location", tempSaveLocation);
	}

	void CheckDirectories ()
	{
		Debug.Log ("Directory Check Started");
		if(!Directory.Exists(gameDataLocation))
		{
			Directory.CreateDirectory(gameDataLocation);
			Debug.Log("Game Data Directory Was Not Found, So Has Been Created");
		}
		else { Debug.Log("Game Data Directory Was Found"); }

		if(!Directory.Exists(gameDataLocation + "/Saves/"))
		{
			Directory.CreateDirectory(gameDataLocation + "/Saves/");
			Debug.Log("Save Data Directory Was Not Found, So Has Been Created");
		}
		else { Debug.Log("Save Data Directory Was Found"); }

		if(Directory.Exists(tempSaveLocation))
		{
			Directory.Delete(tempSaveLocation, true);
			Directory.CreateDirectory(tempSaveLocation);
			Debug.Log("Temp Save Data Directory Was Found And Has Been Refreshed");
		}
		else
		{
			Directory.CreateDirectory(tempSaveLocation);
			Debug.Log("Temp Save Data Directory Was Not Found, So Has Been Created");
		}

		Debug.Log ("Directory Check Finished");
		CheckFiles ();
	}

	void CheckFiles ()
	{
		Debug.Log ("File Check Started");
		if (!File.Exists (gameDataLocation + "/Game.ini")) {
			CreateGameINI (); 
			Debug.Log ("Game.ini Was Not Found So Has Been Created");
		} 
		else { Debug.Log ("Game.ini Was Found"); }
		Debug.Log ("File Check Finished");

	}
	
	void CreateGameINI () 
	{
		gameINI.Open (gameDataLocation + "/Game.ini");
		//
		
		//
		gameINI.Close ();
	}
}
