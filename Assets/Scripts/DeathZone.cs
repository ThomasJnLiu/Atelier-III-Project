using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player"){
            Debug.Log("collided");
            PlayerController pm = other.gameObject.GetComponent<PlayerController>();
            pm.RespawnPlayer();
            
        }
    }
}
