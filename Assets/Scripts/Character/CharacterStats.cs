using UnityEngine;

public class CharacterStats : MonoBehaviour
{
	public int maxHealth = 10;
	public int currentHealth { get; private set; }

	public StatsUI statsUI;
	public PlayerController player;

	public int ammoGrenade = 0;
	public int damage;
	public int armor;


	private void Start()
	{
		currentHealth = maxHealth - 3;
		statsUI.SetMaxHealth(maxHealth);
		statsUI.SetHealth(currentHealth);
		statsUI.SetAmmo(ammoGrenade);
	}


	public void TakeDamage(int damage)
	{
		damage -= armor;
		damage = Mathf.Clamp(damage, 0, int.MaxValue);

		currentHealth -= damage;
		Debug.Log(transform.name + " take " + damage + " damage.");

		statsUI.SetHealth(currentHealth);

		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void Heal(int healingAmount)
	{
		if(healingAmount + currentHealth <= maxHealth)
		{
			currentHealth += healingAmount;
		}
		statsUI.SetHealth(currentHealth);
	}

	public void Armor(int armorAmount)
	{
		if (armorAmount + armor <= maxHealth)
		{
			armor += armorAmount;
		}
		statsUI.SetArmor(armor);
	}

	public void UpgradeMaxHealth(int upgradeAmount)
	{
		maxHealth += upgradeAmount;
		statsUI.SetMaxHealth(maxHealth);
	}

	public void SetGrenadeAmmo(int grenadeAmount)
	{
		ammoGrenade += grenadeAmount;
		statsUI.SetAmmo(ammoGrenade);
	}

	public virtual void Die()
	{
		//Die in some way
		//this method is meant to be overwritten
		Debug.Log(transform.name + " died");
	}
}
