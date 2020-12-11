using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	MalfunctionManager malfunctionManager;
	[SerializeField] int maxMalfunctions = 5;
	[SerializeField] float timeToComplete = 5 * 60; // 5 minutes
	[SerializeField] EngineBurner topEngineBurner;
	[SerializeField] EngineBurner bottomEngineBurner;
	float currentTime = 0.0f;
	int currentScore = 0;
	bool isGameOver = false;

	public delegate void GameCondition();
	public event GameCondition OnGameComplete;
	public event GameCondition OnGameOver;
	bool pauseGameTime = false;
	public bool Paused
	{
		get
		{
			return pauseGameTime;
		}
		set
		{
			if(value == false && isGameOver)
			{
				return;
			}
			malfunctionManager.PauseMalfunctionCreation = value;
			pauseGameTime = value;
		}
	}
	public float CurrentTravelProgress { get { return Mathf.Clamp(currentTime / timeToComplete, 0.0f, 1.0f); } }
	public int CurrentScore
	{
		get
		{
			if (!isGameOver)
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
		if (malfunctionManager.PauseMalfunctionCreation)
		{
			return;
		}

		if(!Paused)
		{
			currentTime += Time.deltaTime;
		}

		if (currentTime >= timeToComplete)
		{
			SetGameOver();
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

		if (topEngineBurner.FuelPercentage < 0.0f || bottomEngineBurner.FuelPercentage < 0.0f)
		{
			StartGameOver();
		}
	}

	public void StartGameOver()
	{
		SetGameOver();
		OnGameOver?.Invoke();
	}

	void SetGameOver()
	{
		Paused = true;
		isGameOver = true;
	}
}
