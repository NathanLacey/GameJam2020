using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1;
    [SerializeField] InputAction MoveAction;

    Rigidbody2D mRigidbody;
    void Start()
    {
        MoveAction.Enable();
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mRigidbody.position += MoveAction.ReadValue<Vector2>() * MoveSpeed * Time.deltaTime;
    }
}
