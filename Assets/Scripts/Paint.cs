using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class Paint : MonoBehaviour
{
  public Camera playerCamera;

  public GameObject Plane;
  public PhotonView pv;
  public List<GameObject> goList;
  public float paintDistance = 5.0f;
  Vector3 blobExtrude;
  public GameObject paintParticles;
  public GameObject sprayPoint;

  public int selectedColor;
  public Image fill;
  public Image outline;

  public GameObject undoText;
  Vector3 screenCenter = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        goList = new List<GameObject>();

        Plane = GameObject.Find("Plane");
        pv = GetComponent<PhotonView>();
        if (pv.IsMine)
        {
            playerCamera = this.GetComponentInChildren<Camera>();
        }
    }

  // Update is called once per frame
  void Update()
  {
        // Draw ray from center of screen for debugging
        if (pv.IsMine)
        {
            Ray playerRay = playerCamera.ScreenPointToRay(screenCenter);
            Debug.DrawRay(playerRay.origin, playerRay.direction * 10, Color.red);
        }
    // check if player belongs to client
    if (!pv.IsMine)
    {
      return;
    }
    if (Input.GetKey(KeyCode.X) && goList.Count > 0 && pv.IsMine)
    {
      PhotonNetwork.Destroy(goList[goList.Count - 1].gameObject);
      goList.RemoveAt(goList.Count - 1);
    }
    if (Input.GetKeyDown(KeyCode.Z) && goList.Count > 0 && pv.IsMine)
    {
      //for (int i = goList.Count - 1; i >= 0; i--)
      //{
      PhotonNetwork.Destroy(goList[goList.Count - 1].gameObject);
      goList.RemoveAt(goList.Count - 1);
      //}
      //foreach (GameObject blob in goList)
      //{
      //    PhotonNetwork.Destroy(blob);

      //}
      //goList.Clear();
    }

    if (Input.GetMouseButton(0) && goList.Count < 1980 &&pv.IsMine)
    {
       
      var Ray = playerCamera.ScreenPointToRay(screenCenter);
      RaycastHit hit;

      if (Physics.Raycast(Ray, out hit, paintDistance))
      {
        blobExtrude = Vector3.up * 0.1f;

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
        if (hit.point != null && goList.Count >= 0 && hit.transform.gameObject.tag!="Untagged")
        {

          int blobColor = GetComponent<PlayerAttributes>().selectedColor;
          if (blobColor == 0)
          {
            var go = PhotonNetwork.Instantiate("blueBlob", hit.point + blobExtrude, hit.transform.rotation);
            goList.Add(go);
          }
          else if (blobColor == 1)
          {
            var go = PhotonNetwork.Instantiate("redBlob", hit.point + blobExtrude, hit.transform.rotation);
            goList.Add(go);
          }
          else if (blobColor == 2)
          {
            var go = PhotonNetwork.Instantiate("yellowBlob", hit.point + blobExtrude, hit.transform.rotation);
            goList.Add(go);
          }
          else if (blobColor == 3)
          {
            var go = PhotonNetwork.Instantiate("greenBlob", hit.point + blobExtrude, hit.transform.rotation);
            goList.Add(go);
          }


          //go.transform.localScale = (Vector3.one /2) / (hit.distance*4);
        }
      }
    }
    //if (Input.GetMouseButtonDown(0))
    //{
    //  fill.color = new Color32(0, 127, 225, 255);
    //  outline.color = new Color32(0, 0, 0, 255);
    //}
    //if (Input.GetMouseButtonUp(0))
    //{
    //  fill.color = new Color32(0, 127, 225, 190);
    //  outline.color = new Color32(0, 0, 0, 190);

    //}
    if (goList.Count > 1500)
    {
      undoText.SetActive(true);
    }
    else
    {
      undoText.SetActive(false);
    }

  }

}
