using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    private ParticleSystem towerParticle;
    [SerializeField]
    private Animator towerAnim;

    public void TriggerFallAnim() {
        this.towerAnim.SetTrigger("TowerFall");
    }

    private void PlayTowerFX() {
        this.towerParticle.Play();
    }

    private IEnumerator NextTurn(float seconds) {
        yield return new WaitForSeconds(seconds);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }
}
