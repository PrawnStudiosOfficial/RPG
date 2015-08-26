using UnityEngine;
using System.Collections;

public class JoinServerMenu : MonoBehaviour 
{

    private GameObject IP; //IP InputField Object.
    private GameObject Port; //Port InputField Object.

    public string serverIP = "prawnstudios.com"; //String for custom IP used when hosting private servers.
    public string serverPort = "5055"; //INT for custom Port used when hosting private servers.
    public string gameName;

    void Start()
    {
        IP = GameObject.Find("IP Input");
        Port = GameObject.Find("Port Input");
    }

    public void Connect () 
	{
        PhotonNetwork.ConnectToMaster(serverIP, int.Parse(serverPort), PlayerPrefs.GetString("App ID"), PlayerPrefs.GetString("Version"));

        StartCoroutine(CheckConnection());
    }

    public void Back()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        Application.LoadLevel("Multiplayer Menu");
    }

    public void UpdateIP(string _ip)
    {
        serverIP = _ip;
    }

    public void UpdatePort(string _port)
    {
        serverPort = _port;
    }

    public void UpdateName(string _name)
    {
        gameName = _name;
    }

    public IEnumerator CheckConnection()
    {
        bool connected = false; //Temp bool to see if we are connected to the serer
        while (connected == false)
        {
            yield return new WaitForSeconds(1);
            if (PhotonNetwork.connected) //Checks to see if we are connected to the server
            {
                connected = true; //Updates the Bool to stop the loop
                PhotonNetwork.JoinRoom(gameName, false);
            }
        }
    }

    void OnJoinedRoom()
    {
        Application.LoadLevel("Multiplayer Lobby"); //Loads the Lobby
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()); //Debug
    }
}
