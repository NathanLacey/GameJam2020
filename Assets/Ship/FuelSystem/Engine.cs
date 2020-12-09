using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    [SerializeField] private float engineMaxHeat;
    [SerializeField] private float fuelRestoreHeatAmount;
    [SerializeField] private GameObject heatBar;
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
            //Ship stops??
            Debug.Log("OUT OF FUEL YOU DUMB ASS");
        }
        heatBarSlider.value = currentEngineHeat / engineMaxHeat;
    }

    private void BurnFuel(GameObject fuel)
    {
        FuelManager.Instance.ResetFuel(fuel);
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
        if(collision.gameObject.tag == "Fuel")
        {
            BurnFuel(collision.gameObject);
        }
    }
}
