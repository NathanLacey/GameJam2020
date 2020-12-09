using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalfunctionManager : MonoBehaviour
{
	public static System.Random random = new System.Random();

	List<Malfunction> malfunctions = new List<Malfunction>();
	int malfunctionIndex;
	[SerializeField] [Range(0.0f, 100.0f)] float malfunctionRate;
	float nextMalfunction;

	void Start()
	{
		malfunctions.AddRange(Resources.FindObjectsOfTypeAll<Malfunction>());
		malfunctions.Shuffle();
		nextMalfunction = Time.fixedTime + malfunctionRate;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.fixedTime >= nextMalfunction)
		{
			if (malfunctions.Count < 1)
			{
				return;
			}

			int startingIndex = malfunctionIndex;
			bool malfunctionTriggered = false;
			// keep trying to trigger malfunctions until one happens or we've tried all of them
			while (!malfunctionTriggered)
			{
				malfunctionTriggered = malfunctions[malfunctionIndex].TryActivate(random);
				if (++malfunctionIndex >= malfunctions.Count)
				{
					malfunctionIndex = 0;
					malfunctions.Shuffle();
				}
				// stop if we've made it full circle
				if (malfunctionIndex == startingIndex)
				{
					break;
				}
			}

			nextMalfunction = Time.fixedTime + malfunctionRate;
		}
	}
}
