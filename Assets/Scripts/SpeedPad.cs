﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "mainPlayer" || other.gameObject.tag == "target"){
            PlayerController pm = other.gameObject.GetComponent<PlayerController>();
            pm.SpeedPadBoost();
        }
    }
}
