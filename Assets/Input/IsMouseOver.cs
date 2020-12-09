using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class IsMouseOver : MonoBehaviour
{
	[SerializeField] string ObjectTag;

	[HideInInspector] public bool MouseIsOver = false;
	
	private void Update()
	{
		CheckIfMouseIsOver();
	}

	void CheckIfMouseIsOver()
	{
		Vector2 mousePosition = Mouse.current.position.ReadValue();
		var ray = Camera.main.ScreenPointToRay(mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 1000))
		{
			if (hit.collider.tag == ObjectTag)
			{
				MouseIsOver = true;
				return;
			}
		}

		MouseIsOver = false;
	}
}
