using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction : MonoBehaviour
{
	[SerializeField] [Range(0.0f, 1.0f)] float activationChance;
	[SerializeField] GameObject miniGameObject;
	bool isActive = false;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		if (isActive)
		{
			IMiniGame miniGameComponent;
			if (miniGameObject && miniGameObject.TryGetComponent<IMiniGame>(out miniGameComponent))
			{
				if (miniGameComponent.IsFinished)
				{
					isActive = false;
					gameObject.SetActive(false);
				}
			}
		}
	}

	// What happens when the player tries to fix it
	public void Interact()
	{
		if (isActive)
		{
			// hook into the minigame and disable when completed
			IMiniGame miniGameComponent;
			if (miniGameObject && miniGameObject.TryGetComponent<IMiniGame>(out miniGameComponent))
			{
				// TODO: check that we're not already running the minigame
				miniGameComponent.StartMiniGame();
			}
			// if there's no minigame just disable the malfunction
			else
			{
				isActive = false;
				gameObject.SetActive(false);
			}
		}
	}

	// When called the malfunction is put in an active state
	public void Trigger()
	{
		isActive = true;
		// display broken icon
		gameObject.SetActive(true);
	}

	// called by the manager to start the malfunction
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
