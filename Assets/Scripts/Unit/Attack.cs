using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    [SerializeField]
    private GameObject FireBall;
    private Animator animator;

    private void Start() {
        this.animator = this.GetComponent<Animator>();
    }

    public void UnitAttack(Unit attacker, Unit target, int damage) {

        this.Melee(attacker, target, damage);
        this.Range(attacker, target, damage);
    }

    private void Range(Unit attacker, Unit target, int damage) {
        Quaternion originaRotation = attacker.transform.rotation;
        attacker.transform.LookAt(target.transform, new Vector3(0, 1, 0));
        target.TakeDamage(damage,attacker);
        
        this.StartCoroutine(this.WaitForAnimationRange(2.0f, attacker, originaRotation));
    }

    private void Melee(Unit attacker, Unit target, int damage){
        Quaternion originaRotation = attacker.transform.rotation;
        attacker.transform.LookAt(target.transform, new Vector3(0, 1, 0));
        this.animator.SetTrigger("Melee");

        EventBroadcaster.Instance.PostEvent(EventNames.UnitActionEvents.ON_ATTACK_START);

        this.StartCoroutine(this.WaitForAnimationMelee(2.0f, attacker, target, damage, originaRotation));

    }

    private IEnumerator WaitForAnimationMelee(float seconds, Unit attacker, Unit target, int damage, Quaternion rotation) {
        yield return new WaitForSeconds(seconds);
        target.TakeDamage(damage,attacker);
        attacker.transform.rotation = rotation;
        EventBroadcaster.Instance.PostEvent(EventNames.UnitActionEvents.ON_ATTACK_END);
    }

    private IEnumerator WaitForAnimationRange(float seconds, Unit attacker, Quaternion rotation) {
        yield return new WaitForSeconds(seconds);
        attacker.transform.rotation = rotation;
        EventBroadcaster.Instance.PostEvent(EventNames.UnitActionEvents.ON_ATTACK_END);
    }

}
