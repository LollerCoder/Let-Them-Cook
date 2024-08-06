using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {
    private Transform currentUnit;
    private List<Renderer> children = new List<Renderer>();
    public void UpdatePointer(Unit unit) {
        foreach (Transform child in unit.transform) {
            if(child.tag == "Arrow") {
                currentUnit = child.transform;
                break;
            }
        }

        this.transform.position = currentUnit.position;
    }

    private void Show() {
        foreach(Renderer child in this.children) {
            child.enabled = true;
        }
    }
    private void Hide() {
        foreach (Renderer child in this.children) {
            child.enabled = false;
        }
    }

    private void Start() {
        foreach(Transform child in this.transform) {
            if (child.tag == "Pointer") {
                this.children.Add(child.gameObject.GetComponent<Renderer>());
            }
        }
        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_ATTACK_START, this.Hide);
        EventBroadcaster.Instance.AddObserver(EventNames.UnitActionEvents.ON_ATTACK_END, this.Show);
    }
}
