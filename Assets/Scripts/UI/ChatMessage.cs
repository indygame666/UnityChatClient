using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatMessage 
{
    public string _owner { get; private set; }
    public string _messageText { get; private set; }
    public int _colorID { get; private set; }
    public ChatMessage(string ownerID, string message, int colorID)
    {
        _owner = ownerID;
        _messageText = message;
        _colorID = colorID;
    }
}
