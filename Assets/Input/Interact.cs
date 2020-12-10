using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interact : MonoBehaviour
{
	[SerializeField] InputAction interactAction;

	// Start is called before the first frame update
	void Start()
	{
		interactAction.Enable();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("Malfunction"))
		{
			if (interactAction.ReadValue<float>() > 0)
			{
				collision.gameObject.GetComponent<Malfunction>().Interact(gameObject);
			}
		}
	}
}
