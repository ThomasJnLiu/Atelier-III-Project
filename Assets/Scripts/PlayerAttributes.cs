using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAttributes : MonoBehaviour
{

    public int selectedColor=0;

    public GameObject sprayModel;

    public Material[] sprayColor;

    public ParticleSystem paintParticle;

    private PhotonView pv;
    // Start is called before the first frame update

    void Start()
    {

        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            sprayModel.SetActive(false);
        }
        ParticleSystem.MainModule ma = paintParticle.main;
        ma.startColor = sprayColor[0].color;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Alpha1) &&pv.IsMine)
        {
                pv.RPC("red", RpcTarget.AllBuffered);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && pv.IsMine)
        {
            pv.RPC("blue", RpcTarget.AllBuffered);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && pv.IsMine)
        {
            pv.RPC("yellow", RpcTarget.AllBuffered);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && pv.IsMine)
        {
            pv.RPC("green", RpcTarget.AllBuffered);

        }
    }
    [PunRPC]
    public void red()
    {
        sprayModel.GetComponent<Renderer>().materials[1].color = sprayColor[0].color;
        ParticleSystem.MainModule ma = paintParticle.main;
        ma.startColor = sprayColor[0].color;
        selectedColor = 0;
    }
    [PunRPC]
    public void blue()
    {
        sprayModel.GetComponent<Renderer>().materials[1].color = sprayColor[1].color;
        ParticleSystem.MainModule ma = paintParticle.main;
        ma.startColor = sprayColor[1].color;
        selectedColor = 1;
    }
    [PunRPC]
    public void yellow()
    {
        sprayModel.GetComponent<Renderer>().materials[1].color = sprayColor[2].color;
        ParticleSystem.MainModule ma = paintParticle.main;
        ma.startColor = sprayColor[2].color;
        selectedColor = 2;
    }
    [PunRPC]
    public void green()
    {
        sprayModel.GetComponent<Renderer>().materials[1].color = sprayColor[3].color;
        ParticleSystem.MainModule ma = paintParticle.main;
        ma.startColor = sprayColor[3].color;
        selectedColor = 3;
    }
}
