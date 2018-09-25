using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour {


    #region Singleton
    public static TurnManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    int actualTurn;

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
            SetTurn(actualTurn);
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
        SetTurn(i);
    }
    [PunRPC]
    public void SetTurn(int i)
    {
        Debug.Log("SetTurn");
        if (PhotonNetwork.player.ID == i)
        {
            player.MyTurn = true;
            startTurnDelegate();
        }
        else
        {
            player.MyTurn = false;
        }
        Debug.Log("Mi Turno: " + player.MyTurn);
    }

}
