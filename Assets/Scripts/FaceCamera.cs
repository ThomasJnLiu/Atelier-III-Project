using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour
{
    [SerializeField] Transform mainCameraTransform;
    [SerializeField] GameObject cameraObject;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.parent.tag == "mainPlayer"){
            cameraObject.tag = "MainCamera";
        }
        mainCameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate(){
        transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
    }
}
