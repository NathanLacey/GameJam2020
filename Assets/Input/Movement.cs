using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float MoveSpeed = 1;
    Rigidbody2D mRigidbody;
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        mRigidbody.AddForce(context.action.ReadValue<Vector2>() * MoveSpeed, ForceMode2D.Impulse);
    }
}
