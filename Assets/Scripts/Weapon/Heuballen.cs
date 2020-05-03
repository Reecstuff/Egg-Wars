using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Heuballen : MonoBehaviour
{
	public float radius = 6f;
	public float force = 700f;

	public int damage = 80;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.GetComponent<Enemy>())
		{
			Explode();
		}
		if (other.gameObject.GetComponentInParent<DungeonGenerator>())
		{
			Explode();
		}
	}

	void Explode()
	{
		Collider[] colliderToDestroy = Physics.OverlapSphere(transform.position, radius);

		foreach (Collider nearbyObj in colliderToDestroy)
		{
			Enemy destruct = nearbyObj.GetComponent<Enemy>();
			if (destruct != null)
			{
				destruct.health -= damage;
			}
		}

		Collider[] colliderToMove = Physics.OverlapSphere(transform.position, radius);

		foreach (Collider nearbyObj in colliderToMove)
		{
			Rigidbody rb = nearbyObj.GetComponent<Rigidbody>();
			if (rb != null)
			{
				rb.AddExplosionForce(force, transform.position, radius);
			}
		}

		Destroy(gameObject);
	}
}
