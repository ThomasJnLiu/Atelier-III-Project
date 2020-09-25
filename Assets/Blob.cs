using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Blob : MonoBehaviour
{
    public float blobSize=0.05f;
    public bool destroyBlob = false;
    public float blobLifetime=0.0f;

    public PhotonView photonView;
    
    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        gameObject.name = photonView.Owner.ToString(); 
        transform.localScale = Vector3.one * blobSize;

        

        if (destroyBlob)
        {
            Destroy(gameObject, blobLifetime);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Destroy(gameObject);
        }
        
        //photonView.RPC("RpcMethodName", photonView.Owner);
        
    }
}
