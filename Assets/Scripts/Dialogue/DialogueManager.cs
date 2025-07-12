using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance = null;

    [SerializeField] private Queue sentences = new Queue();
    [SerializeField] private Queue sprites = new Queue();
    [SerializeField] private Queue names = new Queue();

    public float TextSpeed;

    public Animator animator;
    public Text nameText;
    public Text dialogeText;
    public Image image;

    private bool isTyping = false;
    private bool skipTyping = false;

    public void StartDialogue(Dialogue dialogue) {
        EventBroadcaster.Instance.PostEvent(EventNames.Dialogue_Events.ON_DIALOGUE_START);

        this.animator.SetBool("Open", true);

        this.sentences.Clear();
        this.sprites.Clear();
        this.names.Clear();

        foreach (Sprite sprite in dialogue.sprites) {
            this.sprites.Enqueue(sprite);
        }

        foreach (string name in dialogue.names) {
            this.names.Enqueue(name);
        }

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

        Sprite sprite = (Sprite)this.sprites.Dequeue();
        string sentence = (string)this.sentences.Dequeue();
        string name = (string)this.names.Dequeue();

        this.nameText.text = name;
        this.image.sprite = sprite;
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
        EventBroadcaster.Instance.PostEvent(EventNames.Dialogue_Events.ON_DIALOGUE_FINISHED);
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
