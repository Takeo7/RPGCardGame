using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetworkConnection : MonoBehaviour {
    public Text test;

    int readyNumPlayers;

    string GameVersion = "0.0.1";
    public GameManager GM;
    public PhotonView GM_PV;

    public PhotonView PV;

    public GameObject matchingPanel;

    private void Start()
    {
        Connect();
    }

    #region Network

    void Connect() // funcion para conectar a la Network
    {
        PhotonNetwork.ConnectUsingSettings(GameVersion);
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
        test.text = "Mi ID: " + PhotonNetwork.player.ID;
        Debug.Log("Player ID: " + PhotonNetwork.player.ID);
        switch (PhotonNetwork.player.ID)
        {
            case 1:
                PV.RPC("InstantiateNET", PhotonTargets.AllBuffered, "peter");
                break;
            case 2:
                PV.RPC("InstantiateNET", PhotonTargets.AllBuffered, "Luis");
                break;
            case 3:
                PV.RPC("InstantiateNET", PhotonTargets.AllBuffered, "boris");
                break;
            case 4:
                PV.RPC("InstantiateNET", PhotonTargets.AllBuffered, "Edu");
                break;
            default:
                Disconnect();
                break;
        }
    }
    [PunRPC]
    public void InstantiateNET(string n)
    {
        if (PhotonNetwork.player.ID == 1)
        {
            GameObject g;
            g = PhotonNetwork.Instantiate("matchingIcon", Vector3.zero, Quaternion.identity, 0);
            g.GetComponent<PhotonView>().RPC("ChangeParams", PhotonTargets.AllBuffered, n);
            g.transform.SetParent(matchingPanel.transform);
            g.transform.localScale = Vector3.one;
            GM.maxPlayers++;
        }
    }

    public void ReadyButton()
    {
        PV.RPC("UpdateReady", PhotonTargets.AllBuffered, PhotonNetwork.player.ID);
    }
    [PunRPC]
    public void UpdateReady(int i)
    {
        if (PhotonNetwork.player.ID == 1)
        {
            readyNumPlayers++;
            PV.RPC("SetReady", PhotonTargets.AllBuffered, i);
            if (readyNumPlayers >= GM.maxPlayers)
            {
                AllReady();
            }
        }
    }
    [PunRPC]
    public void SetReady(int i)
    {
        matchingPanel.transform.GetChild(i - 1).GetComponent<MatchingIcon>().readyImage.enabled = true;
    }
    public void AllReady()
    {
        Debug.Log("Start Match");
        GM_PV.RPC("StartMatch", PhotonTargets.All);
        PhotonNetwork.room.IsVisible = false;
    }
    #endregion
}
