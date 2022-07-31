using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessageController : MonoBehaviour
{
    public static ChatMessageController _singleton;

    public List<PlayerMessagePrefabController> _chatList;

    private int StorageMessageLimit = 20;

    private void Awake()
    {
        _chatList = new List<PlayerMessagePrefabController>();
       
            if (_singleton == null)
            {
                _singleton = this;
            }
            else if (_singleton != this)
            {
                Destroy(this);
            }
    }

    public void UpdateChatList(List<ChatMessage> messageList)
    {

        for (int i=0; i< messageList.Count;i++)
        {
            ChatMenuViewPortController.UpdateChatViewPort(messageList[i]);
        }
    }

    public void AddToQueue(PlayerMessagePrefabController message)
    {
        if (_chatList.Count == StorageMessageLimit)
        {
            Destroy(_chatList[0].gameObject);
            _chatList.RemoveAt(0);

            _chatList.Add(message);
        }
        else
        {
            _chatList.Add(message);
        }

        Debug.Log(_chatList.Count);
    }


}
