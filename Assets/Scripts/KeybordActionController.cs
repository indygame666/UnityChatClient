using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeybordActionController : MonoBehaviour
{
    /*  private void Update()
      {
          if (Input.GetKeyDown(KeyCode.Return))
          {
              Debug.Log("Return key was pressed.");
          }

          if (Input.GetKeyDown(KeyCode.Escape))
          {
              Application.Quit();
          }

          if (Input.GetKeyDown(KeyCode.KeypadEnter))
          {
              if (LocalClient._singleton._tcp._clientSocket.Connected)
              {
                  ClientMessageManager._singleton.SendChatMessage();
              }
              else 
              {
                  ConnectionManager._singleton.ConnectToServer();
              }
          }
      }*/

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (LocalClient._singleton._tcp._clientSocket != null)
            {
                ClientMessageManager._singleton.SendChatMessage();
            }
            else
            {
                ConnectionManager._singleton.ConnectToServer();
            }
        }
    }
}
