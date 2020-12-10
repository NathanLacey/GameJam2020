using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMiniGame : MonoBehaviour, IMiniGame
{
    List<IsTriggered> TriggerComponents = new List<IsTriggered>();
    List<DragAndSpin> GearComponents = new List<DragAndSpin>();
    public bool IsFinished { 
        get
		{
            return TriggerComponents.Count == 0 ? false : TriggerComponents.TrueForAll(trigger => trigger.IsBeingTriggered);
        }
    }

    void SetupComponents()
	{
        if (TriggerComponents.Count == 0)
        {
            TriggerComponents.AddRange(GetComponentsInChildren<IsTriggered>());
        }
        if(GearComponents.Count == 0)
		{
            GearComponents.AddRange(GetComponentsInChildren<DragAndSpin>());
		}
    }

    private void Update()
    {
        if (IsFinished)
        {
            gameObject.SetActive(false);
        }
    }


    public void StartMiniGame()
	{
        gameObject.SetActive(true);
        SetupComponents();
        GearComponents.ForEach(gear => gear.Reset());
    }

    public void OnMalfunctionStart()
	{
        
    }
}
