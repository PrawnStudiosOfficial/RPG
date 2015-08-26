using UnityEngine;

public class NetworkPlayer : Photon.MonoBehaviour
{
    private Vector3 updatedPosition;
    private Quaternion updatedRotation;


    void Update()
    {
        if (!photonView.isMine)
        {
            transform.position = Vector3.Lerp(transform.position, updatedPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, updatedRotation, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            // Network player, receive data
            this.updatedPosition = (Vector3)stream.ReceiveNext();
            this.updatedRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
