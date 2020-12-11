using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSoundController : MonoBehaviour
{
	[SerializeField] GameObject shipFloorManagerObject;
	[SerializeField] [Range(0.0f, 1.0f)] float aboveDeckVolume = 1.0f;
	[SerializeField] [Range(0.0f, 1.0f)] float belowDeckVolume = 1.0f;
	ShipFloorManager floorManager = null;
	bool wasBelowDeck = false;

	// Start is called before the first frame update
	void Start()
	{
		if (shipFloorManagerObject && shipFloorManagerObject.TryGetComponent<ShipFloorManager>(out floorManager))
		{
			AudioSource engineAudio;
			if (gameObject.TryGetComponent<AudioSource>(out engineAudio))
			{
				if (floorManager.isLowerDeck)
				{
					UseLowerDeckAudio(engineAudio);
				}
				else
				{
					UseUpperDeckAudio(engineAudio);
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{
		AudioSource engineAudio;
		if (gameObject.TryGetComponent<AudioSource>(out engineAudio))
		{
			if (!wasBelowDeck && floorManager.isLowerDeck)
			{
				UseLowerDeckAudio(engineAudio);
			}
			else if (wasBelowDeck && !floorManager.isLowerDeck)
			{
				UseUpperDeckAudio(engineAudio);
			}
		}
	}

	void UseUpperDeckAudio(AudioSource engineAudio)
	{
		engineAudio.bypassEffects = false;
		engineAudio.volume = 0.3f;
		wasBelowDeck = false;
	}

	void UseLowerDeckAudio(AudioSource engineAudio)
	{
		engineAudio.bypassEffects = true;
		engineAudio.volume = 1.0f;
		wasBelowDeck = true;
	}
}
