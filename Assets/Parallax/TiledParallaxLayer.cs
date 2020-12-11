using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledParallaxLayer : ParallaxLayer
{
	[SerializeField] int horGridCount;
	[SerializeField] int vertGridCount;
	[SerializeField] float tileWidth;
	[SerializeField] float tileHeight;
    readonly System.Random randomGen = new System.Random(Guid.NewGuid().GetHashCode());
	float horGridSize;
	float vertGridSize;
	private void Start()
	{
		horGridSize = horGridCount * tileWidth;
		vertGridSize = vertGridCount * tileHeight;
		objectPool.Resize(horGridCount * vertGridCount);

		for (int xPos = 0; xPos < horGridCount; ++xPos)
		{
			for (int yPos = 0; yPos < vertGridCount; ++yPos)
			{
				var spawnPos = ReferenceObject.transform.position;
				float x = xPos * tileWidth - horGridSize / 2.0f;
				float y = yPos * tileHeight - vertGridSize / 2.0f;
				spawnPos.x += x;
				spawnPos.y += y;
				spawnPos.z = 0;
				int prefabIndex = randomGen.Next(0, spawnPrefabs.Count);
				objectPool[yPos * horGridCount + xPos] = Instantiate(spawnPrefabs[prefabIndex], spawnPos, Quaternion.identity, transform);
			}
		}
	}

	private void Update()
	{
		for(int i = 0; i < objectPool.Count; ++i)
		{
			var pos = objectPool[i].transform.position;
			if (pos.x < -horGridSize / 2)
			{
				pos.x += horGridSize;
				objectPool[i].transform.position = pos;
			}
		}
	}
}
