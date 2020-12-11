using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FacingDirection : MonoBehaviour
{
    [SerializeField] InputAction LookAction;
    // Start is called before the first frame update
    void Start()
    {
        LookAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = LookAction.ReadValue<Vector2>();
        if(!Camera.main)
		{
            return;
		}
        Vector2 originPosition = Camera.main.WorldToScreenPoint(transform.position);
        Vector2 directionVector = mousePosition - originPosition;
        float angle = Mathf.Atan2(directionVector.x, directionVector.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(-angle + 90, Vector3.forward);
    }
}
