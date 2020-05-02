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

	char uniformChar = 'I';


	public void SetMaxHealth(int tobeMaxHealth)
	{
		maxHealth.text = string.Concat(Enumerable.Repeat(uniformChar, tobeMaxHealth));
	}

	public void SetHealth(int health)
	{
		if (health == 0)
			textHealth.text = string.Empty;
		else
			textHealth.text = string.Concat(Enumerable.Repeat(uniformChar, health));
	}

	public void SetAmmo(int ammo)
	{
		if (ammo == 0)
			textAmmo.text = string.Empty;
		else
			textAmmo.text = string.Concat(Enumerable.Repeat(uniformChar, ammo));
	}

}
