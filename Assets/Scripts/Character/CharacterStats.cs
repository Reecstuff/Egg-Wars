﻿using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public Stat damage;
	public Stat armor;

	private void Awake()
	{
		currentHealth = maxHealth;
	}

	public void TakeDamage(int damage)
	{
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHealth -= damage;
		Debug.Log(transform.name + " take " + damage + " damage.");

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public virtual void Die()
	{
		//Die in some way
		//this method is meant to be overwritten
		Debug.Log(transform.name + " died");
	}
}
