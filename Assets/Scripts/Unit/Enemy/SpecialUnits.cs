using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialUnits : MonoBehaviour, ITurnTaker {
    protected Transform location;
    public Transform Location { get { return this.location; } }
    public float Speed { get; set; }
    public Sprite Sprite { get; set; }

    [SerializeField]
    protected Sprite holder;

    public virtual IEnumerator Turn() {
        yield return null;
    }

}
