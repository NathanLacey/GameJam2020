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
}
