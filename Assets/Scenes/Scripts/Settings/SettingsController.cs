using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private GameObject _settingsWindow;
    [SerializeField] private GameObject _dialogueWindow;
    
    [SerializeField] private TextMeshProUGUI _errorText;
    
    [SerializeField] private TMP_InputField _redField;
    [SerializeField] private TMP_InputField _greenField;
    [SerializeField] private TMP_InputField _blueField;
    
    [SerializeField] private TMP_InputField _fontSizeField;
    [SerializeField] private TextMeshProUGUI _dialogText;
    
    
    public Animator settingsAnimator;

    public void OpenWindow()
    {
        settingsAnimator.SetBool("isStart", true);
    }

    public bool ValidationRedField()
    {
        if (string.IsNullOrEmpty(_redField.text))
        {
            Error("Значения не могут быть пустыми!");
            return false;
        }
        else if (float.Parse(_redField.text) > 1 || 
                 float.Parse(_redField.text) < 0)
        {
            Error("Значение не может быть меньше 0 и больше 1!");
            return false;
        }
        
        return true;
    }    
    
    public bool ValidationGreenField()
    {
        if (string.IsNullOrEmpty(_greenField.text))
        {
            Error("Значения не могут быть пустыми!");
            return false;
        }
        else if (float.Parse(_greenField.text) > 1 || 
                 float.Parse(_greenField.text) < 0)
        {
            Error("Значение не может быть меньше 0 и больше 1!");
            return false;
        }
        
        return true;
    }    
    
    public bool ValidationBlueField()
    {
        if (string.IsNullOrEmpty(_blueField.text))
        {
            Error("Значения не могут быть пустыми!");
            return false;
        }
        else if (float.Parse(_blueField.text) > 1 || 
                 float.Parse(_blueField.text) < 0)
        {
            Error("Значение не может быть меньше 0 и больше 1!");
            return false;
        }

        return true;
    }

    public void ValidationFontSizeField()
    {
        if (string.IsNullOrEmpty(_fontSizeField.text))
        {
            Error("Значения не могут быть пустыми!");
            return;
        }

        _dialogText.fontSize = float.Parse(_fontSizeField.text);
    }

    void Error(string message)
    {
        StopCoroutine(Wait());
        _errorText.text = message;
        settingsAnimator.SetBool("isError", true);
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(5);
        settingsAnimator.SetBool("isError", false);
    }

    public void SetColor()
    {
        bool flag = ValidationRedField() && ValidationGreenField() && ValidationBlueField();
        
        if (!flag)
        {
            return;
        }
        else
        {
            float red = float.Parse(_redField.text);
            float green = float.Parse(_greenField.text);
            float blue = float.Parse(_blueField.text);
            
            _dialogueWindow.GetComponent<Image>().color = new Color(red, green, blue, 0.6f);
        }
    }

    public void CloseWindow()
    {
        settingsAnimator.SetBool("isStart", false);
        settingsAnimator.SetBool("isError", false);
    }
}
