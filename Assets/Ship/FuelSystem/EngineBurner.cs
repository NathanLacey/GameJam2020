using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineBurner : MonoBehaviour
{
    [SerializeField] private float engineMaxHeat;
    [SerializeField] private float fuelRestoreHeatAmount;
    [SerializeField] private float fuelConsumption = 10.0f;
    // Start is called before the first frame update
    private float currentEngineHeat;
    void Awake()
    {
        currentEngineHeat = engineMaxHeat * 0.75f;
    }

    public float FuelPercentage { get { return currentEngineHeat / engineMaxHeat; } }
    // Update is called once per frame
    void FixedUpdate()
    {
        currentEngineHeat -= Time.fixedDeltaTime * Random.Range(fuelConsumption - fuelConsumption * 0.25f, fuelConsumption + fuelConsumption * 0.25f);
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
            currentEngineHeat = currentEngineHeat + Random.Range(fuelRestoreHeatAmount - fuelRestoreHeatAmount * 0.25f, fuelRestoreHeatAmount + fuelRestoreHeatAmount * 0.25f);
        }
    }
}
