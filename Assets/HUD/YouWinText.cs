using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouWinText : MonoBehaviour
{
	[SerializeField] GameManager gameManager;

	private void Start()
	{
		gameManager.OnGameComplete += ActivateText;
	}

	void ActivateText()
	{
		foreach (Transform trans in transform)
		{
			trans.gameObject.SetActive(true);
		}
	}
}
