using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Paint : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject paintBlob;
    public GameObject Plane;
    public float blobSize;
    // Start is called before the first frame update
    void Start()
    {
        playerCamera= this.GetComponentInChildren<Camera>();
        Plane = GameObject.Find("Plane");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            var Ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(Ray, out hit))
            {
                var go = PhotonNetwork.Instantiate("paintBlob", hit.point + Vector3.up * 0.1f, Quaternion.identity);
                go.transform.localScale = Vector3.one * blobSize;
            }
        }
    }
}
