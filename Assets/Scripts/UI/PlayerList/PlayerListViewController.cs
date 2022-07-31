using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerListViewController : MonoBehaviour
{
    public static PlayerListViewController _singleton;

    [SerializeField] GameObject _contentGameObject;
    [SerializeField] GameObject _chatMessagePrefab;

    private void Awake()
    {
        if (_singleton == null)
        {
            _singleton = this;
        }
        else if (_singleton != this)
        {
            Destroy(this);
        }

    }

    public PlayerListPrefabController UpdateChatViewPort(Player player)
    {
        GameObject playerGameObject = Instantiate(_singleton._chatMessagePrefab, _singleton._contentGameObject.transform);
        PlayerListPrefabController prefabController = playerGameObject.GetComponent<PlayerListPrefabController>();

        if (prefabController != null)
        {
            prefabController.InitPrefabValues(player._id,player._nickname, player._colorID, player._status);
        }

        return prefabController;
    }

}
