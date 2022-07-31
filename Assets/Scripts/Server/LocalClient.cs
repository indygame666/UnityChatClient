using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class LocalClient : MonoBehaviour
{
    public static LocalClient _singleton;
    public static int _bufferDataSize = 4096;

    public TMPro.TMP_InputField _inputNicknameField;

    public string _nickname { get; private set; }
    public int _colorID { get; private set; }


    public string _ip = "127.0.0.1";
    public int _port = 777;
    public int _id { get; private set; }
    
    public TCP _tcp;
    private bool _isConnected = false;

    private delegate void PacketHandler(Packet packet);
    private static Dictionary<int, PacketHandler> packetHandler;

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

    void Start()
    {
        ///
        _colorID = 0;
        _tcp = new TCP();
    }

    private void OnApplicationQuit()
    {
        Disconnect();
    }

    #endregion

    public void AssignNickName()
    {
        _nickname = _inputNicknameField.text;
    }

    public void SetColorID(int id)
    {
        _colorID = id;
    }

    private void Disconnect()
    {
        if (_isConnected == true)
        {
            _isConnected = false;
            _tcp._clientSocket.Close();

            Debug.Log("Disconnected from server");
        }
    }

    public void SetID(int id)
    {
        _id = id;
    }

    public bool ConnectToServer()
    {
        AssignNickName();

        if (_nickname.Length < 3)
        {
            return false;
        }
        
        _isConnected = true;
        InitClientData();
        _tcp.Connect();
        return true;
    }


    public class TCP
    {
        public TcpClient _clientSocket;
        private NetworkStream _stream;
        private Packet _data;
        private byte[] _buffer;

        public void Connect()
        {
            _clientSocket = new TcpClient
            {
                ReceiveBufferSize = _bufferDataSize,
                SendBufferSize = _bufferDataSize

            };

            _buffer = new byte[_bufferDataSize];
            _clientSocket.BeginConnect(_singleton._ip, _singleton._port, ConnectCallback, _clientSocket);

        }

        private void ConnectCallback(IAsyncResult result)
        {
            _clientSocket.EndConnect(result);

            if (!_clientSocket.Connected)
            {
                return;
            }

            _stream = _clientSocket.GetStream();

            _data = new Packet();

            _stream.BeginRead(_buffer, 0, _bufferDataSize, ReceiveCallback,null);
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                int byteLength = _stream.EndRead(result);
                if (byteLength <= 0)
                {
                    _singleton.Disconnect();
                    return;
                }
                byte[] byteData = new byte[byteLength];
                Array.Copy(_buffer, byteData, byteLength);

                this._data.Reset(HandleData(byteData));

                _stream.BeginRead(_buffer, 0, _bufferDataSize, ReceiveCallback, null);
            }
            catch (Exception e)
            {
                Disconnect();
            }
        }

        private bool HandleData(byte[] _data)
        {
            int _packetLength = 0;

            this._data.SetBytes(_data);

            if (this._data.UnreadLength() >= 4)
            {
                _packetLength = this._data.ReadInt();
                if (_packetLength <= 0)
                {
                    return true;
                }
            }

            while (_packetLength > 0 && _packetLength <= this._data.UnreadLength())
            {
                byte[] _packetBytes = this._data.ReadBytes(_packetLength);
                ThreadManager.ExecuteOnMainThread(() =>
                {
                    using (Packet _packet = new Packet(_packetBytes))
                    {
                        int _packetId = _packet.ReadInt();
                        packetHandler[_packetId](_packet);
                    }
                });

                _packetLength = 0;
                if (this._data.UnreadLength() >= 4)
                {
                    _packetLength = this._data.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }
            }

            if (_packetLength <= 1)
            {
                return true;
            }

            return false;
        }

        public void SendData(Packet packet)
        {
            try
            { 
                if (_clientSocket !=null)
                {
                    _stream.BeginWrite(packet.ToArray(), 0, packet.Length(), null, null); ;
                }
            }
            catch(Exception e)
            {
                Debug.Log($"Exception: Can't send data to server: {e}");
            }
        }

        public void Disconnect()
        {
            {
                _singleton.Disconnect();
                _stream = null;
                _buffer = null;
                _data = null;
            }
        }
    }
    
    private void InitClientData()
    {
        packetHandler = new Dictionary<int, PacketHandler>()
        {
            {
                (int)ServerPackets.welcome, ClientMessageManager.WelcomeReceived
            },
            {
                (int)ServerPackets.chat,ClientMessageManager.UpdateMessage
            },
            {
                (int)ServerPackets.clientList,ClientMessageManager.UpdateClientList
            },
            
            {
                (int)ServerPackets.viewData,ClientMessageManager.UpdateViewData
            },
            {
                (int)ServerPackets.serverMessage,ClientMessageManager.ReceivedServerMessage
            }


        };
        Debug.Log("Initialized packets");
    }
}
