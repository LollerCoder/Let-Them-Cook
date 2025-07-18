using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileHelper : MonoBehaviour {
    public static TileHelper Instance = null;

    [SerializeField]
    private GameObject rangeTile;

    private List<GameObject> activeRange = new List<GameObject>();

    private List<GameObject> inactiveRange = new List<GameObject>();

    [SerializeField]
    private Material Attack;
    [SerializeField]
    private Material Heal;
    [SerializeField]
    private Material Walk;
    [SerializeField]
    private Material Current;
    [SerializeField]
    private Material HealTile;
    [SerializeField]
    private Material CannonTile;

    [SerializeField]
    private Transform RangeParent;

    public GameObject SpawnRangeIndicator(Vector3 pos, RangeType type) {
        GameObject obj = this.inactiveRange[0];
        this.inactiveRange.Remove(obj);

        //Debug.Log(obj.name);
        obj.SetActive(true);
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.identity;

        switch (type) {
            case RangeType.WALK:
                obj.GetComponent<MeshRenderer>().material = Walk;
                break;
            case RangeType.ATTACK:
                obj.GetComponent<MeshRenderer>().material = Attack;
                break;
            case RangeType.HEAL:
                obj.GetComponent<MeshRenderer>().material = Heal;
                break;
            case RangeType.CURRENT:
                obj.GetComponent<MeshRenderer>().material = Current;
                break;
            case RangeType.HEALTILE:
                obj.GetComponent<MeshRenderer>().material = HealTile;
                break;
            case RangeType.OBJECTIVE:
                obj.GetComponent<MeshRenderer>().material = CannonTile;
                break;
            default: break;
        }

        activeRange.Add(obj);
        return obj;
    }

    public void DeactivateRangeIndicator(GameObject range, Tile tile) {
        if (range == null) { // check if range is null
            return;
        }

        if (activeRange.Contains(range)) {
            range.SetActive(false);
            //Debug.Log(range.name);
            this.inactiveRange.Add(range);
            this.activeRange.Remove(range);
            tile.rangeIndicator = null;
        }
    }
    public void Awake() {
        if (Instance == null) {
            Instance = this;
        }
        else if (Instance != null) {
            Destroy(this.gameObject);
        }

        for (int i = 0; i < 50; i++) {
            GameObject clone = GameObject.Instantiate(rangeTile, this.RangeParent);
            clone.name = i.ToString();
            clone.SetActive(false);
            this.inactiveRange.Add(clone);
        }
    }
}

public enum RangeType { 
    WALK,
    ATTACK,
    HEAL,
    CURRENT,
    HEALTILE,
    CANNON,
    OBJECTIVE
}

