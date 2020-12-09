using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPropeller : MonoBehaviour
{
    DragAndSpin Gear;
    [SerializeField] int TotalSpinsToFix = 1;
	public bool IsFinished { get { return Gear.TotalTimesSpun >= TotalSpinsToFix; } }

	// Start is called before the first frame update
	void Start()
    {
        Gear = GetComponent<DragAndSpin>();
    }

	private void OnDisable()
	{
		Gear.Reset();
	}
}
