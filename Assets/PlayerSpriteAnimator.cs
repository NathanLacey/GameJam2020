using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAnimator : MonoBehaviour
{
	[SerializeField] Animator PlayerAnimator;
	[SerializeField] FacingDirection Rotator;
	void Update()
	{
		float playerAngle = Rotator.transform.rotation.eulerAngles.z;
		PlayerAnimator.SetInteger("Direction", (int)((playerAngle + 45.0f) / 90.0f) % 4);
	}
}
