using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction : MonoBehaviour
{
	[SerializeField] [Range(0.0f, 1.0f)] float activationChance;
	bool isActive = false;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	// What happens when the player tries to fix it
	public void Interact()
	{
		if (isActive)
		{
			// hook into the minigame and disable when completed
			gameObject.SetActive(false);
		}
	}

	// When called the malfunction is put in an active state
	public void Trigger()
	{
		isActive = true;
		// display broken icon
		gameObject.SetActive(true);
	}

	public bool TryActivate(System.Random random)
	{
		if (!isActive && random.NextDouble() <= activationChance)
		{
			Trigger();
			return true;
		}
		return false;
	}
}
