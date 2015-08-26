using UnityEngine;
using System.Collections;

public class ServerInfo : MonoBehaviour 
{

	public string serverVersion;

	void ConnectToMainServer ()
	{
		var clientVersion = PlayerPrefs.GetString ("Client Version");
		Debug.Log ("Trying To Connect To Main Server");
		PhotonNetwork.ConnectToMaster ("www.prawnstudios.com", 5055, "e2e7e52c-97ef-415b-90c4-a1c61727151a", clientVersion);

		StartCoroutine ( CheckConnection() );
	}

	IEnumerator CheckConnection ()
	{
		if (PhotonNetwork.connecting) 
		{ 
			yield return new WaitForSeconds(0.5f); 
			StartCoroutine (CheckConnection() ); 
		}
		else if (PhotonNetwork.connected) 
		{ 
			Debug.Log ("Connected To Main Server"); 
		}
		else 
		{ 
			Debug.Log ("Failed To Connect To Main Server"); 
		}
	}

	void OnGUI ()
	{
		GUILayout.Label ( PhotonNetwork.connectionStateDetailed.ToString() );
		if(PhotonNetwork.connected)
		{
			GUILayout.Label ( "Ping = " + PhotonNetwork.GetPing ().ToString () );
		}
	}
}
