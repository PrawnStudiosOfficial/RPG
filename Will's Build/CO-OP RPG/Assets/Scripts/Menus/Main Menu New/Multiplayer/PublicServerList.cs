using UnityEngine;
using System.Collections;

public class PublicServerList : MonoBehaviour 
{
    private Rect a = new Rect(10, 10, 1900, 1060);

    void Start()
    {
        PhotonNetwork.ConnectToMaster("prawnstudios.com", 5055, PlayerPrefs.GetString("App ID"), PlayerPrefs.GetString("Version"));
    }

    void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Multiplayer Lobby"); //Loads the Lobby
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        //GUI.Box(ResizeGUI(a), "Menu");
        GUILayout.BeginArea(ResizeGUI(a));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        //GUILayout.FlexibleSpace();
        //
        if (PhotonNetwork.GetRoomList().Length > 0)
        {
            foreach (RoomInfo game in PhotonNetwork.GetRoomList())
            {
                if (GUILayout.Button("Name: " + game.name + "\n Player Count:" + game.playerCount + "/" + game.maxPlayers + "\n Game Status:" + game.customProperties["Status"]))
                {
                    PhotonNetwork.JoinRoom(game.name);
                }
            }
        }
        else
        {
            GUILayout.Label("No Games Found");
        }
        if(GUILayout.Button("Back"))
        {
            Application.LoadLevel("Multiplayer Menu");
        }
        //
        //GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    Rect ResizeGUI(Rect _rect)
    {
        float FilScreenWidth = _rect.width / 1920;
        float rectWidth = FilScreenWidth * Screen.width;
        float FilScreenHeight = _rect.height / 1080;
        float rectHeight = FilScreenHeight * Screen.height;
        float rectX = (_rect.x / 1920) * Screen.width;
        float rectY = (_rect.y / 1080) * Screen.height;

        return new Rect(rectX, rectY, rectWidth, rectHeight);
    }
}
