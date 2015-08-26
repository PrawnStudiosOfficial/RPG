using UnityEngine;
using System.Collections;

public class SingleplayerMenu : MonoBehaviour 
{

    public void Continue ()
    {

    } 

    public void NewGame ()
    {

    }

    public void LoadGame ()
    {

    }

    public void Back ()
    {
        Application.LoadLevel("Main Menu");
    }
}
