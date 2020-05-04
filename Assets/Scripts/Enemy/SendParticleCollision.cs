using UnityEngine;

public class SendParticleCollision : MonoBehaviour
{
    [SerializeField]
    Enemy thisEnemy;


    private void OnParticleCollision(GameObject other)
    {
        if(other.GetComponent<PlayerController>())
        {
            thisEnemy?.DamagePlayer();
        }
    }

    
}
