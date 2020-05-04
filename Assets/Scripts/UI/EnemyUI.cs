using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{ 
	[SerializeField]
	TextMeshProUGUI nametext;
	[SerializeField]
	TextMeshProUGUI textHealth;

	char uniformChar = 'I';


	public void SetName(string name)
	{
		nametext.text = name;
	}

	public void SetHealth(int health)
	{
		if (health == 0)
			textHealth.text = string.Empty;
		else
			textHealth.text = string.Concat(Enumerable.Repeat(uniformChar,Mathf.RoundToInt(health / 10)));
	}
}
