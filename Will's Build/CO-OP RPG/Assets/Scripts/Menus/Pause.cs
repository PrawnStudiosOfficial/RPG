using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour
{
    string mainMenuSceneName;
    private bool pauseEnabled = false;
    private bool showGraphicsDropDown = false;

    void Start()
    {
        pauseEnabled = false;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKeyDown("escape"))
        {

            //check if game is already paused		
            if (pauseEnabled == true)
            {
                //unpause the game
                pauseEnabled = false;
                Cursor.visible = false;
                this.gameObject.GetComponent<playerMovement>().enabled = true;
                this.gameObject.GetComponent<cameraController>().enabled = true;
            }

            //else if game isn't paused, then pause it
            else if (pauseEnabled == false)
            {
                pauseEnabled = true;
                Cursor.visible = true;
                this.gameObject.GetComponent<playerMovement>().enabled = false;
                this.gameObject.GetComponent<cameraController>().enabled = false;
            }
        }
    }

    void OnGUI()
    {

        if (pauseEnabled == true)
        {

            //Make a background box
            GUI.Box(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 250, 200), "Pause Menu");

            //Make Main Menu button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50, 250, 50), "Main Menu"))
            {
                PhotonNetwork.Disconnect();
                Application.LoadLevel("Main Menu");
            }

            //Make Change Graphics Quality button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 250, 50), "Change Graphics Quality"))
            {

                if (showGraphicsDropDown == false)
                {
                    showGraphicsDropDown = true;
                }
                else
                {
                    showGraphicsDropDown = false;
                }
            }

            //Create the Graphics settings buttons, these won't show automatically, they will be called when
            //the user clicks on the "Change Graphics Quality" Button, and then dissapear when they click
            //on it again....
            if (showGraphicsDropDown == true)
            {
                if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2, 250, 50), "Fastest"))
                {
                    QualitySettings.SetQualityLevel(0);
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 50, 250, 50), "Fast"))
                {
                    QualitySettings.SetQualityLevel(1);
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 100, 250, 50), "Simple"))
                {
                    QualitySettings.SetQualityLevel(2);
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 150, 250, 50), "Good"))
                {
                    QualitySettings.SetQualityLevel(3);
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 200, 250, 50), "Beautiful"))
                {
                    QualitySettings.SetQualityLevel(4);
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 150, Screen.height / 2 + 250, 250, 50), "Fantastic"))
                {
                    QualitySettings.SetQualityLevel(5);
                }

                if (Input.GetKeyDown("escape"))
                {
                    showGraphicsDropDown = false;
                }
            }

            //Make quit game button
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 50, 250, 50), "Quit Game"))
            {
                PhotonNetwork.Disconnect();
                Application.Quit();
            }
        }
    }
}
