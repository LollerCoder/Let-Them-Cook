using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    private void Play(string name) {
        SFXManager.Instance.Play("Steps");
    }
}
