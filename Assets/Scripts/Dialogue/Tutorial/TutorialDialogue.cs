using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStart : MonoBehaviour
{
    public static DialogueStart Instance = null;
    [SerializeField] private DialogueTrigger start;
    [SerializeField] private DialogueTrigger nextToJenkins;
    [SerializeField] private DialogueTrigger notNextToJenkins;


    //bool startDone = false;
    //bool movedCorrectly = false;
    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.StartDialogue(this.start.dialogue);
    }

    public void jenkins()
    {
        DialogueManager.Instance.StartDialogue(this.nextToJenkins.dialogue);
    }
    public void notJenkins()
    {
        DialogueManager.Instance.StartDialogue(this.notNextToJenkins.dialogue);
    }



    // Update is called once per frame
    void Update()
    {
           
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
