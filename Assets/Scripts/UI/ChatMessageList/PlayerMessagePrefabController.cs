using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMessagePrefabController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _chatMessageText;

    public void InitPrefabValues(ChatMessage message)
    {
        ///Nickname properties
        _nicknameText.text = message._owner + ":";
        _nicknameText.color = ColorPanelManager._singleton._colorList[message._colorID]._color;
        ///_chatMessage properties
        _chatMessageText.text = message._messageText;
        _chatMessageText.color = ColorPanelManager._singleton._colorList[message._colorID]._color;
    }
}
