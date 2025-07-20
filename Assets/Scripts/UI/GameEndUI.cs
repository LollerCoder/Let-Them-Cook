using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameEndUI : MonoBehaviour
{
   
    [SerializeField] private Image TitleImg;
    [SerializeField] private Image ButtonImg;
    [SerializeField] private Image TitleTextImg;
    //Sucess
    [SerializeField] private Sprite TitleImgWin;
    [SerializeField] private Sprite ButtonImgWin;
    [SerializeField] private Sprite TitleTextImgWin;
    //Fail
    [SerializeField] private Sprite TitleImgLose;
    [SerializeField] private Sprite ButtonImgLose;
    [SerializeField] private Sprite TitleTextImgLose;

    // Start is called before the first frame update
    void Start()
    {
       
        EventBroadcaster.Instance.AddObserver(EventNames.BattleManager_Events.CHECK_END_CONDITION,UISetup);
    }

    void UISetup(Parameters param)
    {

        if(param.GetBoolExtra("End", false))
        {
            this.TitleImg.sprite = TitleImgWin;
            this.ButtonImg.sprite = ButtonImgWin;
            this.TitleTextImg.sprite = TitleTextImgWin;

        }
        else
        {
            this.TitleImg.sprite = TitleImgLose;
            this.ButtonImg.sprite = ButtonImgLose;
            this.TitleTextImg.sprite = TitleTextImgLose;
        }
        
        this.GetComponentInChildren<DemoSceneLoading>().ScreenName = param.GetStringExtra("SceneToLoad", "NEVERAPPEARS");

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

