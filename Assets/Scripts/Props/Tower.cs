using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
    [SerializeField]
    private ParticleSystem towerParticle;
    [SerializeField]
    private Animator towerAnim;
    [SerializeField]
    private Cannon cannon;
    [SerializeField]
    private GameObject cornSpawn;

    public void TriggerFallAnim() {
        this.towerAnim.SetTrigger("TowerFall");
    }

    private void RemoveCannonInTurnOrder() {
        UnitActionManager.Instance.RemoveUnitFromOrder(this.cannon);
    }

    private void PlayTowerFX() {
        this.towerParticle.Play();
    }

    private void MoveCamera() {
        Parameters param = new Parameters();
        param.PutExtra("POS", this.cornSpawn.transform.position);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.MOVE_CAMERA, param);
    }
    private IEnumerator NextTurn(float seconds) {
        yield return new WaitForSeconds(seconds);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }
}
