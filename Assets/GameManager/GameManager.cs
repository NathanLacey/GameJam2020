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

    public delegate void GameCondition();
    public event GameCondition OnGameComplete;
    public event GameCondition OnGameOver;

    public float CurrentTravelProgress { get { return Mathf.Clamp(currentTime / timeToComplete, 0.0f, 1.0f); } }
    public float CriticalMalfunctionProgress { get { return Mathf.Clamp((float)malfunctionManager.CriticalMalfunctionCount / maxMalfunctions, 0.0f, 1.0f); } }

	private void Start()
	{
        malfunctionManager = GetComponent<MalfunctionManager>();
	}
	void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= timeToComplete)
		{
            OnGameComplete?.Invoke();
		}

        if(malfunctionManager != null)
		{
            if(malfunctionManager.CriticalMalfunctionCount >= maxMalfunctions)
			{
                StartGameOver();
			}
		}
		else
		{
            Debug.Log("Must Attach Malfunction Manager");
		}

        if(topEngineBurner.FuelPercentage < 0.0f || bottomEngineBurner.FuelPercentage < 0.0f)
		{
            StartGameOver();
		}
    }

    public void StartGameOver()
	{
        OnGameOver?.Invoke();
	}
}
