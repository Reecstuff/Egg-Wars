using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : EquipAbleItem
{
	public int armor;

	public void SetArmor()
	{
		GetComponentInParent<CharacterStats>().Armor(armor);
	}
}
