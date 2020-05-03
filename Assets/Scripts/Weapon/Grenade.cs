using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : EquipAbleItem
{
	public float delay = 1f;
	public float radius = 6f;
	public float force = 70f;
	public int damage = 100;

	float countdown;
	bool hasExploded = false;

	private void Start()
	{
		countdown = delay;
	}

	private void Update()
	{
		countdown -= Time.deltaTime;
		if (countdown <= 0f && !hasExploded)
		{
			Explode();
			hasExploded = true;
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
				Debug.Log("Damage");
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
