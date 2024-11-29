using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CookTutorialDailogueHandler : MonoBehaviour
{
    [SerializeField]
    public Dialogue _dialogue;

    [SerializeField]
    public Dialogue _dialogue2;

    [SerializeField]
    private Animator _animator;

    private bool _isIterating = false;

    private int _currentTextIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        DialogueManager.Instance.StartDialogue(this._dialogue);
    }

    // Update is called once per frame
    void Update()
    {
        //if the dialogue ends, iterate
        if (this._isIterating && !this._animator.GetBool("Open"))
        {
            this._isIterating = false;
            this._currentTextIndex++;
            Debug.Log("Iterated! " + this._currentTextIndex);
        }

        //if the dialogue is playing...
        if (!this._isIterating && this._animator.GetBool("Open"))
        {
            this._isIterating = true;
        }

        //if dialogue ends and the player has cooked!
        if (!this._animator.GetBool("Open") && this._currentTextIndex == 2)
        {
            //Debug.Log("Going to next scene!");
            SceneManager.LoadScene("Level 1");
        }
    }

    public void GoToNextScene()
    {
        DialogueManager.Instance.StartDialogue(this._dialogue2);
    }
}
