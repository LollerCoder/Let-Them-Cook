using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedVegetable : MonoBehaviour {
    private float accuracy;
    private float speed;
    private float attack;
    private float healthpoint;
    private float defense;

    public Tile Tile;

    public string Name;
    public void SetStats() {

    }

    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
