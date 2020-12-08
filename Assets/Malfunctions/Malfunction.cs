﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Malfunction : MonoBehaviour
{
	[SerializeField] [Range(0.0f, 1.0f)] float activationChance;
	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Trigger()
	{

	}

	public bool TryActivate(System.Random random)
	{
		if (random.NextDouble() <= activationChance)
		{
			Trigger();
			return true;
		}
		return false;
	}
}
