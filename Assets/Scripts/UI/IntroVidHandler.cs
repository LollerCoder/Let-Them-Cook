using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroVidHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _vidBackground;

    VideoPlayer _vidPlayer;

    // Start is called before the first frame update
    void Start()
    {
        this._vidPlayer = this.gameObject.GetComponent<VideoPlayer>();
        if (this._vidPlayer == null) Debug.LogWarning("No video player found in " + this.gameObject.name);

        this._vidBackground.SetActive(false);

        this._vidPlayer.loopPointReached += this.OnVideoEnd;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayVid()
    {
        //Debug.Log("Playing video!");

        this._vidBackground.SetActive(true);

        this._vidPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer source)
    {
        //Debug.Log("Video ended!");
        SceneManager.LoadScene("Tutorial-1");
    }
}
