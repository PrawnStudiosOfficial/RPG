using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.IO;

public class MenuManager : MonoBehaviour 
{

	public GameObject mainMenu;
	public GameObject singlePlayerMenu;
	public GameObject multiplayerMenu;

	public GameObject hostGameMenu;
	public GameObject joinGameMenu;
	public GameObject findGameMenu;

	public string ip;
	public int port;

	public string roomName;
	public bool roomVisible;
	public bool roomOpen;
	public int roomMaxPlayers;

	void Start () 
	{
		mainMenu = GameObject.Find ("Main Menu");

		singlePlayerMenu = GameObject.Find ("Single Player Menu");
		multiplayerMenu = GameObject.Find ("Multiplayer Menu");

		hostGameMenu = GameObject.Find ("Create Game Menu");
		joinGameMenu = GameObject.Find ("Join Server Menu");
		findGameMenu = GameObject.Find ("Public Server Menu");

		singlePlayerMenu.SetActive (false);
		multiplayerMenu.SetActive (false);
		hostGameMenu.SetActive (false);
		joinGameMenu.SetActive (false);
		findGameMenu.SetActive (false);
	}

	public void ShowMainMenu ()
	{
		mainMenu.SetActive (true);
		singlePlayerMenu.SetActive (false);
		multiplayerMenu.SetActive (false);

		PhotonNetwork.offlineMode = false;
	}

	public void ShowSinglePlayerMenu ()
	{
		mainMenu.SetActive (false);
		singlePlayerMenu.SetActive (true);
		multiplayerMenu.SetActive (false);

		PhotonNetwork.offlineMode = true;
	}

	public void ShowMultiplayerMenu ()
	{
		mainMenu.SetActive (false);
		singlePlayerMenu.SetActive (false);
		multiplayerMenu.SetActive (true);

		hostGameMenu.SetActive (false);
		joinGameMenu.SetActive (false);
		findGameMenu.SetActive (false);

		PhotonNetwork.offlineMode = false;
		if(PhotonNetwork.connected) { PhotonNetwork.Disconnect (); }
	}

	public void CreateGameMenu ()
	{
		multiplayerMenu.SetActive (false);
		hostGameMenu.SetActive (true);
		joinGameMenu.SetActive (false);
		findGameMenu.SetActive (false);
	}
	
	public void JoinGameMenu ()
	{
		multiplayerMenu.SetActive (false);
		hostGameMenu.SetActive (false);
		joinGameMenu.SetActive (true);
		findGameMenu.SetActive (false);
	}
	
	public void FindGameMenu ()
	{
		multiplayerMenu.SetActive (false);
		hostGameMenu.SetActive (false);
		joinGameMenu.SetActive (false);
		findGameMenu.SetActive (true);

		ConnectToPublicServer ();
	}

	public void SetIP (string _ip)
	{
		ip = _ip;
	}

	public void SetPort (string _port)
	{
		port = int.Parse(_port);
	}

	public void StartServer ()
	{
		if(File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Prawn Studios/RPG/Server/Start Server.bat"))
		{
			System.Diagnostics.Process proc = new System.Diagnostics.Process();
			proc.StartInfo.FileName = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Prawn Studios/RPG/Server/Start Server.bat";
			proc.StartInfo.WorkingDirectory = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Prawn Studios/RPG/Server";
			proc.Start();
		}
		else
		{
			UnityEngine.Debug.LogError("Start Server.bat Missing From: " + System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/My Games/Prawn Studios/RPG/Server/");
		}
	}

	public void ConnectToPrivateServer ()
	{
		if (PhotonNetwork.connected) { PhotonNetwork.Disconnect(); }
		PhotonNetwork.ConnectToMaster (ip, port, "e2e7e52c-97ef-415b-90c4-a1c61727151a", PlayerPrefs.GetString("Client Version"));
	}

	public void ConnectToPublicServer ()
	{
		PhotonNetwork.ConnectToMaster ("prawnstudios.com", 5055, "e2e7e52c-97ef-415b-90c4-a1c61727151a", PlayerPrefs.GetString("Client Version"));
	}
	
	public void CreateGame ()
	{
		//Start Server
		RoomOptions roomOptions = new RoomOptions ();
		roomOptions.isOpen = true;
		roomOptions.isVisible = true;
		roomOptions.maxPlayers = 4;

		PhotonNetwork.CreateRoom (roomName, roomOptions, TypedLobby.Default);
        Application.LoadLevel("Tutorial");
	}

	void OnGUI ()
	{
		GUILayout.Label (PhotonNetwork.connectionState.ToString());
	}
}
