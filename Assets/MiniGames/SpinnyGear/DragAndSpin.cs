using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndSpin : MonoBehaviour
{
	[SerializeField] InputAction ClickAction;
	[SerializeField] InputAction DragAction;

	CircleCollider2D Collider;
	[SerializeField] [Range(0.0f, 1.0f)] public float DeprecationValue;
	float TotalDistanceSpun = 0.0f;

	public int TotalTimesSpun { get { return (int)(TotalDistanceSpun / 720.0f); } }

	[SerializeField] public bool AutoSpin = false;

	bool HasStartedDragging = false;
	float AngleAtDragStart = 0.0f;
	float TouchAngleAtDragStart = 0.0f;
	float LastRotationAngle = 0.0f;
	Quaternion OriginalRotation = Quaternion.identity;

	private void Start()
	{
		ClickAction.Enable();
		DragAction.Enable();
		Collider = GetComponent<CircleCollider2D>();
		OriginalRotation = gameObject.transform.rotation;
	}

	private void Update()
	{
		float deprecationAngle = Time.deltaTime * (DeprecationValue * 50.0f);
		TotalDistanceSpun -= AutoSpin ? -deprecationAngle : deprecationAngle;
		if (TotalDistanceSpun < 0.0f)
		{
			TotalDistanceSpun = 0.0f;
		}
		else
		{
			gameObject.transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.z + deprecationAngle, Vector3.forward);
		}

		RealDrag();
	}

	public void Reset()
	{
		HasStartedDragging = false;
		AngleAtDragStart = 0.0f;
		TouchAngleAtDragStart = 0.0f;
		LastRotationAngle = 0.0f;
		TotalDistanceSpun = 0.0f;
		gameObject.transform.rotation = OriginalRotation;
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
