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
    public GameObject targetText;
    public GameObject timerText;
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
        if(targetText == null){
            targetText = GameObject.FindGameObjectWithTag("minigameText2");
        }
        if(timerText == null){
            timerText = GameObject.FindGameObjectWithTag("minigameText3");
        }
        if(!gameRunning && targetText != null){
            targetText.SetActive(false);
        }
        if(!gameRunning && timerText != null){
            timerText.SetActive(false);
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
            this.photonView.RPC("RPC_GameReady", RpcTarget.All, other.Owner.NickName);
            StartCoroutine("MinigameTimer");
        }
        
    }

    [PunRPC]
    private void RPC_GameReady(string name){
        gameRunning = true;
        minigameText.SetActive(true);
        text = minigameText.GetComponent<TextMeshProUGUI>();
        text.text = ("game has been started! " + name + " is the target!");
        targetText.SetActive(true);
        timerText.SetActive(true);
        targetText.GetComponent<TextMeshProUGUI>().text = ("Target: "+ name);
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
        targetText.SetActive(false);
        timerText.SetActive(false);
    }

    public IEnumerator MinigameTimer(){
        Debug.Log("starting timer");
        for(int i = 90; i >= 0; i--){
            yield return new WaitForSeconds(1f);
            timerText.GetComponent<TextMeshProUGUI>().text = ("Time: "+ i);
            this.photonView.RPC("RPC_UpdateTimer", RpcTarget.All, i);

        }
        GetWinner();
    }

    [PunRPC]
    private void RPC_UpdateTimer(int time){
        timerText.GetComponent<TextMeshProUGUI>().text = ("Time: "+ time);
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
        targetText.GetComponent<TextMeshProUGUI>().text = ("Target: "+ targetName);
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
