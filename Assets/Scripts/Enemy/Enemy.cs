using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

[RequireComponent(typeof(NavMeshAgent), typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    [SerializeField]
    protected string EnemyName;

    public int health = 100;
    public float Speed = 10;
    public float standardSpeed;

    public int damage = 3;

    protected ParticleSystem[] particleSystem;

    [SerializeField]
    protected float randomRadius = 5;
    [SerializeField]
    protected float maxPitch = 2;
    [SerializeField]
    protected int perLevelHealth = 4;

    protected NavMeshAgent agent;
    protected AudioSource source;
    protected Animator animator;
    protected PlayerSonar sonar;
    protected int wiggleSafe = 0;
    protected EnemyUI enemyUI;
    protected bool isdying = false;
    

    public bool noPlayerInSight = true;

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
        health += DungeonMaster.Instance.levelCount * perLevelHealth;
        enemyUI = GetComponentInChildren<EnemyUI>();
        enemyUI.SetName(EnemyName);
        enemyUI.SetHealth(health);
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

    virtual public void GotHit(int damage)
    {
        health -= damage;
        if (health <= 0 && !isdying)
            EnemyDying();

        enemyUI.SetHealth(health);

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

    virtual public void SomethingElseIsInTrigger()
    {
        if (noPlayerInSight)
        {
            agent.SetDestination(RandomNavmeshLocation());
        }
    }


    protected void OnCollisionEnter(Collision other)
    {
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
        agent.enabled = false;
    }

    protected void PlayParticles()
    {
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

    public Vector3 RandomNavmeshLocation()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomRadius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, randomRadius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
