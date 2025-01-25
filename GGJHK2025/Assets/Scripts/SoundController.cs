using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    AudioClip[] audioClips;
    [SerializeField]
    AudioSource source;
    GameManager manager;
    bool inTransition;
    void Start()
    {
        manager = FindObjectOfType<GameManager>();
        source.clip = audioClips[0];
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (manager.Score - manager.badScore)
        {
            case  <=-10:
                source.clip = audioClips[1];
                break;
            case >= 10:
                source.clip = audioClips[0];
                break;


        }


    }
    void Transition(int type)
    {
        switch(type)
        {
            case 0:
                float time = 4f;
                source.volume -= 1;
                if (source.volume <= 0)
                {
                    time -= 1 * Time.deltaTime;
                }
                if (time <= 0)
                {
                    time = 4f;
                    inTransition = false;
                    source.volume = 0.7f;
                    source.clip = audioClips[1];
                }
                break;
        }
        

    }
}
