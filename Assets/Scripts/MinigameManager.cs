using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;



public class MinigameManager : MonoBehaviourPun
{
    public bool gameRunning = false;
    public static MinigameManager instance;
    public List<GameObject> players = new List<GameObject>();
    public GameObject[] playerNum;
    public GameObject target;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            // A unique case where the Singleton exists but not in this scene
            if (instance.gameObject.scene.name == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame(){
        if(gameRunning == false){
            players = GameObject.FindGameObjectsWithTag("Player").ToList();
            players.Add(GameObject.FindGameObjectWithTag("mainPlayer"));
            Debug.Log(PhotonNetwork.PlayerList[0]);
            Debug.Log(PhotonNetwork.PlayerList[0].NickName);
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
            // target = PhotonNetwork.PlayerList[0];
            // Debug.Log(target);
            
            gameRunning = true;
            Debug.Log("game starting");
        }
        
    }
}
