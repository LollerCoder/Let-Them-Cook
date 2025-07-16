using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
    [SerializeField] private Text Title;
    [SerializeField] private Text ButtonText;  

    // Start is called before the first frame update
    void Start()
    {
       
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION,UISetup);
    }

    void UISetup(Parameters param)
    {

        this.Title.text = param.GetStringExtra("Title","NEVERAPPEARS");
        this.ButtonText.text = param.GetStringExtra("ButtonText", "NEVERAPPEARS");
        this.GetComponentInChildren<DemoSceneLoading>().ScreenName = SceneManager.GetActiveScene().name;

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

