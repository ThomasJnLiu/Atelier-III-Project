using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using TMPro;


public class MinigameManager : MonoBehaviourPun
{
    public bool gameRunning = false;
    public static MinigameManager instance;
    public List<GameObject> players = new List<GameObject>();
    public GameObject[] playerNum;
    public GameObject target;
    public GameObject minigameText;
    public TextMeshProUGUI text;
    public PlayerAttributes targetAttributes;
    PhotonView other;
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

        PhotonView photonView = PhotonView.Get(this);
    }

    // Update is called once per frame
    void Update()
    {        
        if(minigameText == null){
            minigameText = GameObject.FindGameObjectWithTag("minigameText");
        }

    }

    public void StartGame(GameObject target){
        if(gameRunning == false){
            other = target.GetComponent<PhotonView>();
            targetAttributes = target.GetComponent<PlayerAttributes>();
            targetAttributes.BecomeTarget();

            //players = GameObject.FindGameObjectsWithTag("Player").ToList();
            // target = PhotonNetwork.PlayerList[0];
            // Debug.Log(target);
            gameRunning = true;
            this.photonView.RPC("RPC_GameReady", RpcTarget.All, "game has been started! " + other.Owner.NickName + " is the target!");
            StartCoroutine("MinigameTimer");
        }
        
    }

    [PunRPC]
    private void RPC_GameReady(string message){
        gameRunning = true;
        minigameText.SetActive(true);
        text = minigameText.GetComponent<TextMeshProUGUI>();
        text.text = (message);
        StartCoroutine("HideTextTimer");
    }

    [PunRPC]
    private void RPC_GameEnd(string winnerName){
        gameRunning = false;
        Debug.Log("(RPC) game is ending...");
        minigameText.SetActive(true);
        text = minigameText.GetComponent<TextMeshProUGUI>();
        text.text = (winnerName + " is the winner!");
        StartCoroutine("HideTextTimer");

    }

    public IEnumerator MinigameTimer(){
        Debug.Log("starting timer");
        yield return new WaitForSeconds(30f);
        GetWinner();
    }

    public void UpdateTarget(GameObject target){
        other = target.GetComponent<PhotonView>();
        targetAttributes = target.GetComponent<PlayerAttributes>();
        targetAttributes.BecomeTarget();
        this.photonView.RPC("RPC_UpdateTarget", RpcTarget.All, other.Owner.NickName);
    }

    public void GetWinner(){
        GameObject winner = GameObject.FindGameObjectWithTag("target");
        winner.GetComponent<PlayerAttributes>().NotTarget();
        playerNum = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject p in playerNum){
            p.GetComponent<PlayerAttributes>().NotTarget();
        }
        GameObject.FindGameObjectWithTag("mainPlayer").GetComponent<PlayerAttributes>().NotTarget();
        this.photonView.RPC("RPC_GameEnd", RpcTarget.All, winner.GetComponent<PhotonView>().Owner.NickName);
    }
    [PunRPC]
    private void RPC_UpdateTarget(string targetName){
        Debug.Log(targetName + " is the new target!");
    }

    public IEnumerator HideTextTimer(){
        yield return new WaitForSeconds(3f);
        this.photonView.RPC("RPC_HideText", RpcTarget.All);
    }

    [PunRPC]
    private void RPC_HideText(){
        minigameText.SetActive(false);
    }
}
