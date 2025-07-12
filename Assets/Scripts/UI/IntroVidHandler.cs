using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVidHandler : MonoBehaviour
{
    //[Header("Video")]
    //[SerializeField]
    //private GameObject _vidBackground;
    //VideoPlayer _vidPlayer;

    [Header("Scenes")]
    [SerializeField] 
    private string introScene = "Intro Cutscene";
    [SerializeField] 
    private string nextScene = "Intro Cutscene";

    [Header("Level Holder")]
    [SerializeField]
    private GameObject levelHolder;



    // Start is called before the first frame update
    void Start()
    {
        //this._vidPlayer = this.gameObject.GetComponent<VideoPlayer>();
        //if (this._vidPlayer == null) Debug.LogWarning("No video player found in " + this.gameObject.name);

        //this._vidBackground.SetActive(false);

        //this._vidPlayer.loopPointReached += this.OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    this._vidPlayer.Stop();
        //    this.OnVideoEnd(this._vidPlayer);
        //}
    }

    public void PlayVid()
    {
        //Debug.Log("Playing video!");

        //this._vidBackground.SetActive(true);

        //this._vidPlayer.Play();


        //make new save
        levelHolder.GetComponent<GameScript>().SaveGame();

        //loading the scene
        SceneManager.LoadScene(introScene);
    }

    private void OnVideoEnd()
    {
        //Debug.Log("Video ended!");

        //make new save
        //levelHolder.GetComponent<GameScript>().SaveGame();

        //loading the scene
        SceneManager.LoadScene(nextScene);
    }
}
