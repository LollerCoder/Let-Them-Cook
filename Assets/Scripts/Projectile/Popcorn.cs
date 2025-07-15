using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popcorn : MonoBehaviour {
    public float gravityMultiplier = 2f;
    public bool forAllyTarget = false;
    [SerializeField]
    private Rigidbody rb;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.GetComponent<Tower>() is Tower tower) {
            tower.TriggerFallAnim();
        }

        if (collision.gameObject.GetComponent<Unit>() is Unit unit) {
            unit.TakeDamageFromTile(5);
            this.forAllyTarget = false;
        }

        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.identity;

        this.rb.velocity = Vector3.zero;
        this.rb.angularVelocity = Vector3.zero;

        this.gameObject.SetActive(false);
    }
    public void FixedUpdate() {
        this.rb.AddForce(Physics.gravity * (this.gravityMultiplier - 1), ForceMode.Acceleration);
    }
}
