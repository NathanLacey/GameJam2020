﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerMiniGame : MonoBehaviour, IMiniGame
{
    [SerializeField] SpinPropeller WorkingPropeller;
    [SerializeField] FixPropeller BrokenPropeller;

    public bool IsFinished { get { return BrokenPropeller.IsFinished; } }

	private void Update()
	{
		if(IsFinished)
		{
			BrokenPropeller.gameObject.SetActive(false);
			WorkingPropeller.gameObject.SetActive(true);
		}
	}

	private void OnEnable()
	{
		Reset();
	}

	void Reset()
	{
		BrokenPropeller.gameObject.SetActive(true);
		WorkingPropeller.gameObject.SetActive(false);
	}

	public void StartMiniGame()
	{
		Reset();
	}
}
