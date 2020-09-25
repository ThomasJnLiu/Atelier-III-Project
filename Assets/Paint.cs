using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Paint : MonoBehaviour
{
    public Camera playerCamera;
    public GameObject paintBlob;
    public GameObject Plane;
    public PhotonView pv;
    List<GameObject> goList;
    public float paintDistance = 5.0f;
    Vector3 blobExtrude;
    // Start is called before the first frame update
    void Start()
    {
        goList = new List<GameObject>();
        playerCamera = this.GetComponentInChildren<Camera>();
        Plane = GameObject.Find("Plane");
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.X))
        {
            PhotonNetwork.Destroy(goList[goList.Count - 1].gameObject);
            goList.RemoveAt(goList.Count - 1);
            Debug.Log(goList.Count);
            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {

                if (gameObj.name == pv.Owner.ToString())
                {


                    

                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            var Ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(Ray, out hit,paintDistance))
            {
                blobExtrude = Vector3.up* 0.1f;

                if (hit.transform.gameObject.tag == "wall_north")
                {
                    blobExtrude = Vector3.forward * 0.1f;
                }
                else if (hit.transform.gameObject.tag == "wall_south")
                {
                    blobExtrude = Vector3.back * 0.1f;
                }
                else if (hit.transform.gameObject.tag == "wall_east")
                {
                    blobExtrude = Vector3.right * 0.1f;
                }
                else if (hit.transform.gameObject.tag == "wall_west")
                {
                    blobExtrude = Vector3.left * 0.1f;
                }
                else if (hit.transform.gameObject.tag == "floor")
                {
                    blobExtrude = Vector3.up * 0.1f;
                }
                else
                {
                    blobExtrude = new Vector3(0, 0, 0);
                }
                if (hit.point != null)
                {
                    var go = PhotonNetwork.Instantiate("paintBlob", hit.point + blobExtrude, hit.transform.rotation);
                    goList.Add(go);
                    //go.transform.localScale = (Vector3.one /2) / (hit.distance*4);
                    

                }
                


            }
        }
    }
}
