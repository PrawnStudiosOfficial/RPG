/*
//
//  NEEDS OPTIMIZING (Remove Duplication of Code) NEEDS COMMENTING PROPERLY
//
*/


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Interact : Photon.MonoBehaviour
{
    private Transform cam;
    public int interactRange = 2;

    [HideInInspector]
    public GameObject PlayerInvUI;
    [HideInInspector]
    public GameObject LocalInvUI;
    [HideInInspector]
    public GameObject ContainerInvUI;

    [HideInInspector]
    public GameObject UIItem;

    private bool containerInvOpen = false;
    [HideInInspector]
    public bool localInvOpen = false;

    RaycastHit hit;

    public bool showTradeMessage = false;
    private string tradeFrom;
    private int tradeMessageTime = 4;
    public bool tradeTimer = false;
    public bool tradeAccepted = false;

    void Start()
    {
        cam = transform.FindChild("Camera").transform;

        PlayerInvUI = GameObject.Find("Player Inventory Title");
        LocalInvUI = GameObject.Find("Local Inventory Title");
        ContainerInvUI = GameObject.Find("Container Inventory Title");

        PlayerInvUI.SetActive(false);
        LocalInvUI.SetActive(false);
        ContainerInvUI.SetActive(false);
    }

    void Update()
    {
        if (Physics.Raycast(cam.position, cam.forward, out hit, interactRange))
        {
            if (!localInvOpen)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interactions();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!containerInvOpen)
            {
                if (localInvOpen)
                {
                    UIToggle(LocalInvUI, true, false);
                    //Remove Item Prefabs
                    foreach (Transform child in LocalInvUI.transform.FindChild("Inventory"))
                    {
                        Destroy(child.gameObject);
                    }
                }
                else
                {
                    ShowLocalUI();   
                }
                localInvOpen = !localInvOpen;
            }
        }

        if (Input.GetKeyUp(KeyCode.T))
        {
            if(tradeTimer)
            {
                Debug.Log("Trade Request Accepted");                
                StopCoroutine("TradeRecieved");                
                tradeTimer = false;
                showTradeMessage = false;
                tradeAccepted = true;
            }
            else if(tradeAccepted)
            {
                tradeAccepted = false;
                tradeTimer = false;
                showTradeMessage = false;
                tradeFrom = "";
            }
        }
    }

    void Interactions()
    {
        switch (hit.transform.tag)
        {
            case "Container":
                if (containerInvOpen)
                {
                    //Remove Item Prefabs
                    foreach (Transform child in ContainerInvUI.transform.FindChild("Inventory"))
                    {
                        Destroy(child.gameObject);
                    }

                    foreach (Transform child in PlayerInvUI.transform.FindChild("Inventory"))
                    {
                        Destroy(child.gameObject);
                    }
                    UIToggle(ContainerInvUI, true, true);
                    ContainerInvUI.transform.Find("Title").GetComponent<Text>().text = "Container Name + Items:";
                }
                else
                {
                    UIToggle(ContainerInvUI, false, true);
                    ContainerInvUI.transform.Find("Title").GetComponent<Text>().text = hit.transform.name;

                    //  Create Item Prefabs for Container
                    int top = 20;
                    int bottom = 1100; 
                    for(int i = 0; i < hit.transform.GetComponent<Container>().content.Count; i++)
                    {
                        SpawnItemUIPrefab(ContainerInvUI, i, hit.transform.GetComponent<Container>().content, top, bottom);
                        top += 110;
                        bottom -= 110;
                    }

                    //  Create Item Prefabs for Player
                    top = 20;
                    bottom = 1100;
                    for (int i = 0; i < this.GetComponent<PlayerInventory>().inventory.Count; i++)
                    {
                        SpawnItemUIPrefab(PlayerInvUI, i, this.GetComponent<PlayerInventory>().inventory, top, bottom);
                        top += 110;
                        bottom -= 110;
                    }
                }
                containerInvOpen = !containerInvOpen;
                break;
            case "NPC":     //Interact with the NPC
                Debug.Log("You tried to interact with " + hit.transform.name + " this feature is currently unavalible.");
                break;
            case "Player":  //Send Trade Request
                hit.transform.GetComponent<PhotonView>().RPC("TradeRequest",PhotonTargets.All, photonView.owner.name);
                break;
        }
    }

    public void UIToggle(GameObject Obj, bool Active, bool ToggleControls)
    {
        if(Active)
        {
            PlayerInvUI.SetActive(false);
            Obj.SetActive(false);
            Cursor.visible = false;
            if (ToggleControls)
            {
                this.gameObject.GetComponent<playerMovement>().enabled = true;
                this.gameObject.GetComponent<cameraController>().enabled = true;
            }
        }
        else
        {
            PlayerInvUI.SetActive(true);
            Obj.SetActive(true);
            Cursor.visible = true;
            if (ToggleControls)
            {
                this.gameObject.GetComponent<playerMovement>().enabled = false;
                this.gameObject.GetComponent<cameraController>().enabled = false;
            }
        }
    }

    public void ShowLocalUI()
    {
        UIToggle(LocalInvUI, false, false);
        //  Create Item Prefabs for Local Inventory
        int top = 20;
        int bottom = 1100;
        for (int i = 0; i < this.GetComponent<PlayerInventory>().localInventory.Count; i++)
        {
            SpawnItemUIPrefab(LocalInvUI, i, this.GetComponent<PlayerInventory>().localInventory, top, bottom);
            top += 110;
            bottom -= 110;
        }

        //  Create Item Prefabs for Player
        top = 20;
        bottom = 1100;
        for (int i = 0; i < this.GetComponent<PlayerInventory>().inventory.Count; i++)
        {
            SpawnItemUIPrefab(PlayerInvUI, i, this.GetComponent<PlayerInventory>().inventory, top, bottom);
            top += 110;
            bottom -= 110;
        }
    }


    void SpawnItemUIPrefab (GameObject Parent, int ID, List<Item> Items, int Top, int Bottom)   //Spawns a GUI Item Prefab with the Data passed in.
    {
        GameObject Item = Instantiate(UIItem, Vector3.zero, Quaternion.identity) as GameObject;
        Item.transform.SetParent(Parent.transform.FindChild("Inventory"), false);
        Item.transform.FindChild("Name Text").GetComponent<Text>().text = Items[ID].name;
        Item.GetComponent<RectTransform>().offsetMin = new Vector2(20, Bottom);
        Item.GetComponent<RectTransform>().offsetMax = new Vector2(-20, -Top);
    }

    [PunRPC]
    void TradeRequest(string from)
    {
        if (photonView.isMine) // Checks to see if the trade request is for this client
        {
            tradeFrom = from;
            StartCoroutine("TradeRecieved");
        }
    }

    void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Label");     //
        centeredStyle.alignment = TextAnchor.MiddleCenter;  //
        centeredStyle.fontSize = 20;                        // LOOK FOR BETTER WAY TO DO THIS
        GUI.color = Color.black;                            //
                                                            //

        if (!containerInvOpen && !localInvOpen)
        {
            if (hit.transform != null)
            {
                switch (hit.transform.tag)
                {
                    case "Player":
                        GUI.Label(new Rect(Screen.width * 0.5f - 125, Screen.height * 0.5f - 125, 250, 250), hit.transform.gameObject.GetPhotonView().owner.name, centeredStyle);
                        break;
                    case "Container":
                        GUI.Label(new Rect(Screen.width * 0.5f - 125, Screen.height * 0.5f - 125, 250, 250), "Press E to Open " + hit.transform.name, centeredStyle);
                        break;
                    case "Loose Item":
                        GUI.Label(new Rect(Screen.width * 0.5f - 125, Screen.height * 0.5f - 125, 250, 250), hit.transform.name, centeredStyle);
                        break;
                }
            }
        }

        if(showTradeMessage == true)
        {
            GUI.Label(new Rect(Screen.width * 0.5f - 250, Screen.height * 0.1f - 250, 500, 500), "Trade Request From: " + tradeFrom, centeredStyle);
        }

        if(tradeAccepted == true)
        {
            GUI.Label(new Rect(Screen.width * 0.5f - 250, Screen.height * 0.1f - 250, 500, 500), "You Have Accepted A Trade Request From: " + tradeFrom, centeredStyle);
        }
    }

    public IEnumerator TradeRecieved ()
    {
        showTradeMessage = true;
        tradeTimer = true;
        Debug.Log("Show Trade Message Start from " + tradeFrom + " Press T to Accept");
        yield return new WaitForSeconds(tradeMessageTime * 0.2f);
        showTradeMessage = false;
        yield return new WaitForSeconds(tradeMessageTime * 0.2f);
        showTradeMessage = true;
        yield return new WaitForSeconds(tradeMessageTime * 0.2f);
        showTradeMessage = false;
        yield return new WaitForSeconds(tradeMessageTime * 0.2f);
        showTradeMessage = true;
        yield return new WaitForSeconds(tradeMessageTime * 0.2f);
        showTradeMessage = false;
        tradeTimer = false;
        tradeFrom = "";
        Debug.Log("Show Trade Message End");

    }
}