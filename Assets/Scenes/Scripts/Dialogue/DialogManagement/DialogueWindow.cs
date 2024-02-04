using System;
using System.Collections;
using Ink.Runtime;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(DialogueChoice))]
public class DialogueWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _displayName;
    [SerializeField] private TextMeshProUGUI _displayText;
    
    [SerializeField] private GameObject _dialogueWindow;
    
    public Animator animator;
    
    [SerializeField, Range(0f, 20f)] private float _cooldownNewLater;

    private DialogueChoice _dialogueChoice;
    
    public bool IsStatusAnswer { get; private set; }
    public bool IsPlaing { get; private set; }
    public bool CanContinueToNextLine { get; private set; }

    public float CoolDownNewLater
    {
        get => _cooldownNewLater;
        private set
        {
            _cooldownNewLater = CheckCooldown(value);
        }
    }

    private float CheckCooldown(float value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Значение задержки между буквами отрицательное!");
        }

        return value;
    }

    public void Init()
    {
        IsStatusAnswer = false;
        CanContinueToNextLine = false;
        
        _dialogueChoice = GetComponent<DialogueChoice>();
        _dialogueChoice.Init();
    }

    public void SetActive(bool active)
    {
        if (active)
        {
            animator.SetBool("isStart", true);
        }
        else
        {
            animator.SetBool("isStart", false);
        }
        IsPlaing = active;
    }

    public void SetText(string text)
    {
        _displayText.text = text;
    }    
    
    public void Add(string text)
    {
        _displayText.text += text;
    }    
    
    public void Add(char letter)
    {
        _displayText.text += letter;
    }    
    
    public void ClearText()
    {
        SetText("");
    }

    public void SetName(string namePerson)
    {
        _displayName.text = namePerson;
    }

    public void SetCoolDown(float cooldown)
    {
        CoolDownNewLater = cooldown;
    }

    public void MakeChoice()
    {
        if (CanContinueToNextLine == false)
        {
            return;
        }

        IsStatusAnswer = false;
    }

    public IEnumerator DisplayLine(Story story)
    {
        string line = story.Continue();

        ClearText();
        _dialogueChoice.HideChoices();

        CanContinueToNextLine = false;
        
        bool isAddingRichText = false;

        yield return new WaitForSeconds(0.001f);

        foreach (char letter in line.ToCharArray())
        {
            if (Input.GetMouseButtonDown(0))
            {
                SetText(line);
                break;
            }

            isAddingRichText = letter == '<' || isAddingRichText;

            if (letter == '>')
            {
                isAddingRichText = false;
            }
            
            Add(letter);

            if (isAddingRichText == false)
            {
                yield return new WaitForSeconds(_cooldownNewLater);
            }
        }

        CanContinueToNextLine = true;
        IsStatusAnswer = _dialogueChoice.DisplayChoices(story);
    }
}
