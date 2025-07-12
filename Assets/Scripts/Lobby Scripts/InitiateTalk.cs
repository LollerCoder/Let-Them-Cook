using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateTalk : MonoBehaviour
{
    [SerializeField]
    private Dialogue _Dialogue;
    [SerializeField]
    private Animator _Animator;
    [SerializeField]
    private GameObject _ButtonPrompt;

    private bool isPlayerIn = false;
    private bool isTalking = false;

    // Start is called before the first frame update
    void Start()
    {
        if (_Animator != null)
        {
            _Animator.SetBool("Ally", true);
            _Animator.SetBool("Turn", true);
        }

        EventBroadcaster.Instance.AddObserver(EventNames.Dialogue_Events.ON_DIALOGUE_FINISHED, this.FinishedTalk);
        
        this._ButtonPrompt.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isPlayerIn && !isTalking)
        {
            Talk();
        }
    }

    public void FinishedTalk()
    {
        isTalking = false;
    }

    private void Talk()
    {
        this.isTalking = true;
        DialogueManager.Instance.StartDialogue(_Dialogue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") this.isPlayerIn = true;
        this._ButtonPrompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") this.isPlayerIn = false;
        this._ButtonPrompt.SetActive(false);
    }
}
