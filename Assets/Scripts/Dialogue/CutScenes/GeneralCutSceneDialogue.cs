using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GeneralCutSceneDialogue : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField] private List<Dialogue> _Dialogues = new List<Dialogue>();
    private int _CurrentDialogueIndex = 0;
    [SerializeField] private bool _StartDialogueOnStart;

    [Header("Scene")]
    [SerializeField]
    private string _NextSceneToLoad;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Dialogue_Events.ON_DIALOGUE_FINISHED, this.LoadNextScene);

        if (_StartDialogueOnStart) this.StartDialogue();
    }

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(this._Dialogues[this._CurrentDialogueIndex]);
        this._CurrentDialogueIndex++;
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(this._NextSceneToLoad);
    }

    public void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveAllObservers();
    }
}
