using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeCombat : MonoBehaviour
{
	Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			anim.SetBool("attacking", true);
		}
		else if (Input.GetKeyUp(KeyCode.Space))
		{
			anim.SetBool("attacking", false);
		}
	}
}
