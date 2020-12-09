using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction : MonoBehaviour
{
	[SerializeField] [Range(0.0f, 1.0f)] float activationChance;
	[SerializeField] GameObject miniGameObject;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		IMiniGame miniGameComponent;
		if (miniGameObject && miniGameObject.TryGetComponent<IMiniGame>(out miniGameComponent))
		{
			if (miniGameComponent.IsFinished)
			{
				gameObject.SetActive(false);
			}
		}
	}

	// What happens when the player tries to fix it
	public void Interact()
	{
		if (gameObject.activeSelf)
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
				gameObject.SetActive(false);
			}
		}
	}

	// When called the malfunction is put in an active state
	public void Trigger()
	{
		// display broken icon
		gameObject.SetActive(true);
	}

	// called by the manager to start the malfunction
	public bool TryActivate(System.Random random)
	{
		if (!gameObject.activeSelf && random.NextDouble() <= activationChance)
		{
			Trigger();
			return true;
		}
		return false;
	}
}
