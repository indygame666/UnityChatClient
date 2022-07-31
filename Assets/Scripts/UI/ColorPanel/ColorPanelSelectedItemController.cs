using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ColorPanelSelectedItemController : MonoBehaviour
{
    private Button[] _buttons;

    public void Init(Button[] colorButtonArray)
    {
        _buttons = colorButtonArray;

        foreach (Button button in _buttons)
        {
            button.onClick.AddListener(() => OnButtonClick(button));
        }

        _buttons[0].interactable = false;

        Debug.Log("Init completed");

    }

    public void SetAllButtonsInteractable()
    {
        foreach (Button button in _buttons)
        {
            button.interactable = true;
        }
    }

    private void OnButtonClick(Button clickedButton)
    {
        SetAllButtonsInteractable();
        clickedButton.interactable = false;
    }
}
