using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoomItem : MonoBehaviour
{
    [SerializeField] private TMP_Text roomName;
    MenuManager menuManager;

    //private void Awake()
    //{
    //    roomName = GetComponentInChildren<TextMeshPro>();
    //}

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void SetRoomName(string name)
    {
        roomName.text = name;
    }

    public void OnClickItem()
    {
        menuManager.JoinRoom(roomName.text);
    }
}
