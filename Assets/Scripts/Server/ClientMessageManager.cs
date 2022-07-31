using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ClientMessageManager : MonoBehaviour
{

    public static ClientMessageManager _singleton;
    public TMP_InputField _messageTextInputField;

    public event Action OnWelcomeReceived;
    public event Action OnMessageReceived;
        

    #region UNITY_LIFECYCLE
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
    #endregion

    #region RECIEVED_PACKETS
    public static void WelcomeReceived(Packet _packet)
    {
        string message = _packet.ReadString();
        int Id = _packet.ReadInt();
        Debug.Log($"{message}, ID: {Id} ");
        ///Finishing welcome steps
        LocalClient._singleton.SetID(Id);
        WelcomeMessage();
        _singleton.OnWelcomeReceived?.Invoke();
    }

    public static void ReceivedServerMessage(Packet _packet)
    {
        string message = _packet.ReadString();

        Debug.Log(message);

        ChatMenuViewPortController.UpdateChatViewPort(message);

    }

    public static void UpdateViewData(Packet _packet)
    {

        ///ReadingChatList
        List<ChatMessage> serverMessageList = _packet.ReadMessageList();

        ///Init currentChatList
        ChatMessageController._singleton.UpdateChatList(serverMessageList);

        ////Init currentPlayerList
        List<Player> serverPlayerList = _packet.ReadDictionary();
        PlayerListController._singleton.UpdateChatDictionary(serverPlayerList);
    }

    public static void UpdateMessage(Packet _packet)
    {
        string nickName = _packet.ReadString();
        int colorID = _packet.ReadInt();
        string message = _packet.ReadString();
        Debug.Log($"{message}, Nickname: {nickName} ");

        ChatMessage recievedMessage = new ChatMessage(nickName, message, colorID);

        ChatMenuViewPortController.UpdateChatViewPort(recievedMessage);
        _singleton.OnMessageReceived?.Invoke();
    }


    public static void UpdateClientList(Packet _packet)
    {

        int id = _packet.ReadInt();
        string nickname = _packet.ReadString();
        int colorID = _packet.ReadInt();
        string status =_packet.ReadString();


        Player recievedPlayer = new Player(id, nickname, colorID, status);

        PlayerListController._singleton.UpdatePlayerStatus(recievedPlayer);

    }
    #endregion

    #region PACKETS_TO_SEND
    public static void WelcomeMessage()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {

            _packet.Write(LocalClient._singleton._id);
            _packet.Write(LocalClient._singleton._colorID);
            _packet.Write(LocalClient._singleton._inputNicknameField.text);
           

            SendTCPData(_packet);
        }
    }

    public void SendChatMessage()
    {
        using (Packet _packet = new Packet((int)ClientPackets.chatMessage))
        {
            _packet.Write(LocalClient._singleton._nickname);
            _packet.Write(LocalClient._singleton._colorID);
            _packet.Write(ClientMessageManager._singleton._messageTextInputField.text);

            SendTCPData(_packet);
        }
    }
    #endregion

    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        LocalClient._singleton._tcp.SendData(_packet);
    }
}
