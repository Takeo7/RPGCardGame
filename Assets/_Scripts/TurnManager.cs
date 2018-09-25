using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {

    int actualTurn;

    public PhotonView PV;

    public delegate void EndTurnDelegate();
    public EndTurnDelegate endTurnDelegate;

	void Start () {
	}

    [PunRPC]
    public void FirstTurn()
    {
        actualTurn = 1;
        if (PhotonNetwork.player.ID == 1)
        {
            PV.RPC("SetTurn", PhotonTargets.All, actualTurn);
        }
    }

    [PunRPC]
    public void ChangeTurn(int i)
    {
        endTurnDelegate();
        actualTurn++;
        if (actualTurn >= 5)
        {
            actualTurn = 1;
        }
        PV.RPC("SetTurn", PhotonTargets.All, actualTurn);
    }
	
	
}
