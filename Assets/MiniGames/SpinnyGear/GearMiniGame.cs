using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMiniGame : MonoBehaviour, IMiniGame
{
    [SerializeField] GameObject MovingGearParent;
    [SerializeField] GameObject StoppedGearParent;
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
            StoppedGearParent.SetActive(true);
            MovingGearParent.SetActive(false);
        }
    }


    public void StartMiniGame()
	{
        SetupComponents();
        GearComponents.ForEach(gear => gear.Reset());
        GearComponents.ForEach(gear => gear.transform.parent.rotation = Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
        GearComponents.ForEach(gear => gear.AutoSpin = Random.Range(0, 3) == 0 ? true : false);
        GearComponents.ForEach(gear => gear.DeprecationValue = Random.Range(0.2f, 0.8f));
        StoppedGearParent.SetActive(false);
        MovingGearParent.SetActive(true);
    }

    public void OnMalfunctionStart()
	{
        
    }
}
