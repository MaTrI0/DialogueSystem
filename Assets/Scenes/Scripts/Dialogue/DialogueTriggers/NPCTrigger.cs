using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    [SerializeField] private TextAsset _inkJSON;

    private bool _isPlayerEnter;

    private DialogueController _dialogueController;
    private DialogueWindow _dialogueWindow;

    private void Start()
    {
        _isPlayerEnter = false;

        _dialogueController = FindObjectOfType<DialogueController>();
        _dialogueWindow = FindObjectOfType<DialogueWindow>();
    }

    private void Update()
    {
        _isPlayerEnter = true;
        if (_dialogueWindow.IsPlaing == true || _isPlayerEnter == false)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _dialogueController.EnterDialogueMode(_inkJSON);
        }
    }
}
