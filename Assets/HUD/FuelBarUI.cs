using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBarUI : MonoBehaviour
{
	UnityEngine.UI.Slider slider;
	[SerializeField] EngineBurner engineBurner;
	private void Start()
	{
		slider = GetComponent<UnityEngine.UI.Slider>();
	}

	private void Update()
	{
		slider.value = engineBurner.FuelPercentage;
	}
}
