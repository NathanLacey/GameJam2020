using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinOnClickMainMenu : MonoBehaviour
{
    [SerializeField] InputAction ClickAction;
	[SerializeField] InputAction LookAction;

	CircleCollider2D Collider;
	[SerializeField] int TotalSpinsNeeded = 3;
	float TotalDistanceSpun = 0.0f;

	bool HasStartedDragging = false;
	float AngleAtDragStart;
	float TouchAngleAtDragStart;
	float LastRotationAngle;

	private void Start()
	{
		ClickAction.Enable();
		LookAction.Enable();
		Collider = GetComponent<CircleCollider2D>();
	}

	private void Update()
	{
		RealDrag();

		if (IsFinishedSpinning())
		{
			TransitionToScene();
		}
	}
	void TransitionToScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("Ships");
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
			Vector2 mousePosition = LookAction.ReadValue<Vector2>();
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
