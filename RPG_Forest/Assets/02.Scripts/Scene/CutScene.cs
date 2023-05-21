using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public PlayableDirector myPD;
    [SerializeField]
    private float duration;
    [SerializeField]
    private float time;

    void Start()
    {
        myPD = GetComponent<PlayableDirector>();
        myPD.initialTime = 0.0f;
        duration = (float)myPD.playableAsset.duration;
        time = (float)myPD.initialTime;
    }

    void Update()
    {
        time += Time.deltaTime;

        if(time > duration)
        {
            SceneManager.LoadScene(2);
        }
    }
}
