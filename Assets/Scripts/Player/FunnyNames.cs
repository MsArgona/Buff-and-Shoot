using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class FunnyNames : MonoBehaviourPun, IPunObservable
{
    private static List<Names> names = new List<Names>();

    private static int index;

    private void Awake()
    {
        InitNames();
        DontDestroyOnLoad(gameObject);
    }

    public static string GetRandomName()
    {
        index = Random.Range(0, names.Count - 1);

        while (names[index].IsAlreadyUses)
        {
            index = Random.Range(0, names.Count - 1);
        }

        names[index].IsAlreadyUses = true;

        return names[index].Name;
    }

    //Игрок с этим именем вышел из комнаты
    public static void RemoveName(string name)
    {
        int index = names.FindIndex(x => x.Name == name);
        names[index].IsAlreadyUses = false;
    }

    private void InitNames()
    {
        AddNewName("Obi Wan Kenobi");
        AddNewName("Darth Vader");
        AddNewName("Fluffy Paws");
        AddNewName("Pupsik");
        AddNewName("Kroshka");
        AddNewName("Brutal Macho");
        AddNewName("Zhdun");
        AddNewName("Mad Hamster");
    }

    private void AddNewName(string newName)
    {
        names.Add(new Names(newName, false));
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(index);
            stream.SendNext(names[index].IsAlreadyUses);
        }
        else
        {
            index = (int)stream.ReceiveNext();
            names[index].IsAlreadyUses = (bool)stream.ReceiveNext();
        }
    }
}
