using UnityEngine;
using UnityEngine.SceneManagement;

public class PumpKenCutSceneDialogue : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField] private Dialogue PumpKenTalkTuah;

    [Header("Scene")]
    [SerializeField] private string NextScene;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.Dialogue_Events.ON_DIALOGUE_FINISHED, GoToNextScene);
    }

    public void InitiateTalk()
    {
        DialogueManager.Instance.StartDialogue(PumpKenTalkTuah);
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(NextScene);
    }
}
