using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4DialogueActivator : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField]
    private Dialogue _IntroDialogue;
    [SerializeField]
    private Dialogue _HostageFreeDialogue;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED, this.IntroDialogue);
        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.HOSTAGE_FREE, this.HostageFreeDialogue);
    }

    public void IntroDialogue()
    {
        DialogueManager.Instance.StartDialogue(_IntroDialogue);
    }

    public void HostageFreeDialogue()
    {
        DialogueManager.Instance.StartDialogue(_HostageFreeDialogue);
    }
}
