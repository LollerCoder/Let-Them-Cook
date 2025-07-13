using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedVegetable : MonoBehaviour {
    public Tile Tile;
    public void Hide() {
        this.gameObject.SetActive(false);
    }
}
