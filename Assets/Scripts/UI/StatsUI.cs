using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
	public Slider slider;

	public GameObject health;
	public GameObject ammo;

	public Text textHealth;
	public int countHealth;

	public Text textAmmo;
	public int countGrenade;

	void Start()
	{
		textHealth = health.GetComponent<Text>();
		textAmmo = ammo.GetComponent<Text>();
	}

	private void Update()
	{
		textHealth.text = countHealth.ToString();
		textAmmo.text = countGrenade.ToString();
	}

	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
	}

	public void SetHealth(int health)
	{
		slider.value = health;
	}
}
