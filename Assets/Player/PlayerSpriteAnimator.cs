using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteAnimator : MonoBehaviour
{
	[SerializeField] Animator PlayerAnimator;
	[SerializeField] FacingDirection Rotator;
	[SerializeField] Movement PlayerMovement;
	void Update()
	{
		float playerAngle = Rotator.transform.rotation.eulerAngles.z;
		int playerDirection = (int)((playerAngle + 45.0f) / 90.0f) % 4;
		PlayerAnimator.SetFloat("Angle", playerDirection / 3f);
		float isMoving = PlayerMovement.MoveDirection.sqrMagnitude;
		PlayerAnimator.SetBool("IsWalking", isMoving > 0f);
	}
}
