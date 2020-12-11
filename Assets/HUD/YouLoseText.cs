using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouLoseText : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	private void Start()
	{
		gameManager.OnGameOver += ActivateText;
	}

	void ActivateText()
	{
		foreach(Transform trans in transform)
		{
			trans.gameObject.SetActive(true);
		}
	}
}
