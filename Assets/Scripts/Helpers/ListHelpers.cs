using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListHelpers
{
	private static System.Random rng = new System.Random();

	public static void Shuffle<T>(this List<T> list) where T : class
	{
		for (int i = list.Count; i > 1; --i)
		{
			int k = rng.Next(i + 1);
			T value = list[k];
			list[k] = list[i];
			list[i] = value;
		}
	}

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
			if (size > list.Capacity)
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
