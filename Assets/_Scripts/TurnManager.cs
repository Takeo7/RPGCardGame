using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {


    #region Singleton
    public static TurnManager instance;
    public PhotonView TM_PV;
    public DoorScript door;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    public int actualTurn;

    public GameManager GM;

    Player player;

    public delegate void StartTurnDelegate();
    public StartTurnDelegate startTurnDelegate;

    public delegate void EndTurnDelegate();
    public EndTurnDelegate endTurnDelegate;

	void Start () {
        player = Player.instance;
	}

    [PunRPC]
    public void FirstTurn()
    {
        actualTurn = 1;
        if (PhotonNetwork.player.ID == 1)
        {
            TM_PV.RPC("SetTurn", PhotonTargets.All, actualTurn);
        }
    }

    [PunRPC]
    public void ChangeTurn(int i)
    {
        endTurnDelegate();
        actualTurn++;
        if (actualTurn >= GM.maxPlayers)
        {
            actualTurn = 1;
        }
        SetTurn(i);
    }
    [PunRPC]
    public void SetTurn(int i)
    {
        if (PhotonNetwork.player.ID == i)
        {
            player.MyTurn = true;
            startTurnDelegate();
            //door.ResetDoor();
            //door.CheckDoorInteractuable();
        }
        else
        {
            player.MyTurn = false;
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
}
