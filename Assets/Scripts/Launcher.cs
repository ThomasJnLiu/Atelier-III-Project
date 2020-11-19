using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Launcher : MonoBehaviourPunCallbacks
{
  public static Launcher Instance;

  void Awake()
  {
    Instance = this;
  }
  [SerializeField] TMP_InputField roomNameInputField;
  [SerializeField] TMP_Text errorText;
  [SerializeField] TMP_Text roomNameText;
  [SerializeField] Transform roomListContent;
  [SerializeField] GameObject roomListItemPrefab;
  [SerializeField] Transform playerListContent;
  [SerializeField] GameObject playerListItemPrefab;
  [SerializeField] GameObject startGameButton;
  [SerializeField] TMP_InputField nameInputField;

  // Start is called before the first frame update
  void Start()
  {

  }

  public void ConnectToLobby(){
    if (string.IsNullOrEmpty(nameInputField.text))
    {
      return;
    }

    PhotonNetwork.NickName = nameInputField.text;

    MenuManager.Instance.OpenMenu("Loading");

    Debug.Log("Connecting to Master");

    PhotonNetwork.ConnectUsingSettings();
  }
  public override void OnConnectedToMaster()
  {
    Debug.Log("Connected to Master");

    PhotonNetwork.JoinLobby();
    PhotonNetwork.AutomaticallySyncScene = true;
  }

  public override void OnJoinedLobby()
  {
    MenuManager.Instance.OpenMenu("Title");
    Debug.Log("Joined Lobby");
    // PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
  }

  public void CreateRoom()
  {
    if (string.IsNullOrEmpty(roomNameInputField.text))
    {
      return;
    }
    MenuManager.Instance.OpenMenu("Loading");
    PhotonNetwork.CreateRoom(roomNameInputField.text);
  }

  public override void OnJoinedRoom()
  {
    Debug.Log("joined Room " + PhotonNetwork.CurrentRoom.Name);
    MenuManager.Instance.OpenMenu("Room");
    roomNameText.text = PhotonNetwork.CurrentRoom.Name;

    Player[] players = PhotonNetwork.PlayerList;

    //delete all player list items
    foreach (Transform child in playerListContent)
    {
      Destroy(child.gameObject);
    }

    for (int i = 0; i < players.Count(); i++)
    {
      Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
    }

    startGameButton.SetActive(PhotonNetwork.IsMasterClient);
  }

  public override void OnMasterClientSwitched(Player newMasterClient)
  {
    startGameButton.SetActive(PhotonNetwork.IsMasterClient);
  }

  public override void OnCreateRoomFailed(short returncode, string message)
  {
    MenuManager.Instance.OpenMenu("Error");
    errorText.text = "Room Creation Failed " + message;

  }

  public void LeaveRoom()
  {
    PhotonNetwork.LeaveRoom();
    MenuManager.Instance.OpenMenu("Loading");

  }

  public override void OnLeftRoom()
  {
    MenuManager.Instance.OpenMenu("Title");
  }

  public override void OnRoomListUpdate(List<RoomInfo> roomList)
  {
    foreach (Transform trans in roomListContent)
    {
      Destroy(trans.gameObject);
    }
    for (int i = 0; i < roomList.Count; i++)
    {
      if (roomList[i].RemovedFromList)
      {
        continue;
      }
      Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
    }
  }

  public void JoinRoom(RoomInfo info)
  {
    PhotonNetwork.JoinRoom(info.Name);
    MenuManager.Instance.OpenMenu("Loading");
  }

  public override void OnPlayerEnteredRoom(Player newPlayer)
  {
    Instantiate(playerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);

  }

  public void StartGame()
  {
    PhotonNetwork.LoadLevel(1);

  }

}
