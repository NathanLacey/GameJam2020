using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputAction inputAction;
    [SerializeField] GameObject pauseMenu;
	[SerializeField] GameManager gameManager;
	private void Start()
	{
		inputAction.Enable();
		
	}
	public void OnPauseMenu()
	{
        pauseMenu.SetActive(!pauseMenu.activeSelf);
		gameManager.Paused = pauseMenu.activeSelf;
	}

	private void Update()
	{
		if (inputAction.triggered)
		{
			OnPauseMenu();
		}
	}
}
