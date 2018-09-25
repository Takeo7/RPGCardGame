using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkConnection : MonoBehaviour {


    string GameVersion = "0.0.1";
    public GameManager GM;
    public PhotonView GM_PV;


    #region Network

    void Connect() // funcion para conectar a la Network
    {
        if (PlayerPrefs.GetInt("Online") == 0) // If para ver si ejecutamos en Offline
        {
            PhotonNetwork.offlineMode = true; // Ejecutamos el OfflineMode
            OnJoinedLobby(); // Conectamos directamente a una Room sin pasar por la Network
        }
        else
        {

            PhotonNetwork.ConnectUsingSettings(GameVersion);

            // Conectamos usando los settings Default del Usuario
        }
    }

    void OnConnectedToMaster() // Funcion para saber si hemos conectado al Master
    {
        PhotonNetwork.JoinLobby(); // Nos añadimos a la Lobby
    }

    /*void OnGUI() // para que aparezca informacion en la pantalla de la conexion
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString()); // Informacion de la conexion 
    }*/

    void OnJoinedLobby() // Si hemos conectado al Lobby -->
    {
        Debug.Log("OnJoinedLobby"); // Debug.Log para saber cuando se conecta al Lobby
        PhotonNetwork.JoinRandomRoom(); // Conecta a una Room    
    }

    void OnPhotonRandomJoinFailed() // Si falla a conectar a una Room aleatoria
    {
        Debug.Log("OnPhotonRandomJoinFailed"); // Debug.Log para saber cuando falla en entrar a una Room
        PhotonNetwork.CreateRoom(null); // Creamos una Room puesto que no existe ninguna       
    }

    void OnJoinedRoom() // Cuando conecte a una Room
    {
        Debug.Log("OnJoinedRoom"); // Debug.Log para saber cuando conecta a una Room
        GetNumPlayer();
    }
    [PunRPC]
    private void Disconnect()
    {
        PhotonNetwork.Disconnect();
        if (PlayerPrefs.GetInt("Online") == 1)
        {
            PhotonNetwork.room.IsVisible = false;
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            Vector3 pos = transform.localPosition;
            stream.Serialize(ref pos);
        }
        else
        {
            Vector3 pos = Vector3.zero;
            stream.Serialize(ref pos);  // pos gets filled-in. must be used somewhere
        }
    }
    public void GetNumPlayer()
    {
        Debug.Log("Player ID: " + PhotonNetwork.player.ID);
        switch (PhotonNetwork.player.ID)
        {
            case 1:
            case 2:
            case 3:
                break;
            case 4:
                GM_PV.RPC("StartMatch", PhotonTargets.MasterClient);
                PhotonNetwork.room.IsVisible = false;
                break;
            default:
                Disconnect();
                break;
        }
    }
    #endregion
}
