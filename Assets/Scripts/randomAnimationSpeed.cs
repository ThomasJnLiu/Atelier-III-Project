using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomAnimationSpeed : MonoBehaviour
{
    // Start is called before the first frame update
     Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.speed = Random.Range(0.3f,1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
