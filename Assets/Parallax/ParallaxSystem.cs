using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtra
{
	public static void Resize<T>(this List<T> list, int size) where T : class
	{
		int cur = list.Count;
		if (size < cur)
		{
			list.RemoveRange(size, cur - size);
		}
		else if (size > cur)
		{
			if (size > list.Capacity)
			{
				list.Capacity = size;
			}
			int countToAdd = size - cur;
			for (int i = 0; i < countToAdd; ++i)
			{
				list.Add(null);
			}
		}
	}
	public static void Resize(this List<int> list, int size)
	{
		int cur = list.Count;
		if (size < cur)
		{
			list.RemoveRange(size, cur - size);
		}
		else if (size > cur)
		{
			if (size > list.Capacity)
			{
				list.Capacity = size;
			}
			int countToAdd = size - cur;
			for (int i = 0; i < countToAdd; ++i)
			{
				list.Add(0);
			}
		}
	}
	public static void Resize(this List<float> list, int size)
	{
		int cur = list.Count;
		if (size < cur)
		{
			list.RemoveRange(size, cur - size);
		}
		else if (size > cur)
		{
			if (size > list.Capacity)//this bit is purely an optimisation, to avoid multiple automatic capacity changes.
			{
				list.Capacity = size;
			}
			int countToAdd = size - cur;
			for (int i = 0; i < countToAdd; ++i)
			{
				list.Add(0.0f);
			}
		}
	}
}

public class ParallaxSystem : MonoBehaviour
{
	[SerializeField] GameObject referenceObject;
	[SerializeField] Vector3 wind = Vector3.zero;
	Vector3 prevRefPos;

	[Header("Layers")]
	[SerializeField] List<ParallaxLayer> parallaxLayers;
	[SerializeField] List<float> parallaxControls;

	void OnValidate()
	{ 
		parallaxControls.Resize(parallaxLayers.Count);
	}
	private void Start()
	{
		prevRefPos = referenceObject.transform.position;
	}

	void Update()
	{
		float diminishingMultiplier = 1.0f;
		float incrementalMultiplier = 0.15f;
		Vector3 currentMovement = (referenceObject.transform.position - prevRefPos);
		prevRefPos = referenceObject.transform.position;
		for(int i = 0; i < parallaxLayers.Count; ++i)
		{
			float multiplier = parallaxControls[i];
			diminishingMultiplier *= multiplier;
			incrementalMultiplier /= multiplier;

			var layer = parallaxLayers[i];
			var position = layer.transform.position;
			position += wind * diminishingMultiplier * Time.deltaTime;
			position += currentMovement * incrementalMultiplier;
			position.z = i + 1;
			layer.transform.position = position;
		}
	}
}
