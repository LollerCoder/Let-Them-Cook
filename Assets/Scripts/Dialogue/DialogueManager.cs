using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance = null;

    [SerializeField] private Queue sentences = new Queue();

    public float TextSpeed;

    public Animator animator;
    public Text nameText;
    public Text dialogeText;
    public Image image;

    private bool isTyping = false;
    private bool skipTyping = false;

    public void StartDialogue(Dialogue dialogue) {
        this.animator.SetBool("Open", true);

        this.nameText.text = dialogue.name;

        this.image.sprite = dialogue.sprite;

        this.sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            this.sentences.Enqueue(sentence);
        }

        this.DisplayNextSentence();
    }

    public void DisplayNextSentence() {

        if (this.isTyping) { // check if its currently typing before skipping it
            this.skipTyping = true;
            return;
        }

        if (this.sentences.Count == 0) {
            this.EndDialogue();
            return;
        }

        string sentence = (string)this.sentences.Dequeue();
        this.StopAllCoroutines();
        this.StartCoroutine(this.TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        this.dialogeText.text = "";
        this.isTyping = true; // make isTyping true since its already here
        this.skipTyping = false; // make it false at the start
        foreach (char letter in sentence.ToCharArray()) {
            if (this.skipTyping) {
                this.dialogeText.text = sentence;
                break;
            }
            this.dialogeText.text += letter;

            yield return new WaitForSeconds(this.TextSpeed);
        }

        this.isTyping = false; // reset after done typing
    }

    public void EndDialogue() {
        this.animator.SetBool("Open", false);
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
