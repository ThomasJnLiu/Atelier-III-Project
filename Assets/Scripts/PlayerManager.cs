using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake(){
        PV = GetComponent<PhotonView>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if(PV.IsMine){
            CreateController();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateController(){
        //Instantiate player controller
        Debug.Log("instatiate player");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerController"), Vector3.zero, Quaternion.identity);
    }

    public void RespawnPlayer(){
        transform.position = Vector3.zero;
    }
}
