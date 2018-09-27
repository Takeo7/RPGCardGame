using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingIcon : MonoBehaviour {

    public Text nombre;
    public Image icono;
    public Image readyImage;

    private void Start()
    {
        transform.SetParent(GameManager.instance.panelTransofrm);
        transform.localScale = Vector3.one;
    }

    [PunRPC]
    public void ChangeParams(string n)
    {
        nombre.text = n;
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
