using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth { get; private set; }

	public HealthBar healthBar;

	public Stat damage;
	public Stat armor;

	private void Awake()
	{
		currentHealth = maxHealth;
		healthBar.SetMaxHealth(maxHealth);
	}

	private void Update()
	{
		healthBar.count = currentHealth;
	}

	public void TakeDamage(int damage)
	{
		damage -= armor.GetValue();
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHealth -= damage;
		Debug.Log(transform.name + " take " + damage + " damage.");

		healthBar.SetHealth(currentHealth);

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
