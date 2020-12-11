﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipProgressBar : MonoBehaviour
{
    UnityEngine.UI.Slider slider;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<Sprite> townList;

    [SerializeField] UnityEngine.UI.Image startingTown;
    [SerializeField] UnityEngine.UI.Image endingTown;

    void Start()
    {
        slider = GetComponent<UnityEngine.UI.Slider>();
        System.Random random = new System.Random(System.Guid.NewGuid().GetHashCode());

        int randIndex = random.Next(0, townList.Count);
        startingTown.sprite = townList[randIndex];
        randIndex = random.Next(0, townList.Count);
        endingTown.sprite = townList[randIndex];
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = gameManager.CurrentTravelProgress;
    }
}
