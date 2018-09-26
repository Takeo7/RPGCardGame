using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour {

    public GameManager GM;
    public PhotonView PV;
    public Animator anim;
    public Button buttonDoor;
    TurnManager TM;
    Player player;
    int firstDoor = 0;

    public Image Monster;
    public Button monsterButton;
    public Sprite chestSprite;
    public bool chestOn = false;

    public delegate void OpenFirstDoor();
    public OpenFirstDoor openFirstDoor;

    public delegate void OpenSecondDoor();
    public OpenSecondDoor openSecondDoor;

    void Awake()
    {
        player = Player.instance;
        TM = TurnManager.instance;
        TM.door = this;
        TM.startTurnDelegate += ResetDoor;
        TM.startTurnDelegate += CheckDoorInteractuable;        
    }
    [PunRPC]
    public void VisualDoor()
    {
        anim.SetTrigger("Trigger");
    }


    public void OpenDoor()
    {
        PV.RPC("OpenDoorRPC", PhotonTargets.All);
    }
    [PunRPC]
    public void OpenDoorRPC()
    {
        if (firstDoor == 0)
        {
            Debug.Log("VisualDoor");
            VisualDoor();
            firstDoor++;
            openFirstDoor();
        }
        else if (firstDoor == 1)
        {
            firstDoor++;
            openSecondDoor();
        }
    }

    public void DesactivateDoor()
    {
        buttonDoor.interactable = false;
    }

    public void ResetDoor()
    {
        firstDoor = 0;
        Monster.enabled = false;
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
    [PunRPC]
    public void ActivateMonster(int id)
    {
        Debug.Log("ActivateMonster");
        Monster.gameObject.SetActive(true);
        Monster.sprite = GM.Monsters[id];
    }

    [PunRPC]
    public void ShowTreasure()
    {
        chestOn = true;
        Monster.sprite = chestSprite;
    }
    public void MonsterButton()
    {
        if (chestOn == false)
        {
            PV.RPC("ShowTreasure", PhotonTargets.All);
        }
        else
        {
            PV.RPC("DrawTreasureCards", PhotonTargets.All);
            Monster.gameObject.SetActive(false);
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
