using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpKenIntroHandler : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField]
    private Animator _SargeBobAnimator;
    [SerializeField]
    private Animator _TomIttoAnimator;
    [SerializeField]
    private Animator _PumpKenAnimator;

    public void SetWalking(int isIdle)
    {
        bool value = false;
        if (isIdle == 1) value = true;
        this._SargeBobAnimator.SetBool("Idle", value);
        this._TomIttoAnimator.SetBool("Idle", value);
    }
}
