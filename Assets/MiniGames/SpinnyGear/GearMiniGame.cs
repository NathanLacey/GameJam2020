using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearMiniGame : MonoBehaviour, IMiniGame
{
    [SerializeField] GameObject MovingGearParent;
    [SerializeField] GameObject StoppedGearParent;
    List<IsTriggered> TriggerComponents = new List<IsTriggered>();
    List<DragAndSpin> GearComponents = new List<DragAndSpin>();
    public bool IsFinished { get; private set; }

    void SetupComponents()
	{
        if (TriggerComponents.Count == 0)
        {
            TriggerComponents.AddRange(GetComponentsInChildren<IsTriggered>(true));
        }
        if(GearComponents.Count == 0)
		{
            GearComponents.AddRange(GetComponentsInChildren<DragAndSpin>(true));
		}
    }

    private void Update()
    {
        if(TriggerComponents.Count > 0 && MovingGearParent.activeSelf && TriggerComponents.TrueForAll(trigger => trigger.IsBeingTriggered))
		{
            StoppedGearParent.SetActive(true);
            MovingGearParent.SetActive(false);
            IsFinished = true;
		}
    }


    public void StartMiniGame()
	{
        StoppedGearParent.SetActive(false);
        MovingGearParent.SetActive(true);
    }

    public void OnMalfunctionStart()
	{
        IsFinished = false;
        SetupComponents();
        TriggerComponents.ForEach(trigger => trigger.IsBeingTriggered = false);
        GearComponents.ForEach(gear => gear.Reset());
        GearComponents.ForEach(gear => gear.AutoSpin = Random.Range(0, 3) == 0 ? true : false);
        GearComponents.ForEach(gear => gear.DeprecationValue = Random.Range(0.2f, 0.8f));
    }
}
