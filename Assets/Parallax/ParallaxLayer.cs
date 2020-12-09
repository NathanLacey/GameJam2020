using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ParallaxLayer : MonoBehaviour
{
	public GameObject ReferenceObject { protected get; set; }
	public ParallaxSystem ParallaxSystem { protected get; set; }

	[SerializeField] protected List<GameObject> spawnPrefabs;
	readonly protected List<GameObject> objectPool = new List<GameObject>();
}
