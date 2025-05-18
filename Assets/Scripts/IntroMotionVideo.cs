using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroMotionVideo : MonoBehaviour
{

    public VideoPlayer video;
    public string SceneSiguiente;

    void Start()
    {
        video = GetComponent<VideoPlayer>();
        video.Play();
        video.loopPointReached += CheckOver;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckOver(VideoPlayer vp)
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(SceneSiguiente);
    }
}
