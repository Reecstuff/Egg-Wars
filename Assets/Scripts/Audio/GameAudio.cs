using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameAudio : MonoBehaviour
{
    public static GameAudio Instance;

    [SerializeField]
    AudioClip normalBackGroundMusic;
    [SerializeField]
    AudioClip BossMusic;
    [SerializeField]
    AudioClip MenuMusic;

    AudioSource source;

    private void Awake()
    {
        MakeSingelton();
        source = GetComponent<AudioSource>();
    }
    void MakeSingelton()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void SetBossMusic()
    {
        source.clip = BossMusic;
        source.Play();
    }

    public void ActivateRageSound(bool rage)
    {
        if (rage)
            source.DOPitch(2, 2);
        else
            source.DOPitch(1, 2);
    }

    public void SetNormalMusic()
    {
        source.clip = BossMusic;
        source.Play();
    }
}
