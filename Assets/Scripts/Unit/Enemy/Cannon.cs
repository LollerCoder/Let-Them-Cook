using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cannon : SpecialUnits {
    [SerializeField]
    private ParticleSystem CornParticle;
    [SerializeField]
    private Animator CannonAnims;

    public List<Tile> targetTiles = new List<Tile>();

    public float speed;

    [SerializeField]
    private Transform parent;

    [SerializeField]
    private Transform bossTowerPos;

    [SerializeField]
    private Transform targetTilesCenter;

    [SerializeField]
    private CannonTile controlTile;

    [SerializeField]
    private GameObject popcornPrefab;

    [SerializeField]
    private bool alreadyTurned = false;

    [SerializeField]
    private bool bossCannon = false;

    private bool alreadyShotBossTower = false;

    private Popcorn popcorn;

    private List<Unit> playerUnits = new List<Unit>();

    public void Start() {
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.PLAYERDEATH, this.UpdatePlayerUnitList);
        this.Sprite = holder;
        this.Speed = this.speed;
        GameObject obj = GameObject.Instantiate(this.popcornPrefab);
        if (obj.GetComponent<Popcorn>()) {
            this.popcorn = obj.GetComponent<Popcorn>();
        }
        this.popcorn.gameObject.SetActive(false);
        if (this.bossCannon) {
            List<Unit> units = UnitActionManager.Instance.UnitList;

            foreach (Unit unit in units) {
                if (unit.Type == EUnitType.Ally) {
                    playerUnits.Add(unit);
                }
            }
        }
        UnitActionManager.Instance.TurnOrder.Add(this);       
    }

    private IEnumerator Rotate() {
        Vector3 direction = this.bossTowerPos.position - this.parent.position;
        direction.y = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

        while (Quaternion.Angle(this.parent.rotation, targetRotation) > 0.1f) {
            this.parent.rotation = Quaternion.RotateTowards(this.parent.rotation,
                                                            targetRotation,
                                                            90f * Time.deltaTime);
            yield return null;
        }

        this.parent.rotation = targetRotation;
        this.alreadyTurned = true;
    }

    public override IEnumerator Turn() {
        this.location = this.transform;
        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);
        if (!this.alreadyTurned && this.CheckIfAllyUnitOnControlTile() == EUnitType.Ally) {

            yield return this.StartCoroutine(Rotate());
        }
        if (this.CheckIfAllyUnitOnControlTile() == EUnitType.SpecialTile || this.alreadyShotBossTower) {
            this.StartCoroutine(this.NextTurn(0f));
        }
        else {
            this.CannonAnims.SetTrigger("Fire");
        }
    }
    public void Action() {
        if (this.CheckIfAllyUnitOnControlTile() == EUnitType.Ally) {
            this.AttackBossTower();
        }
        else if (this.CheckIfAllyUnitOnControlTile() == EUnitType.Enemy) {
            this.AttackNearTiles();
        }
        else if (this.bossCannon){
            this.AttackRandomPlayerUnit();
        }
        else {
            this.StartCoroutine(this.NextTurn(0f));
        }
    }

    private void AttackBossTower() {
        this.location = this.bossTowerPos;
        this.SpawnPopcorn(this.bossTowerPos);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);
        this.alreadyShotBossTower = true;
    }

    private void AttackNearTiles() {
        this.location = this.targetTilesCenter;
        this.SpawnPopcorn(this.location);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleCamera_Events.CURRENT_FOCUS);
        foreach (Tile tile in this.targetTiles) {
            Ray ray = new Ray(tile.transform.position, Vector3.up);
            Debug.DrawRay(tile.transform.position, Vector3.up, Color.red, 5f);
            if (Physics.Raycast(ray, out RaycastHit hit, 50.0f, LayerMask.GetMask("Units"))) {
                if (hit.collider.gameObject.GetComponent<Unit>() is Unit unit) {
                    unit.TakeDamageFromTile(10);
                }
            }
        }
        this.StartCoroutine(this.NextTurn(1.0f));
    }

    private EUnitType CheckIfAllyUnitOnControlTile() {
        Ray ray = new Ray(this.controlTile.transform.position, Vector3.up);
        Debug.DrawRay(this.controlTile.transform.position, Vector3.up, Color.red, 5f);

        if (Physics.Raycast(ray, out RaycastHit hit, 50.0f, LayerMask.GetMask("Units"))) {
            if (hit.collider.gameObject.GetComponent<Unit>() is Unit unit) {
                if (unit.Type == EUnitType.Ally) {
                    return EUnitType.Ally;
                }
                if (unit.Type == EUnitType.Enemy) {
                    return EUnitType.Enemy;
                }
            }
        }
        return EUnitType.SpecialTile;
    }

    private void AttackRandomPlayerUnit() { 
        int rand = Random.Range(0, playerUnits.Count);
        this.location = playerUnits[rand].transform;

        this.SpawnPopcorn(this.location);
    }
    public void UpdatePlayerUnitList() {
        List<Unit> units = UnitActionManager.Instance.UnitList.FindAll(u => u.Type == EUnitType.Ally);
        this.playerUnits = units;
    }
    private void SpawnPopcorn(Transform target) {
        Vector3 newPos = new Vector3(target.transform.position.x,
                                     target.transform.position.y + 10f,
                                     target.transform.position.z);

        this.popcorn.transform.position = newPos;
        this.popcorn.gameObject.SetActive(true);
    }
    private IEnumerator NextTurn(float seconds) {
        yield return new WaitForSeconds(seconds);
        EventBroadcaster.Instance.PostEvent(EventNames.BattleManager_Events.NEXT_TURN);
    }
    public void CannonFX()
    {
        CornParticle.Play();
    }
}
