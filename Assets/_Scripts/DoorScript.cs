using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {

    public PhotonView PV;
    public Animator anim;
    public Button buttonDoor;
    TurnManager TM;
    Player player;
    int firstDoor = 0;

    public delegate void OpenFirstDoor();
    public OpenFirstDoor openFirstDoor;

    public delegate void OpenSecondDoor();
    public OpenSecondDoor openSecondDoor;

    void Awake()
    {
        player = Player.instance;
        TM = TurnManager.instance;
        TM.startTurnDelegate += CheckDoorInteractuable;
        TM.startTurnDelegate += ResetDoor;
    }
    [PunRPC]
    public void VisualDoor()
    {
        anim.SetTrigger("Trigger");
    }


    public void OpenDoor()
    {
        if (firstDoor == 0)
        {
            PV.RPC("VisualDoor", PhotonTargets.All);
            firstDoor++;
            openFirstDoor();
        }
        else if (firstDoor == 1)
        {
            firstDoor++;
            openSecondDoor();           
        }

    }

    public void ResetDoor()
    {
        firstDoor = 0;
    }

    public void CheckDoorInteractuable()
    {
        if (player.MyTurn)
        {
            buttonDoor.interactable = true;
        }
        else
        {
            buttonDoor.interactable = false;
        }
    }
}
