using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAnimationHandler : MonoBehaviour
{
    [Header("Dialogues")]
    [SerializeField]
    private Dialogue _DialogueArrived;

    private bool _PrisonerFree = false;

    private Animator _Animator;

    private void Start()
    {
        this._Animator = GetComponent<Animator>();

        EventBroadcaster.Instance.AddObserver(EventNames.HostageRescue_Events.HOSTAGE_FREE, this.PrisonerFreed);

        EventBroadcaster.Instance.AddObserver(EventNames.Dialogue_Events.ON_DIALOGUE_FINISHED, this.MoveBoat);
    }

    public void PrisonerFreed()
    {
        this._PrisonerFree = true;
    }

    public void ReachedEnd()
    {
        EventBroadcaster.Instance.PostEvent(EventNames.HostageRescue_Events.BOAT_ARRIVED);
                
        DialogueManager.Instance.StartDialogue(_DialogueArrived);
    }

    public void MoveBoat()
    {
        if (this._PrisonerFree) this._Animator.SetBool("IsMoving", true);
    }
}
