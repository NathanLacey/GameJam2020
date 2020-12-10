using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparseParallaxLayer : ParallaxLayer
{
    [SerializeField] int spawnPoolSize;

    readonly System.Random randomGen = new System.Random(Guid.NewGuid().GetHashCode());
    [SerializeField] protected int spawnGridSize;
	[SerializeField] protected int totalSpawnAreaSize;
    int spawnGridCount;

    public struct Bounds
    {
        public readonly RectInt boundsRect;
        public bool Occupied { get; set; }
        public Bounds(RectInt rect)
        {
            boundsRect = rect;
            Occupied = false;
        }
    }

    void Start()
    {
        Bounds[,] spawnBounds;

        spawnGridCount = totalSpawnAreaSize / spawnGridSize;
        spawnBounds = new Bounds[spawnGridCount, spawnGridCount];
        for (int xPos = 0; xPos < spawnGridCount; ++xPos)
        {
            for (int yPos = 0; yPos < spawnGridCount; ++yPos)
            {
                spawnBounds[xPos, yPos] = new Bounds(new RectInt()
                {
                    x = xPos * spawnGridSize,
                    y = yPos * spawnGridSize,
                    width = spawnGridSize,
                    height = spawnGridSize
                });
            }
        }
        objectPool.Resize(spawnPoolSize);

        for (int i = 0; i < spawnPoolSize; ++i)
        {
            int randIndexX = 0;
            int randIndexY = 0;
            do
            {
                randIndexX = randomGen.Next(0, spawnGridCount);
                randIndexY = randomGen.Next(0, spawnGridCount);
            } while (spawnBounds[randIndexX, randIndexY].Occupied);

            ref Bounds bounds = ref spawnBounds[randIndexX, randIndexY];
            bounds.Occupied = true;
            float randomXOffset = randomGen.Next(-bounds.boundsRect.width * 40, bounds.boundsRect.width * 40) / 100.0f;
            float randomYOffset = randomGen.Next(-bounds.boundsRect.height * 40, bounds.boundsRect.height * 40) / 100.0f;
            var xPos = bounds.boundsRect.x - totalSpawnAreaSize / 2 + bounds.boundsRect.width / 2 + randomXOffset;
            var yPos = bounds.boundsRect.y - totalSpawnAreaSize / 2 + bounds.boundsRect.height / 2 + randomYOffset;
            var spawnPos = new Vector3(0,0,0);
            spawnPos.x += xPos;
            spawnPos.y += yPos;
            spawnPos.z = 0;

            int prefabIndex = randomGen.Next(0, spawnPrefabs.Count);
            objectPool[i] = Instantiate(spawnPrefabs[prefabIndex], spawnPos, Quaternion.identity, transform);
            float randomScale = randomGen.Next(75, 135) / 100.0f;
            objectPool[i].transform.localScale = new Vector3(randomScale, randomScale, 1.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < spawnPoolSize; ++i)
        {
            var pos = objectPool[i].transform.position;
            if (pos.x < -totalSpawnAreaSize / 2)
            {
                pos.x = totalSpawnAreaSize / 2;
            }
            objectPool[i].transform.position = pos;
        }
    }
}
