using System;
using UnityEngine;
using UnityEngine.UI;

public class OpacityController : MonoBehaviour
{
    [SerializeField] private GameObject _dialogueWindow;
    [SerializeField] private UnityEngine.UI.Slider _slider;

    public void EditOpacity()
    {
        float r = _dialogueWindow.GetComponent<Image>().color.r;
        float g = _dialogueWindow.GetComponent<Image>().color.g;
        float b = _dialogueWindow.GetComponent<Image>().color.b;
        
        _dialogueWindow.GetComponent<Image>().color = new Color(r, g, b, _slider.value);
    }
}
