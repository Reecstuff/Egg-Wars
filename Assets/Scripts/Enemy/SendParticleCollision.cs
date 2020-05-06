using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SendParticleCollision : MonoBehaviour
{
    [SerializeField]
    Enemy thisEnemy;

    [SerializeField]
    ParticleSystem splatterParticles;

    ParticleSystem thisParticleSystem;

    List<ParticleCollisionEvent> collisionEvents;

    private void Start()
    {
        thisParticleSystem = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }


    private void OnParticleCollision(GameObject other)
    {

        ParticlePhysicsExtensions.GetCollisionEvents(thisParticleSystem, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }
        
        if(other.GetComponent<PlayerController>())
        {
            thisEnemy?.DamagePlayer();
        }
    }

    void EmitAtLocation(ParticleCollisionEvent pEvent)
    {
        splatterParticles.transform.position = pEvent.intersection;
        splatterParticles.transform.rotation = Quaternion.LookRotation(pEvent.normal);
        splatterParticles.Play();
    }

    
}
