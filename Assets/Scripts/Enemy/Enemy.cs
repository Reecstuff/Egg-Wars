using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public delegate void EnemyDeath(Enemy thisEnemy);
    public event EnemyDeath OnEnemyDeath;

    // Start is called before the first frame update
    protected void Start()
    {
        sonar = GetComponentInChildren<PlayerSonar>();
        animator = GetComponentInChildren<Animator>();
        particleSystem = GetComponentsInChildren<ParticleSystem>();
        agent = GetComponent<NavMeshAgent>();
        standardSpeed = Speed;
        OnSpeedChange(Speed);
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
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

    protected void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Melee"))
        {
            Destroy(gameObject); //Or takes damage
        }

        if (other.gameObject.GetComponent<PlayerController>())
        {
            OnPlayerCollision();
        }
    }

    virtual protected void OnPlayerCollision()
    {

    }

    virtual protected void DamagePlayer()
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
