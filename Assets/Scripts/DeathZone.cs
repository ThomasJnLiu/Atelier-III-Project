﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "mainPlayer" || other.gameObject.tag == "target"){
            PlayerController pm = other.gameObject.GetComponent<PlayerController>();
            pm.RespawnPlayer();
            
        }
    }
}
