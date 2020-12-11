using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    [SerializeField] private float engineMaxHeat;
    [SerializeField] private float fuelRestoreHeatAmount;
    [SerializeField] private GameObject heatBar;
    [SerializeField] private FuelManager fuelManger;
    private Slider heatBarSlider;
    private float currentEngineHeat;

    void Awake()
    {
        currentEngineHeat = engineMaxHeat;
        heatBarSlider = heatBar.GetComponent<Slider>();
    }

    void Update()
    {
        currentEngineHeat -= Time.fixedDeltaTime;
        if (currentEngineHeat <= 0)
        {
            FindObjectOfType<GameManager>().StartGameOver();
        }
        heatBarSlider.value = currentEngineHeat / engineMaxHeat;
    }

    private void BurnFuel(GameObject fuel)
    {
        fuelManger.ResetFuel(fuel);
        if (currentEngineHeat + fuelRestoreHeatAmount >= engineMaxHeat)
        {
            currentEngineHeat = engineMaxHeat;
        }
        else
        {
            currentEngineHeat = currentEngineHeat + fuelRestoreHeatAmount;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fuel"))
        {
            BurnFuel(collision.gameObject);
        }
    }
}
