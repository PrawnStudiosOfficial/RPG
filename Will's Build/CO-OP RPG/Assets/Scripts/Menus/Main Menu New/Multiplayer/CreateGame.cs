using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CreateGame : MonoBehaviour
{

    private string defaultIP = "prawnstudios.com"; //The default IP to use if one is not entered.
    private string defaultPort = "5055"; //The default Port to use if one is not entered;

    private string serverIP = "prawnstudios.com"; //String for custom IP used when hosting private servers.
    private string serverPort = "5055"; //INT for custom Port used when hosting private servers.

    private string gameName; //The Name used to create the room.

    private bool gamePrivate = false; //Is the game on Public Or a Private Server.
    private bool gameHidden = false; //Is the game shown on the gamelist of the Server(public & private).

    private GameObject IP; //IP InputField Object.
    private GameObject Port; //Port InputField Object.

    public ExitGames.Client.Photon.Hashtable customProps;
    ExitGames.Client.Photon.Hashtable roomCustomProperties = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        IP = GameObject.Find("IP Input");
        Port = GameObject.Find("Port Input");
    }

    public void UpdateName(string _name)
    {
        gameName = _name;
    }

    public void UpdateIP(string _ip)
    {
        serverIP = _ip;
    }

    public void UpdatePort(string _port)
    {
        serverPort = _port;
    }

    public void TogglePrivate()
    {
        if (gamePrivate)
        {
            gamePrivate = false;
            IP.GetComponent<InputField>().interactable = false; //Toggles if the InputField can be edited.
            Port.GetComponent<InputField>().interactable = false; //Toggles if the InputField can be edited.
            serverIP = "prawnstudios.com"; //Sets the IP to the defualt IP.
            serverPort = "5055"; //Sets the Port to the defualt Port.
        }
        else
        {
            gamePrivate = true;
            IP.GetComponent<InputField>().interactable = true; //Toggles if the InputField can be edited.
            Port.GetComponent<InputField>().interactable = true; //Toggles if the InputField can be edited.
            serverIP = IP.transform.FindChild("Text").GetComponent<Text>().text; //Sets the IP to the one from the IP InputField.
            serverPort = Port.transform.FindChild("Text").GetComponent<Text>().text; //Sets the Port to the one from the Port InputField.
        }
    }

    public void ToggleVisible()
    {
        gameHidden = !gameHidden;
    }

    public void Back()
    {
        if (PhotonNetwork.connected)
        {
            PhotonNetwork.Disconnect();
        }
        Application.LoadLevel("Multiplayer Menu");
    }

    public void Create()
    {
        if (serverIP == "")
        {
            serverIP = defaultIP;
        }
        if (serverPort == "")
        {
            serverPort = defaultPort;
        }

        PhotonNetwork.ConnectToMaster(serverIP, int.Parse(serverPort), PlayerPrefs.GetString("App ID"), PlayerPrefs.GetString("Version"));

        StartCoroutine(CheckConnection());
    }

    void CreateRoom()
    {
        //RoomOptions roomOptions = new RoomOptions() { isVisible = !gameHidden, maxPlayers = 4};
        //PhotonNetwork.CreateRoom(gameName, roomOptions, TypedLobby.Default);

        string[] roomPropsInLobby = { "Status"};
        //ExitGames.Client.Photon.Hashtable customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "Status", 0 } };
        PhotonNetwork.CreateRoom(gameName, !gameHidden, true, 4, roomCustomProperties, roomPropsInLobby);
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()); //Debug
    }

    public IEnumerator CheckConnection ()
    {
        bool connected = false; //Temp bool to see if we are connected to the serer
        while (connected == false)
        {
            yield return new WaitForSeconds(1);
            if (PhotonNetwork.connected) //Checks to see if we are connected to the server
            {
                connected = true; //Updates the Bool to stop the loop
                CreateRoom(); //Creates Room
            }
        }
    }

    void OnCreatedRoom()
    {
        roomCustomProperties.Add("Status", "Lobby");
        PhotonNetwork.room.SetCustomProperties(roomCustomProperties);
        Application.LoadLevel("Multiplayer Lobby"); //Loads the Lobby
    }
}
