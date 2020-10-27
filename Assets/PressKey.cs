using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PressKey : MonoBehaviour
{
    public GameObject startText;
    private PhotonView pv;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && pv.IsMine)
        {
            startText.SetActive(false);
        }
    }
}
