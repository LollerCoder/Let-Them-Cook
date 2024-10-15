using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedVegetableManager : MonoBehaviour {
    public static DroppedVegetableManager Instance = null;

    [SerializeField]
    private List<StringDroppedVegtPair> dropVegetablePair = new List<StringDroppedVegtPair>();

    private Dictionary<string, DroppedVegetable> dropVegetableDictionary = new Dictionary<string, DroppedVegetable>();

    private void ProvideStats() {

    }
    public void CreateDropVegetable(string name, Vector3 pos) {
        if (this.dropVegetableDictionary.ContainsKey(name)) {
            DroppedVegetable droppedVegetable = GameObject.Instantiate(this.dropVegetableDictionary[name]);
            droppedVegetable.transform.position = pos;
        }
        else {
            Debug.Log(name + " not found!");
        }
    }

    private void Start() {
        foreach (StringDroppedVegtPair pair in dropVegetablePair) {
            if (!dropVegetableDictionary.ContainsKey(pair.key)) {
                dropVegetableDictionary.Add(pair.key, pair.value);
            }
        }
    }

    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != null) {
            Destroy(this.gameObject);
        }
    }
}

[Serializable]
public class StringDroppedVegtPair {
    public string key;
    public DroppedVegetable value;
    public StringDroppedVegtPair(string key, DroppedVegetable value) {
        this.key = key;
        this.value = value;
    }
}
