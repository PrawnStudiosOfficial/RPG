using UnityEngine;
using System.Collections;

public class SpawnPlayer : Photon.MonoBehaviour 
{
    private Vector3 player1Spawn = new Vector3(0, 0, 0);
    private Vector3 player2Spawn = new Vector3(5, 0, 0);
    private Vector3 player3Spawn = new Vector3(10, 0, 0);
    private Vector3 player4Spawn = new Vector3(15, 0, 0);

    [HideInInspector]
    public Vector3 activeSpawn;


    void Start () 
	{
        PhotonNetwork.automaticallySyncScene = true;

        switch (PhotonNetwork.player.ID)
        {
            case 0:
                activeSpawn = player1Spawn;
                break;
            case 1:
                activeSpawn = player2Spawn;
                break;
            case 2:
                activeSpawn = player3Spawn;
                break;
            case 3:
                activeSpawn = player4Spawn;
                break;
        }

        GameObject clientPlayer = PhotonNetwork.Instantiate(("Prefabs/Player"), activeSpawn, Quaternion.identity, 0);
        clientPlayer.GetComponent<playerMovement>().enabled = true;
        clientPlayer.GetComponent<cameraController>().enabled = true;
        clientPlayer.GetComponent<Interact>().enabled = true;
        clientPlayer.GetComponent<PlayerInventory>().enabled = true;
        clientPlayer.GetComponent<MeshCollider>().enabled = true;
        clientPlayer.GetComponent<Pause>().enabled = true;
        clientPlayer.transform.FindChild("Camera").gameObject.SetActive(true);
    }
}
