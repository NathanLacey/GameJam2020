using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinOnClick : MonoBehaviour
{
    [SerializeField] InputAction ClickAction;
	[SerializeField] InputAction LookAction;

	CircleCollider2D Collider;
	[SerializeField] float TotalDistanceSpun = 0.0f;

	bool HasStartedDragging = false;
	float AngleAtDragStart;
	float TouchAngleAtDragStart;
	private void Start()
	{
		ClickAction.Enable();
		LookAction.Enable();
		Collider = GetComponent<CircleCollider2D>();
	}

	private void Update()
	{
		RealDrag();
	}

	void FakeDrag()
	{
		float isDragging = ClickAction.ReadValue<float>();
		if (isDragging > 0.0f)
		{
			Vector2 mousePosition = LookAction.ReadValue<Vector2>();
			if (Collider.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePosition)))
			{
				Vector2 gearPosition = Camera.main.WorldToScreenPoint(transform.position);
				Vector3 v3 = mousePosition - gearPosition;
				float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
				transform.eulerAngles = new Vector3(0, 0, angle);
				TotalDistanceSpun += angle;
			}

			if(IsFinishedSpinning())
			{
				TransitionToScene();
			}
		}
	}

	void TransitionToScene()
	{
		UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
	}

	bool IsFinishedSpinning()
	{
		return TotalDistanceSpun > 3000.0f;
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
				TouchAngleAtDragStart = Mathf.Atan2(mousePosition.y - screenSpaceTransform.y, mousePosition.x - screenSpaceTransform.x) * Mathf.Rad2Deg;
			}

			if (Collider.OverlapPoint(Camera.main.ScreenToWorldPoint(mousePosition)))
			{
				var directionFromMouseToCenter = mousePosition - new Vector2(screenSpaceTransform.x, screenSpaceTransform.y);
				float newRot = Mathf.Atan2(directionFromMouseToCenter.y, directionFromMouseToCenter.x) * Mathf.Rad2Deg;
				float RotationAngle = AngleAtDragStart + newRot - TouchAngleAtDragStart;
				gameObject.transform.rotation = Quaternion.AngleAxis(RotationAngle, Vector3.forward);
			}
		}
		else
		{
			HasStartedDragging = false;
		}
	}
}
