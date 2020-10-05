using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class paintParticleController : MonoBehaviour
{
    private PhotonView pv;
    public GameObject paintParticles;
    // Start is called before the first frame update
    void Start()
    {
        pv = GetComponent<PhotonView>();
        pv.RPC("hideParticles", RpcTarget.AllBuffered);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)&&pv.IsMine)
        {
            pv.RPC("showParticles", RpcTarget.AllBuffered);
            paintParticles.SetActive(false);
        }
        else if (Input.GetMouseButtonUp(0) && pv.IsMine)
        {
            pv.RPC("hideParticles", RpcTarget.AllBuffered);
        }


        }
    [PunRPC]
    public void hideParticles()
    {
        paintParticles.SetActive(false);
    }
    [PunRPC]
    public void showParticles()
    {
        paintParticles.SetActive(true);
    }
}
