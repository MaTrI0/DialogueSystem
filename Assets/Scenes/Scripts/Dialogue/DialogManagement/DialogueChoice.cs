using System;
using Ink.Runtime;
using TMPro;
using UnityEngine;

public class DialogueChoice : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private GameObject[] _choices;
    private TextMeshProUGUI[] _choicesText;

    public void Init()
    {
        _choicesText = new TextMeshProUGUI[_choices.Length];

        ushort index = 0;
        foreach (GameObject choice in _choices)
        {
            _choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
        }
    }

    public bool DisplayChoices(Story story)
    {
        Choice[] currentChoices = story.currentChoices.ToArray();

        if (currentChoices.Length > _choices.Length)
        {
            throw new ArgumentNullException("Ошибка! В сценарии больше выборов, чем кнопок в игре!");
        }

        HideChoices();

        ushort index = 0;
        
        foreach (Choice choice in currentChoices)
        {
            _choices[index].SetActive(true);
            _choicesText[index++].text = choice.text;
            
            animator.SetBool("isStart", true);
        }

        return currentChoices.Length > 0;
    }

    public void HideChoices()
    {
        animator.SetBool("isStart", true);
        foreach (var button in _choices)
        {
            button.SetActive(false);
        }
    }
}
