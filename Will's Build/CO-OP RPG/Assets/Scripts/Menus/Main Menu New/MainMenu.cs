using UnityEngine;
using System.Collections;
using System.IO;

public class MainMenu : MonoBehaviour 
{
    private string tempData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "/Temp/Prawn Studios/RPG/"; //Default location for the save game cache.
    private string gameData = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Prawn Studios/RPG/"; //Default location for the game data to be saved (Save Files, Config Files)
    private string gameVersion = "A001";

    public void SinglePlayerMenu ()
    {
        Application.LoadLevel("Singleplayer Menu"); //Loads the Singleplayer Menu.
    }

    public void MultiplayerMenu ()
    {
        Application.LoadLevel("Multiplayer Menu"); //Loads the Multiplayer Menu.
    }

    void Start ()
    {
        DataCheck(); //Checks the Games Core Data Files to make sure everything exists. 
    }

    void DataCheck()
    {
        //
        //  PLAYERPREFS CHECKS
        //
        if (!PlayerPrefs.HasKey("Temp Data")) //Checks to see if the Save Game Cache Location is already saved as a PlayerPref
        {
            PlayerPrefs.SetString("Temp Data", tempData); //Sets the Save Game Cache Location to a PlayerPref
        }

        if (!PlayerPrefs.HasKey("Save Cache")) //Checks to see if the Save Game Cache Location is already saved as a PlayerPref
        {
            PlayerPrefs.SetString("Save Cache", tempData + "/Saves/"); //Sets the Save Game Cache Location to a PlayerPref
        }

        if (!PlayerPrefs.HasKey("Game Data")) //Checks to see if the Game Data Location is already saved as a PlayerPref
        {
            PlayerPrefs.SetString("Game Data", gameData); //Sets the Game Data Location to a PlayerPref
        }

        if (!PlayerPrefs.HasKey("Save Location")) //Checks to see if the Game Data Location is already saved as a PlayerPref
        {
            PlayerPrefs.SetString("Save Location", gameData + "/Saves/"); //Sets the Game Data Location to a PlayerPref
        }

        if (!PlayerPrefs.HasKey("App ID")) //Checks to see if the App ID is already saved as a PlayerPref
        {
            PlayerPrefs.SetString("App ID", "e2e7e52c - 97ef - 415b - 90c4 - a1c61727151a"); //Sets the App ID to a PlayerPref
        }

        if (!PlayerPrefs.HasKey("Version")) //Checks to see if the Game Version is already saved as a PlayerPref
        {
            PlayerPrefs.SetString("Version", gameVersion); //Sets the Game Version as a PlayerPref
        }
        else 
        {
            if(PlayerPrefs.GetString("Version") != gameVersion) //Checks to see if the Game Version Player Pref is up to Date.
            {
                PlayerPrefs.SetString("Version", gameVersion); //Sets the Updated Game Version as the PlayerPref
            }
        }

        //
        //  DIRECTORY CHECKS
        //
        if (!Directory.Exists(gameData)) //Checks to see if the Game Data Folder Exists.
        {
            Directory.CreateDirectory(gameData); //Create the Game Data Folder if it does not exist. 
        }

        if (!Directory.Exists(gameData + "/Saves/")) //Checks to see if the Saves Folder Exists.
        {
            Directory.CreateDirectory(gameData + "/Saves/"); //Create the Saves Folder if it does not exist. 
        }

        if (!Directory.Exists(tempData)) //Checks to see if the Temp Data Folder Exists.
        {
            Directory.CreateDirectory(tempData); //Create the Temp Data Folder if it does not exist. 
        }

        if (!Directory.Exists(tempData + "/Saves/")) //Checks to see if the Saves Cache Folder Exists.
        {
            Directory.CreateDirectory(tempData + "/Saves/"); //Create the Saves Cache Folder if it does not exist. 
        }

        //
        //  FILE CHECKS
        //
    }

    public void UpdatePlayerName (string name)
    {
        PhotonNetwork.playerName = name;
    }
}
