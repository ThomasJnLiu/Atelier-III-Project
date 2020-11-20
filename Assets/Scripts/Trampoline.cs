using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
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
        if(other.gameObject.tag == "Player" && other.relativeVelocity.magnitude > 2){
            Debug.Log(other.relativeVelocity.magnitude);
            // Cap max velocity from trampoline at 18
            if(other.relativeVelocity.magnitude*1.5f < 18){
                other.rigidbody.AddForce(0, other.relativeVelocity.magnitude*1.5f, 0, ForceMode.Impulse);
            }else{
                other.rigidbody.AddForce(0, 18, 0, ForceMode.Impulse);
            }
            
        }
    }
}
