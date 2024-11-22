using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedVegetableManager : MonoBehaviour {
    public static DroppedVegetableManager Instance = null;

    [SerializeField]
    private List<StringDroppedVegtPair> dropVegetablePair = new List<StringDroppedVegtPair>();

    private Dictionary<string, DroppedVegetable> dropVegetableDictionary = new Dictionary<string, DroppedVegetable>();

    private List<DroppedVegetable> vegInField = new List<DroppedVegetable>();
    public List<DroppedVegetable> VegInField { 
        get { return this.vegInField; } 
    }

    public void CreateDropVegetable(Unit deadUnit) {
        string name = deadUnit.IngredientType.ToString();
        if (this.dropVegetableDictionary.ContainsKey(name)) {
            DroppedVegetable droppedVegetable = GameObject.Instantiate(this.dropVegetableDictionary[name]);
            Vector3 pos = new Vector3(deadUnit.transform.position.x, 0.5f, deadUnit.transform.position.z);
            droppedVegetable.transform.position = pos;
            droppedVegetable.Name = name;
            droppedVegetable.Tile = deadUnit.Tile;
            this.ProvideStats(droppedVegetable);

            this.vegInField.Add(droppedVegetable);
        }
        else {
            Debug.Log(name + " not found!");
        }
    }
    private void ProvideStats(DroppedVegetable veg) {
        
    }
    public void PickUpVegetable(DroppedVegetable veg) {
        Parameters param = new Parameters();
        param.PutExtra("VEG", veg);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.UPDATE_INVENTORY, param);
        veg.Hide();
        this.vegInField.Remove(veg);
    }
    public void EatVegetable(DroppedVegetable veg) {
        veg.Hide();
        this.vegInField.Remove(veg);
    }

    private void Start() {
        foreach (StringDroppedVegtPair pair in this.dropVegetablePair) {
            if (!this.dropVegetableDictionary.ContainsKey(pair.key)) {
                this.dropVegetableDictionary.Add(pair.key, pair.value);
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
