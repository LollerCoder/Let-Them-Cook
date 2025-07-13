using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyDialogue : MonoBehaviour
{
    public static LobbyDialogue Instance;
    public static bool FirstTime;

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
            FirstTime = true;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (FirstTime)
        {
            FirstTime = false;
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
