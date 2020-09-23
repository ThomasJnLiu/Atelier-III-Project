using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    public float blobSize=0.05f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.one * blobSize;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
