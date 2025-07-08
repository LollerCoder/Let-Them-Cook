using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3DialogueActivator : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField]
    private Dialogue _IntroDialogue;
    [SerializeField]
    private Dialogue _KeyFoundDialogue;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ON_START, LateStart);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED, LevelStartDialogue);
    }

    public void LateStart()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Level3_Objectives.KEY_FOUND, KeyFoundDialogue);
    }

    public void LevelStartDialogue()
    {
        DialogueManager.Instance.StartDialogue(_IntroDialogue);
    }

    public void KeyFoundDialogue(Parameters param)
    {
        Debug.Log("FOUND KEY!");
        DialogueManager.Instance.StartDialogue(_KeyFoundDialogue);
    }

}
