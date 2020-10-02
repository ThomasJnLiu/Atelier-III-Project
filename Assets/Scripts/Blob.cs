using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Blob : MonoBehaviour
{
    public float blobSize=0.05f;

    public PhotonView photonView;
    
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        gameObject.name = photonView.Owner.ToString(); 
        transform.localScale = Vector3.one * blobSize;

        

        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        //photonView.RPC("RpcMethodName", photonView.Owner);
        
    }
}
