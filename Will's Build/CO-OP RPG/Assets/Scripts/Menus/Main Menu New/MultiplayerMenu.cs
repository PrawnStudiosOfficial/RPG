using UnityEngine;
using System.Collections;

public class MultiplayerMenu : MonoBehaviour 
{

    public void PublicServerListMenu ()
    {
        Application.LoadLevel("Public Server Menu");
    }

    public void JoinServerMenu ()
    {
        Application.LoadLevel("Join Server Menu");
    }

    public void HostServerMenu ()
    {
        Application.LoadLevel("Create Game Menu");
    }

    public void Back()
    {
        Application.LoadLevel("Main Menu");
    }
}
