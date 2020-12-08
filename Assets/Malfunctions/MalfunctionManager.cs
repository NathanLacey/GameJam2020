using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalfunctionManager : MonoBehaviour
{
	List<Malfunction> malfunctions;
	[SerializeField] [Range(0.0f, 100.0f)] float malfunctionRate;

	void Start()
	{
		malfunctions.AddRange(GameObject.FindObjectsOfType<Malfunction>());
	}

	// Update is called once per frame
	void Update()
	{

	}
}
