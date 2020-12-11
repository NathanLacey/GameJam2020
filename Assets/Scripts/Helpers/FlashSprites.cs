using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashSprites : MonoBehaviour
{
	[SerializeField] float flashIntervalSeconds = 2.0f;
	float elapsedSeconds = 0.0f;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		elapsedSeconds += Time.deltaTime;
		if (elapsedSeconds >= flashIntervalSeconds)
		{
			SpriteRenderer[] renderers = GetComponentsInChildren<SpriteRenderer>();
			foreach(SpriteRenderer sprite in renderers)
			{
				sprite.enabled = !sprite.enabled;
			}
			elapsedSeconds = 0.0f;
		}
	}
}
