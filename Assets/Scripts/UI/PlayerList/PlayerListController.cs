using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListController : MonoBehaviour
{
    public static PlayerListController _singleton;

    public Dictionary<int,PlayerListPrefabController> _playerDictionary;

    private void Awake()
    {
        _playerDictionary = new Dictionary<int, PlayerListPrefabController>();

        if (_singleton == null)
        {
            _singleton = this;
        }
        else if (_singleton != this)
        {
            Destroy(this);
        }
    }

    public void UpdateChatDictionary(List<Player> serverPlayerList)
    {
        
        for (int i = 0; i < serverPlayerList.Count; i++)
        {
            Debug.Log("ID:" + serverPlayerList[i]._id);
            if (_playerDictionary.ContainsKey(serverPlayerList[i]._id) == false)
            {
                ///instantiate playerPrefabItem in ScrollView
                PlayerListPrefabController playerGameObject =  PlayerListViewController._singleton.UpdateChatViewPort(serverPlayerList[i]);
                if (playerGameObject != null)
                {
                    _playerDictionary.Add(serverPlayerList[i]._id, playerGameObject);
                }
                
            }
        }
    }

    public void UpdatePlayerStatus(Player receivedPlayer)
    {
        if (_playerDictionary.ContainsKey(receivedPlayer._id) == true)
        {
            
            _playerDictionary[receivedPlayer._id].InitPrefabValues(receivedPlayer._id,receivedPlayer._nickname, receivedPlayer._colorID, receivedPlayer._status);
        }
        else
        {
            PlayerListPrefabController playerGameObject = PlayerListViewController._singleton.UpdateChatViewPort(receivedPlayer);
            _playerDictionary.Add(receivedPlayer._id, playerGameObject);
        }
    }

}
