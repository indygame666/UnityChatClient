using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AdminMessagePrefabController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    public void InitPrefabValues (string message)
    {
        _textMeshPro.text = message;
    }
}
