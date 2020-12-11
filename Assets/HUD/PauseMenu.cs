using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputAction inputAction;
    [SerializeField] GameObject pauseMenu;

	private void Start()
	{
		inputAction.Enable();
		
	}
	void OnPauseMenu()
	{
        pauseMenu.SetActive(!pauseMenu.activeSelf);
	}

	private void Update()
	{
		if (inputAction.triggered)
		{
			OnPauseMenu();
		}
	}
}
