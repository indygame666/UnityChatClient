using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConnectionManager : MonoBehaviour
{
    public static ConnectionManager _singleton;
    [SerializeField] GameObject _chatMenu;
    [SerializeField] GameObject _startMenu;
    [SerializeField] GameObject _loadingScreenMenu;
    [SerializeField] GameObject _nicknameErrorPanel;

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

    public void ConnectToServer()
    {
     
        bool isConnecting = LocalClient._singleton.ConnectToServer();
        if (isConnecting == true)
        {
            _startMenu.SetActive(false);
            _loadingScreenMenu.SetActive(true);
            ClientMessageManager._singleton.OnWelcomeReceived += ToggleLoadingScreen;
        }
        else
        {
            _nicknameErrorPanel.SetActive(true);
        }
    }

    private void ToggleLoadingScreen()
    {
        _loadingScreenMenu.SetActive(false);
        _chatMenu.SetActive(true);
        ClientMessageManager._singleton.OnWelcomeReceived -= ToggleLoadingScreen;
    }

}
