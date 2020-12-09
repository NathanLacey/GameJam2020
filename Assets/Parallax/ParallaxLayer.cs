using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnPrefabs;
    [SerializeField] int totalSpawnAreaSize;
    [SerializeField] int spawnGridSize;
    [SerializeField] int spawnPoolSize;

    public GameObject ReferenceObject { private get; set; }
    public ParallaxSystem parallaxSystem{ private get; set; }

    readonly List<GameObject> objectPool = new List<GameObject>();
    readonly System.Random randomGen = new System.Random(Guid.NewGuid().GetHashCode());
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
    Bounds[,] spawnBounds;
    void Start()
    {
        spawnGridCount = totalSpawnAreaSize / spawnGridSize;
        spawnBounds = new Bounds[spawnGridCount, spawnGridCount];
        for(int xPos = 0; xPos < spawnGridCount; ++xPos)
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
            var xPos = bounds.boundsRect.x - totalSpawnAreaSize / 2 + bounds.boundsRect.width / 2;
            var yPos = bounds.boundsRect.y - totalSpawnAreaSize / 2 + bounds.boundsRect.height / 2;
            var spawnPos = ReferenceObject.transform.position;
            spawnPos.x += xPos;
            spawnPos.y += yPos;
            spawnPos.z = 0;

            int prefabIndex = randomGen.Next(0, spawnPrefabs.Count);
            objectPool[i] = Instantiate(spawnPrefabs[prefabIndex], spawnPos, Quaternion.identity, transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check all gameobjects to see if position is outside the bounds of the camera
         
        // If outside bounds disable gameobject and move to end of list

        // For all disabled objects, spawn new gameobjects based on wind direction
    }
}
