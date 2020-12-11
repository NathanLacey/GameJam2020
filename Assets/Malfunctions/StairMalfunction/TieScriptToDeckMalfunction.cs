using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieScriptToDeckMalfunction : MonoBehaviour
{
	enum DeckLevel
	{
		Top,
		Bottom
	}
	[SerializeField] MonoBehaviour controlledScript;
	[SerializeField] DeckLevel deckLevel;
	MalfunctionManager malfunctionManager = null;

	// Start is called before the first frame update
	void Start()
	{
		malfunctionManager = GameObject.FindObjectOfType<MalfunctionManager>();
		TrySetControlledEnableState();
	}

	// Update is called once per frame
	void Update()
	{
		TrySetControlledEnableState();
	}

	void TrySetControlledEnableState()
	{
		if (!malfunctionManager)
		{
			return;
		}
		switch (deckLevel)
		{
			case DeckLevel.Top:
				{
					controlledScript.enabled = malfunctionManager.hasBottomDeckMalfunctions;
					break;
				}
			case DeckLevel.Bottom:
				{
					controlledScript.enabled = malfunctionManager.hasTopDeckMalfunctions;
					break;
				}
			default:
				break;
		}
	}
}
