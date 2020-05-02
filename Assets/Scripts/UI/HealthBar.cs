using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Text textHealth;
	public int count;

	void Start()
	{
		textHealth = gameObject.GetComponentInChildren<Text>();
	}

	private void Update()
	{
		textHealth.text = count.ToString();
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
