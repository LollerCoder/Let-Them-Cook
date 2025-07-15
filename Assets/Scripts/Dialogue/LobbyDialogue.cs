using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDialogue : MonoBehaviour
{
    public static LobbyDialogue Instance;
    public static List<bool> DialogueComplete = new List<bool>();

    [Header("Dialogue")]
    [SerializeField]
    private Dialogue _IntroDialogue;
    [SerializeField]
    private Dialogue _MapDialogue;

    private bool _MapTutorialDone = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            for (int i = 0; i < 5; i++)
            {
                DialogueComplete.Add(false);
            }
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!LobbyDialogue.DialogueComplete[0])
        {
            LobbyDialogue.DialogueComplete[0] = true;
            _MapTutorialDone = false;
            DialogueManager.Instance.StartDialogue(_IntroDialogue);

            return;
        }


    }

    public void MapTutorialPlay()
    {
        if (!_MapTutorialDone)
        {
            _MapTutorialDone = true;
            DialogueManager.Instance.StartDialogue(_MapDialogue);
        }
    }
}
