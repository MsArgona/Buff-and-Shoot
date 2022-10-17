using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuManager : MonoBehaviourPunCallbacks
{
    [Tooltip("Name of the new room")]
    [SerializeField] private TMP_InputField createInput;

    [Tooltip("The name of the room to join")]
    [SerializeField] private TMP_InputField joinInput;

    [Tooltip("The maximum number of players in the scene")]
    [SerializeField] private int MaxPlayerInput = 4; //доделать

    [SerializeField] private Transform contentObj;
    [SerializeField] private RoomItem roomItemPrefab;
    private List<RoomItem> roomItemsList = new List<RoomItem>();

    private string playerName;

    private List<string> rooms = new List<string>();

    private float timeBtwUpdates = 1.5f;
    private float nextUpdateTime;

    private void Start()
    {
        //PhotonNetwork.JoinLobby(); //удалить
        //PhotonNetwork.ConnectUsingSettings();
        playerName = FunnyNames.GetRandomName();
        PhotonNetwork.NickName = playerName;
    }

    public void CreateRoom()
    {
        //if (rooms.Exists(x => string.Equals(x, createInput.text, System.StringComparison.OrdinalIgnoreCase)))
        //{
        //    Debug.Log(" омната с таким именем уже существует");
        //}
        //else
        if (createInput.text == "")
            return;

        rooms.Add(createInput.text);

        var roomOptions = new RoomOptions
        {
            MaxPlayers = (byte)MaxPlayerInput
        };

        PhotonNetwork.CreateRoom(createInput.text.ToLower(), roomOptions);

    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);

        ////ошибка, т.к.он смотрит локально, а не на сервере
        //if (rooms.Exists(x => string.Equals(x, joinInput.text, System.StringComparison.OrdinalIgnoreCase)))
        //{
        //    PhotonNetwork.JoinRoom(joinInput.text.ToLower());
        //}
        //else
        //{
        //    Debug.Log(" омнаты с таким именем Ќ≈ существует");
        //}
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if (Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBtwUpdates;
        }
    }

    // огда список комнат обновилс€, мы в меню тоже обновл€ем все комнаты
    private void UpdateRoomList(List<RoomInfo> roomList)
    {
        DestroyItems(); //очищаем список
        RepopulateItems(roomList); //заполн€ем обновленными комнатами список
    }

    private void DestroyItems()
    {
        foreach (var item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();
    }

    private void RepopulateItems(List<RoomInfo> list)
    {
        foreach (var room in list)
        {
            RoomItem newRoom = Instantiate(roomItemPrefab, contentObj);
            newRoom.SetRoomName(room.Name);
            roomItemsList.Add(newRoom);
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public void OnExitGame()
    {
        Application.Quit();
    }
}
