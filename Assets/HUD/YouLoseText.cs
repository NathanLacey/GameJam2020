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
			if(trans.name == "Score")
			{
				trans.gameObject.GetComponent<UnityEngine.UI.Text>().text = "Score: " + gameManager.CurrentScore;
			}
		}
	}
}
