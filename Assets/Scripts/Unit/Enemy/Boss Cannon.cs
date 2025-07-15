using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCannon : SpecialUnits {
    [SerializeField]
    private ParticleSystem CornParticle;
    [SerializeField]
    private Animator CannonAnims;
    [SerializeField]
    private Popcorn popcorn;

    public float speed;

    public void Start() {
        this.Sprite = holder;
        this.Speed = this.speed;
        UnitActionManager.Instance.TurnOrder.Add(this);
    }
}
