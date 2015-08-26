using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MultiplayerLobby : MonoBehaviour 
{
    private GameObject player1Panel;
    private GameObject player2Panel;
    private GameObject player3Panel;
    private GameObject player4Panel;

    private PhotonView photonView;

    private List<int> playerList = new List<int>();

    Hashtable PlayerCustomProps = new Hashtable();
    ExitGames.Client.Photon.Hashtable playerProps = new ExitGames.Client.Photon.Hashtable();

    ExitGames.Client.Photon.Hashtable roomCustomProperties = new ExitGames.Client.Photon.Hashtable();

    void Start()
    {
        player1Panel = GameObject.Find("Player 1 Panel");
        player2Panel = GameObject.Find("Player 2 Panel");
        player3Panel = GameObject.Find("Player 3 Panel");
        player4Panel = GameObject.Find("Player 4 Panel");

        photonView = PhotonView.Get(this);

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("Is Master Client");
            playerList.Add(PhotonNetwork.playerList[0].ID);

            //roomCustomProperties.Add("Status", "Lobby");
            //PhotonNetwork.room.SetCustomProperties(roomCustomProperties);
            //Debug.Log(PhotonNetwork.room.customProperties["Status"]);
        }
        else
        {
            Debug.Log("Is not Master Client");
        }

        playerProps.Add("Ping", PhotonNetwork.GetPing());
        PhotonNetwork.player.SetCustomProperties(playerProps);
        StartCoroutine("UpdatePing");
    }

    void Update ()
    {
        if (PhotonNetwork.connected)
        {
            switch (playerList.Count)
            {
                case 1:
                    player1Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[0]).name;
                    player2Panel.transform.FindChild("Player Name").GetComponent<Text>().text = "Player 2";
                    player3Panel.transform.FindChild("Player Name").GetComponent<Text>().text = "Player 3";
                    player4Panel.transform.FindChild("Player Name").GetComponent<Text>().text = "Player 4";
                    break;
                case 2:
                    player1Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[0]).name;
                    player2Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[1]).name;
                    player3Panel.transform.FindChild("Player Name").GetComponent<Text>().text = "Player 3";
                    player4Panel.transform.FindChild("Player Name").GetComponent<Text>().text = "Player 4";
                    break;
                case 3:
                    player1Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[0]).name;
                    player2Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[1]).name;
                    player3Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[2]).name;
                    player4Panel.transform.FindChild("Player Name").GetComponent<Text>().text = "Player 4";
                    break;
                case 4:
                    player1Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[0]).name;
                    player2Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[1]).name;
                    player3Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[2]).name;
                    player4Panel.transform.FindChild("Player Name").GetComponent<Text>().text = PhotonPlayer.Find(playerList[3]).name;
                    break;
            }


            if (playerList.Count == 1)
            {
                player1Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[0]).customProperties["Ping"].ToString();
                player2Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: Not Connected";
                player3Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: Not Connected";
                player4Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: Not Connected";
            }
            else if (playerList.Count == 2)
            {
                player1Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[0]).customProperties["Ping"].ToString();
                player2Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[1]).customProperties["Ping"].ToString();
                player3Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: Not Connected";
                player4Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: Not Connected";
            }
            else if (playerList.Count == 3)
            {
                player1Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[0]).customProperties["Ping"].ToString();
                player2Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[1]).customProperties["Ping"].ToString();
                player3Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[2]).customProperties["Ping"].ToString();
                player4Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: Not Connected";
            }
            else if (playerList.Count == 4)
            {
                player1Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[0]).customProperties["Ping"].ToString();
                player2Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[1]).customProperties["Ping"].ToString();
                player3Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[2]).customProperties["Ping"].ToString();
                player4Panel.transform.FindChild("Player Ping").GetComponent<Text>().text = "Ping: " + PhotonPlayer.Find(playerList[3]).customProperties["Ping"].ToString();
            }

            switch (PhotonNetwork.isMasterClient)
            {
                case false:
                    GameObject.Find("Button Start").GetComponent<Button>().interactable = false;
                    break;
                case true:
                    GameObject.Find("Button Start").GetComponent<Button>().interactable = true;
                    break;
            }
        }
    }

    public void CallStartGame()
    {
        roomCustomProperties.Add("Status", "In-Game");
        PhotonNetwork.room.SetCustomProperties(roomCustomProperties);

        PhotonView photonView = PhotonView.Get(this);
        photonView.RPC("StartGame", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void StartGame ()
    {
        PhotonNetwork.LoadLevel("Tutorial");
    }

    public void QuitGame()
    {
        PhotonNetwork.Disconnect();
        Application.LoadLevel("Multiplayer Menu");
    }

    public void OnPhotonPlayerConnected( PhotonPlayer player)
    {
        Debug.Log("Player Connected: " + player.name); // not seen if you're the player connecting
        
        if(PhotonNetwork.isMasterClient)
        {
            playerList.Add(player.ID);

            photonView.RPC("ClearPlayerList", PhotonTargets.Others);
            for (int i = 0; i < playerList.Count; i++)
            {
                photonView.RPC("AddPlayer", PhotonTargets.Others, playerList[i]);
            }
        }
    }

    public void OnPhotonPlayerDisconnected(PhotonPlayer player)
    {
        Debug.Log("Player Disconnected: " + player.name);
        
        if (PhotonNetwork.isMasterClient)
        {
            playerList.Remove(player.ID);

            photonView.RPC("ClearPlayerList", PhotonTargets.Others);
            for (int i = 0; i < playerList.Count; i++)
            {
                photonView.RPC("AddPlayer", PhotonTargets.Others, playerList[i]);
            }
        }
    }

    [PunRPC]
    public void ClearPlayerList()
    {
        playerList = new List<int>();
    }

    [PunRPC]
    public void AddPlayer(int playerID)
    {
        playerList.Add(playerID);
    }

    [PunRPC]
    public void RemovePlayer(int playerID)
    {
        playerList.Remove(playerID);
    }

    private IEnumerator UpdatePing()
    {
        yield return new WaitForSeconds(1);
        playerProps.Remove("Ping");
        playerProps.Add("Ping", PhotonNetwork.GetPing());
        PhotonNetwork.player.SetCustomProperties(playerProps);
        StartCoroutine("UpdatePing");
    }
}
