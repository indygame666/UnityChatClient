using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerListPrefabController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _idText;
    [SerializeField] private TextMeshProUGUI _nicknameText;
    [SerializeField] private TextMeshProUGUI _onlineStatusText;

    [SerializeField] Color _onlineStatusColor;
    [SerializeField] Color _offlineStatusColor;
    public void InitPrefabValues(int _id,string nickName, int colorID, string status)
    {
        ///Nickname properties
        _idText.text = $"[{_id}]" ;
        _nicknameText.text = nickName; 
        _nicknameText.color = ColorPanelManager._singleton._colorList[colorID]._color;


        if (String.Equals(status, "(online)") == true)
        {
            _onlineStatusText.color = _onlineStatusColor;
        }
        else
        {
            _onlineStatusText.color = _offlineStatusColor;
        }

        _onlineStatusText.text = status;
  
    }


}
