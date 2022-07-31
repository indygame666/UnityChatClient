using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMenuViewPortController : MonoBehaviour
{
    public static ChatMenuViewPortController _singleton;

    [SerializeField] GameObject _contentGameObject;
    [SerializeField] GameObject _playerMessagePrefab;
    [SerializeField] GameObject _adminMessagePrefab;

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

    public static void UpdateChatViewPort(ChatMessage message)
    {
        GameObject chatMessage = Instantiate(_singleton._playerMessagePrefab, _singleton._contentGameObject.transform);
        PlayerMessagePrefabController prefabController = chatMessage.GetComponent<PlayerMessagePrefabController>();

        if (prefabController != null)
        {
            prefabController.InitPrefabValues(message);
            ChatMessageController._singleton.AddToQueue(prefabController);
        }
    }

    public static void UpdateChatViewPort(string message)
    {
        GameObject chatMessage = Instantiate(_singleton._adminMessagePrefab, _singleton._contentGameObject.transform);
        AdminMessagePrefabController prefabController = chatMessage.GetComponent<AdminMessagePrefabController>();

        if(prefabController !=null)
        {
            prefabController.InitPrefabValues(message);
        }
        
    }


}
