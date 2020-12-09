using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTriggered : MonoBehaviour
{
	[SerializeField] string TriggerTagName;

	[HideInInspector] public bool IsBeingTriggered = false;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == TriggerTagName)
		{
			IsBeingTriggered = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.tag == TriggerTagName)
		{
			IsBeingTriggered = false;
		}
	}
}
