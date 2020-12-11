using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalfunctionManager : MonoBehaviour
{
	public List<Malfunction> malfunctions = new List<Malfunction>();
	int malfunctionIndex;
	[SerializeField] [Range(0.0f, 100.0f)] float malfunctionRate;
	float nextMalfunction;

	int numTopDeckMalfunctions = 0;
	int numBottomDeckMalfunctions = 0;
	public bool hasTopDeckMalfunctions { get { return numTopDeckMalfunctions > 0; } }
	public bool hasBottomDeckMalfunctions { get { return numBottomDeckMalfunctions > 0; } }

	public int CriticalMalfunctionCount { get { return numBottomDeckMalfunctions + numTopDeckMalfunctions; } }

	void Awake()
	{
		
		malfunctions.AddRange(Resources.FindObjectsOfTypeAll<Malfunction>());
		malfunctions.RemoveAll(malfunction => malfunction.gameObject.scene.rootCount == 0);
		malfunctions.RemoveAll(malfunction => malfunction.activationChance == 0);
		malfunctions.Shuffle();
		malfunctions.ForEach(malfunction => malfunction.gameObject.SetActive(false));
		nextMalfunction = Time.fixedTime + malfunctionRate;
	}

	// Update is called once per frame
	void FixedUpdate()
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
				malfunctionTriggered = malfunctions[malfunctionIndex].TryActivate();
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

		numTopDeckMalfunctions = 0;
		numBottomDeckMalfunctions = 0;
		foreach (var malfunc in malfunctions)
		{
			if (malfunc.gameObject.activeSelf)
			{
				if (malfunc.transform.parent.name == "ShipTopDeck")
				{
					++numTopDeckMalfunctions;
				}
				else
				{
					++numBottomDeckMalfunctions;
				}
			}
		}
	}
}
