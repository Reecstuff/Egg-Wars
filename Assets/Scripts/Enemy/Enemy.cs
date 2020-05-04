using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    public int health = 100;
    public float Speed = 10;
    public float standardSpeed;

    public int damage = 3;

    protected ParticleSystem[] particleSystem; 

    [SerializeField]
    protected float maxPitch = 2;
    protected NavMeshAgent agent;
    protected AudioSource source;
    protected Animator animator;
    protected PlayerSonar sonar;
    protected int wiggleSafe = 0;
    

    public delegate void EnemyDeath(Enemy thisEnemy);
    public event EnemyDeath OnEnemyDeath;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        sonar = GetComponentInChildren<PlayerSonar>();
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentsInChildren<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        standardSpeed = Speed;
        OnSpeedChange(Speed);
        source = GetComponent<AudioSource>();
    }

    public void PlayAudio(AudioClip clip = null, bool loop = false)
    {
        if(!source.isPlaying)
        {
            if (clip != null)
                source.clip = clip;

            source.loop = loop;
            source.Play();
        }
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

    virtual public void PlayerEnterTrigger(GameObject other)
    {

    }

    virtual public void PlayerInTriggerStay(GameObject other)
    {

    }

    virtual public void PlayerExitTrigger(GameObject other)
    {

    }


    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            Destroy(gameObject); //Or takes damage
        }

        if (other.gameObject.GetComponent<PlayerController>())
        {
            OnPlayerCollision(other.gameObject);
        }
    }

    virtual protected void OnPlayerCollision(GameObject player)
    {
        
    }

    virtual public void DamagePlayer()
    {

    }

    virtual public void EnemyDying()
    {

    }

    protected void PlayParticles()
    {
        animator.gameObject.SetActive(false);
        for (int i = 0; i < particleSystem.Length; i++)
        {
            particleSystem[i].Play();
        }
    }

    private void OnDisable()
    {
        OnEnemyDeath?.Invoke(this);
    }

    protected void Die()
    {
        Destroy(gameObject);
    }
}
