using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour {

    int level;
    public Text levelText;
    public PhotonView PV;
    int parentSpawn;
	public int playerID;

    Transform[] spawnPoints;

    private void Awake()
    {
        spawnPoints = GameManager.instance.spawnPoints;
    }
    private void Start()
    {
        transform.SetParent(spawnPoints[parentSpawn]);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }

    [PunRPC]
    public void SetLevel(int i)
    {
        level = i;
        levelText.text = "" + level;
    }
    [PunRPC]
    public void UpLevel(int i)
    {
        level += i;
        levelText.text = "" + level;
    }
    [PunRPC]
    public void DownLevel(int i)
    {
        level -= i;
        levelText.text = "" + level;
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
    [PunRPC]
    public void SetParentSpawn(int i)
    {
        parentSpawn = i;
        transform.SetParent(spawnPoints[i]);
        transform.localPosition = Vector3.zero;
        transform.localScale = Vector3.one;
    }
	[PunRPC]
	public void SetPlayerID(int i)
	{
		playerID = i;
	}
}
