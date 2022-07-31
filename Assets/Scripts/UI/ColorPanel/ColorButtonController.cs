using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorButtonController : MonoBehaviour
{
    [HideInInspector]public int _id = 0; 
    [HideInInspector]public Color _color = Color.black;
    [SerializeField] private Image _targetImage; 

    private void Start()
    {
        _targetImage.color = _color;
    }

    public void SelectColor()
    {
        LocalClient._singleton.SetColorID(_id);
    }


}
