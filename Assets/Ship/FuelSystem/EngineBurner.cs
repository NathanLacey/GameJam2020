using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBurner : MonoBehaviour
{
    [SerializeField] private float engineMaxHeat;
    [SerializeField] private float fuelRestoreHeatAmount;

    // Start is called before the first frame update
    private float currentEngineHeat;
    void Awake()
    {
        currentEngineHeat = engineMaxHeat;
    }

    public float FuelPercentage { get { return currentEngineHeat / engineMaxHeat; } }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentEngineHeat -= Time.fixedDeltaTime;
        if (currentEngineHeat <= 0)
        {
            FindObjectOfType<GameManager>().StartGameOver();
        }
    }
    public void BurnFuel()
    {
        if (currentEngineHeat + fuelRestoreHeatAmount >= engineMaxHeat)
        {
            currentEngineHeat = engineMaxHeat;
        }
        else
        {
            currentEngineHeat = currentEngineHeat + fuelRestoreHeatAmount;
        }
    }
}
