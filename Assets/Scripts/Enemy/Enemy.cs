using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{

    public float Speed = 10;
    public float standardSpeed;
    [SerializeField]
    float maxPitch = 2;

    NavMeshAgent agent;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        standardSpeed = Speed;
        OnSpeedChange(Speed);
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio()
    {
        if(!source.isPlaying)
            source.Play();
    }

    public void PitchAudio(float negativationValue)
    {
        if(maxPitch - negativationValue >= 1)
        {
            source.pitch = maxPitch - negativationValue;
        }
    }



    public void OnSpeedChange(float newSpeed)
    {
        Speed = newSpeed;
        agent.speed = Speed;

        // Set Animation Speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            Destroy(gameObject); //Or takes damage
        }

        if(other.gameObject.GetComponent<PlayerController>())
        {
            transform.DOScale(1.5f, 0.6f);
            maxPitch = 0;
            source.Stop();
            source.Play();
            source.DOPitch(3, 0.1f);
            source.DOFade(2, 0.5f);
            Invoke(nameof(Die), 1f);
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
