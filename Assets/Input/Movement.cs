using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] float MoveSpeed = 1;
    [SerializeField] InputAction MoveAction;

    Rigidbody2D mRigidbody;

    public Vector2 MoveDirection { get { return MoveAction.ReadValue<Vector2>(); } }
    void Start()
    {
        MoveAction.Enable();
        mRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        mRigidbody.position += MoveDirection * MoveSpeed * Time.deltaTime;
    }
}
