using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public static DialogueStart Instance = null;
    [SerializeField] private DialogueTrigger start;

    [SerializeField] private DialogueTrigger nextToJenkins;
    [SerializeField] private DialogueTrigger notNextToJenkins;
    [SerializeField] private DialogueTrigger attackedJenkins;

    
    [SerializeField] private DialogueTrigger nextToFlower;
    [SerializeField] private DialogueTrigger notNextToFlower;

    [SerializeField] private DialogueTrigger outlines;
    [SerializeField] private DialogueTrigger turnOrder;

    private int turnsCounted = 0;


    //bool startDone = false;
    //bool movedCorrectly = false;
    // Start is called before the first frame update
    void Start()
    {
        BattleUI.Instance.HideWaitButton();
        DialogueManager.Instance.StartDialogue(this.start.dialogue);
        //EventBroadcaster.Instance.AddObserver(EventNames.BattleUI_Events.WAIT_BUTTON_SHOW, this.damagedJenkins);
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.NEXT_TURN, this.turnTrack);
    }

    public void jenkins()
    {
        DialogueManager.Instance.StartDialogue(this.nextToJenkins.dialogue);
    }
    public void notJenkins()
    {
        DialogueManager.Instance.StartDialogue(this.notNextToJenkins.dialogue);
    }

    public void damagedJenkins()
    {
        DialogueManager.Instance.StartDialogue(this.attackedJenkins.dialogue);
    }

    public void flower()
    {
        DialogueManager.Instance.StartDialogue(this.nextToFlower.dialogue);
    }

    public void notFlower()
    {
        DialogueManager.Instance.StartDialogue(this.notNextToFlower.dialogue);
    }

    public void outlinePlay()
    {
        DialogueManager.Instance.StartDialogue(this.outlines.dialogue);
    }

    public void turnOrderPlay()
    {
        DialogueManager.Instance.StartDialogue(this.turnOrder.dialogue);
    }

    private void turnTrack()
    {
        switch (turnsCounted)
        {
            case 1:
                
                this.damagedJenkins();
                break;
            case 2:
                this.outlinePlay();
                break;
            case 3:
                this.turnOrderPlay();
                break;

        }
        turnsCounted++;

        Debug.Log("Turn: " + turnsCounted);
    }


    // Update is called once per frame
    void Update()
    {
        if (turnsCounted > 0 && UnitActionManager.Instance.OnMove == true)
        {
            BattleUI.Instance.ShowWaitButton();
        }
    }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }
}
