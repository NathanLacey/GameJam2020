using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Engine : MonoBehaviour
{
    [SerializeField] private EngineBurner engineBurner;
    [SerializeField] private GameObject heatBar;
    [SerializeField] private FuelManager fuelManager;
    private Slider heatBarSlider;

    void Awake()
    {
        heatBarSlider = heatBar.GetComponent<Slider>();
    }

    void Update()
    {

        heatBarSlider.value = engineBurner.FuelPercentage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fuel"))
        {
            fuelManager.ResetFuel(collision.gameObject);
            engineBurner.BurnFuel();
        }
    }
}
