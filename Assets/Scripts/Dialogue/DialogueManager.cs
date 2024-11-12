using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance = null;

    [SerializeField] private Queue sentences = new Queue();

    public Text nameText;
    public Text dialogeText;
    public void StartDialogue(Dialogue dialogue) {

        this.nameText.text = dialogue.name;

        this.sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            this.sentences.Enqueue(sentence);
        }

        this.DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if(this.sentences.Count == 0) {
            this.EndDialogue();
            return;
        }

        string sentence = (string)this.sentences.Dequeue();
        this.dialogeText.text = sentence;
    }

    public void EndDialogue() {
        Debug.Log("End");
    }

    void Start() {
        
    }
    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != null) {
            Destroy(this.gameObject);
        }
    }

}
