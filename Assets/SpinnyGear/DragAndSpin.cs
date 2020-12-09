using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndSpin : MonoBehaviour
{
	[SerializeField] InputAction ClickAction;
	[SerializeField] InputAction DragAction;

	CircleCollider2D Collider;
	[SerializeField] int TotalSpinsNeeded = 3;
	[SerializeField] [Range(0.0f, 1.0f)] float DeprecationValue;
	[SerializeField] float TotalDistanceSpun = 0.0f;

	[SerializeField] bool SpinsClockWise = true;

	bool HasStartedDragging = false;
	float AngleAtDragStart;
	float TouchAngleAtDragStart;
	float LastRotationAngle;


	private void Start()
	{
		ClickAction.Enable();
		DragAction.Enable();
		Collider = GetComponent<CircleCollider2D>();
	}

	private void Update()
	{
		float deprecationAngle = Time.deltaTime * (DeprecationValue * 50.0f);
		TotalDistanceSpun -= SpinsClockWise ? deprecationAngle : -deprecationAngle;
		if (TotalDistanceSpun < 0.0f)
		{
			TotalDistanceSpun = 0.0f;
		}
		else
		{
			gameObject.transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + deprecationAngle, Vector3.forward);
		}

		RealDrag();

		if (IsFinishedSpinning())
		{
			Debug.Log("Done");
		}
	}

	bool IsFinishedSpinning()
	{
		return TotalDistanceSpun / 720.0f >= TotalSpinsNeeded;
	}

	void RealDrag()
	{
		float isDragging = ClickAction.ReadValue<float>();
		if (isDragging > 0.0f)
		{
			Vector2 mousePosition = DragAction.ReadValue<Vector2>();
			var screenSpaceTransform = Camera.main.WorldToScreenPoint(transform.position);

			if (!HasStartedDragging)
			{
				HasStartedDragging = true;
				AngleAtDragStart = transform.rotation.eulerAngles.z;
				LastRotationAngle = AngleAtDragStart;
				TouchAngleAtDragStart = Mathf.Atan2(mousePosition.y - screenSpaceTransform.y, mousePosition.x - screenSpaceTransform.x) * Mathf.Rad2Deg;
			}

			if (Collider.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePosition)))
			{
				var directionFromMouseToCenter = mousePosition - new Vector2(screenSpaceTransform.x, screenSpaceTransform.y);
				float newRot = Mathf.Atan2(directionFromMouseToCenter.y, directionFromMouseToCenter.x) * Mathf.Rad2Deg;
				float RotationAngle = AngleAtDragStart + newRot - TouchAngleAtDragStart;

				gameObject.transform.rotation = Quaternion.AngleAxis(RotationAngle, Vector3.forward);

				TotalDistanceSpun += Mathf.Abs(LastRotationAngle - RotationAngle);
				LastRotationAngle = RotationAngle;
			}
		}
		else
		{
			HasStartedDragging = false;
		}
	}
}
