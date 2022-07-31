using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int _id { get; private set; }
    public string _nickname { get; private set; }
    public int _colorID { get; private set; }
    public string _status { get; private set; }

    public Player(int id, string nickname, int colorID, string status)
    {
        _id = id;
        _nickname = nickname;
        _colorID = colorID;
        _status = status;
    }
}
