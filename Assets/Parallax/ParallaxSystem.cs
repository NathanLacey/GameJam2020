using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxSystem : MonoBehaviour
{
	[SerializeField] GameObject referenceObject;
	[SerializeField] Vector3 wind = Vector3.zero;
	Vector3 prevRefPos;

	[Header("Layers")]
	[SerializeField] List<ParallaxLayer> parallaxLayers;
	[SerializeField] List<float> parallaxControls;


	public Vector3 WindDir
	{
		get { return wind; }
	}
	public Vector3 PrevRefPosition
	{
		get { return prevRefPos; }
	}
	void OnValidate()
	{ 
		parallaxControls.Resize(parallaxLayers.Count);
		
	}

	private void Awake()
	{
		foreach (var layer in parallaxLayers)
		{
			layer.ReferenceObject = referenceObject;
		}
	}
	private void Start()
	{
	}

	void Update()
	{
		float diminishingMultiplier = 1.0f;
		prevRefPos = referenceObject.transform.position;
		for(int i = 0; i < parallaxLayers.Count; ++i)
		{
			float multiplier = parallaxControls[i];
			diminishingMultiplier *= multiplier;

			var layer = parallaxLayers[i];
			var position = layer.transform.position;
			position += wind * diminishingMultiplier * Time.deltaTime;
			position.z = i + 1;
			layer.transform.position = position;
		}
	}
}

