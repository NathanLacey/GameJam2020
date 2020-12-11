using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerMiniGame : MonoBehaviour, IMiniGame
{
    [SerializeField] SpinPropeller WorkingPropeller;
    [SerializeField] FixPropeller BrokenPropeller;
	[SerializeField] GameObject Arrows;
    public bool IsFinished { get { return BrokenPropeller.IsFinished; } }
	private bool FlashInfo = false;
	float FlashMaxSeconds = 2.0f;
	float CurrentSeconds = 0.0f;
	private void Update()
	{
		if(IsFinished)
		{
			BrokenPropeller.gameObject.SetActive(false);
			Arrows.gameObject.SetActive(false);
			WorkingPropeller.gameObject.SetActive(true);
		}

		if(FlashInfo)
		{
			CurrentSeconds += Time.fixedDeltaTime;
			if(CurrentSeconds >= FlashMaxSeconds)
			{
				Flash();
				CurrentSeconds = 0.0f;
			}
		}
	}

	void Flash()
	{
		Arrows.gameObject.SetActive(!Arrows.gameObject.activeSelf);
	}

	void Reset()
	{
		BrokenPropeller.gameObject.SetActive(true);
		Arrows.gameObject.SetActive(true);
		WorkingPropeller.gameObject.SetActive(false);
	}

	public void StartMiniGame()
	{
		FlashInfo = false;
		Arrows.gameObject.SetActive(false);
		BrokenPropeller.GetComponent<DragAndSpin>().enabled = true;
	}

	public void OnMalfunctionStart()
	{
		FlashInfo = true;
		Reset();
	}
}
