using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	MalfunctionManager malfunctionManager;
	[SerializeField] int maxMalfunctions = 5;
	[SerializeField] float timeToComplete = 5 * 60; // 5 minutes
	bool isGameOver = false;

	float currentTime = 0.0f;
	int currentScore = 0;

	public delegate void GameCondition();
	public event GameCondition OnGameComplete;
	public event GameCondition OnGameOver;

	public float CurrentTravelProgress { get { return Mathf.Clamp(currentTime / timeToComplete, 0.0f, 1.0f); } }
	public int CurrentScore
	{
		get
		{
			if(!isGameOver)
			{
				currentScore = (int)(CurrentTravelProgress * 100000.0f);
			}
			return currentScore;
		}
	}
	public float CriticalMalfunctionProgress { get { return Mathf.Clamp((float)malfunctionManager.CriticalMalfunctionCount / maxMalfunctions, 0.0f, 1.0f); } }

	private void Start()
	{
		malfunctionManager = GetComponent<MalfunctionManager>();
		OnGameOver += SetGameOver;
	}
	void Update()
	{
		currentTime += Time.deltaTime;

		if (currentTime >= timeToComplete)
		{
			OnGameComplete?.Invoke();
		}

		if (malfunctionManager != null)
		{
			if (malfunctionManager.CriticalMalfunctionCount >= maxMalfunctions)
			{
				StartGameOver();
			}
		}
		else
		{
			Debug.Log("Must Attach Malfunction Manager");
		}
	}

	public void StartGameOver()
	{
		OnGameOver?.Invoke();
	}

	void SetGameOver()
	{
		isGameOver = true;
	}
}
