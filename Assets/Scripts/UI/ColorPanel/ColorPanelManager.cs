using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChatColor 
{
    public int _id;
    public Color _color;
}

public class ColorPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject _panelParentObject;
    [SerializeField] private Button _colorPrefab;

    public static ColorPanelManager _singleton;
    public ChatColor[] _colorList;

    [SerializeField ]private ColorPanelSelectedItemController _colorPanelSelectedItemController;

    private void Awake()
    {
        if (_colorList !=null)
        {
            Button[] buttonArray = new Button[_colorList.Length];

            for (int i =0; i<_colorList.Length; i++)
            {
                Button button = Instantiate(_colorPrefab, _panelParentObject.transform);
                ColorButtonController colorButton = button.GetComponent<ColorButtonController>();
             
                if (colorButton !=null)
                {
                    colorButton._id = _colorList[i]._id;
                    colorButton._color = _colorList[i]._color;
                }

                buttonArray[i] = button;
            }

            _colorPanelSelectedItemController.Init(buttonArray);
        }

            if (_singleton == null)
            {
                _singleton = this;
            }
            else if (_singleton != this)
            {
                Destroy(this);
            }
       
    }
}
