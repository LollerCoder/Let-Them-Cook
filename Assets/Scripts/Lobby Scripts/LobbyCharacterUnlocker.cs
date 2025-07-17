using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterUnlocker : MonoBehaviour
{
    [Header("Characters")]
    [SerializeField]
    private GameObject _PumpKen;
    [SerializeField]
    private GameObject _GarlField;
    // Start is called before the first frame update
    void Start()
    {
        if (LevelManager.LevelsCompleted >= 3) _PumpKen.SetActive(true);
        else _PumpKen.SetActive(false);

        if (LevelManager.LevelsCompleted >= 4) _GarlField.SetActive(true);
        else _GarlField.SetActive(false);
    }
}
