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
	[SerializeField] List<ParallaxLayer> parallaxLayers;
	[SerializeField] List<float> parallaxControls;

	// Start is called before the first frame update
	void OnValidate()
	{ 
		parallaxControls.Resize(parallaxLayers.Count);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
