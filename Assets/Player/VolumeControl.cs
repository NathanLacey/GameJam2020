using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControl : MonoBehaviour
{
	public void OnChanged(float value)
	{
		AudioListener.volume = value;
	}
}
