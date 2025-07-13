using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2DialogueActivator : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField]
    private Dialogue _IntroDialogue;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.ADDED_UNITS_SELECTED, LevelStartDialogue);
    }

    public void LevelStartDialogue()
    {
        DialogueManager.Instance.StartDialogue(_IntroDialogue);
    }

}
