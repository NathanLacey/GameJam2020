using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPropeller : MonoBehaviour
{
    [SerializeField] DragAndSpin Gear;
    [SerializeField] int TotalSpinsToFix = 1;
	public bool IsFinished { get { return Gear.TotalTimesSpun >= TotalSpinsToFix; } }

	private void OnEnable()
	{
		Gear.Reset();
	}

	private void OnDisable()
	{
		Gear.enabled = false;
	}
}
