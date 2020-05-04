using UnityEngine;

public class SendParticleCollision : MonoBehaviour
{
    [SerializeField]
    Enemy thisEnemy;


    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collision");
        if(other.GetComponent<PlayerController>())
        {
            thisEnemy.DamagePlayer();
        }
    }

    
}
