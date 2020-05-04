using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{

	public TextMeshProUGUI textHealth;
	public TextMeshProUGUI textAmmo;
	[SerializeField]
	TextMeshProUGUI maxHealth;
	[SerializeField]
	TextMeshProUGUI textArmor;

	char uniformChar = 'I';
	char armorChar = '\\' ;

	public void SetMaxHealth(int tobeMaxHealth)
	{
		maxHealth.text = string.Concat(Enumerable.Repeat(uniformChar, tobeMaxHealth));
	}

	public void SetHealth(int health)
	{
		if (health <= 0)
			textHealth.text = string.Empty;
		else
			textHealth.text = string.Concat(Enumerable.Repeat(uniformChar, health));
	}

	public void SetAmmo(int ammo)
	{
		if (ammo <= 0)
			textAmmo.text = string.Empty;
		else
			textAmmo.text = string.Concat(Enumerable.Repeat(uniformChar, ammo));
	}

	public void SetArmor(int armor)
	{
		if (armor <= 0)
			textArmor.text = string.Empty;
		else
			textArmor.text = string.Concat(Enumerable.Repeat(armorChar, armor));
	}

}
