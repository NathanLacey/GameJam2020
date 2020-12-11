using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    [SerializeField] private List<Sprite> fuelSprites = new List<Sprite>();
    [SerializeField] private GameObject fuelTemplatePrefab;
    [SerializeField] private List<GameObject> fuels = new List<GameObject>();
    [SerializeField] private float fuelRespawnTime = 0;
    [SerializeField] private Transform FuelSpawnParent;
    private float currentFuelRespawnTime = 0;
    private int currentFuelObjectIndex = 0;

    void Awake()
    {
        for(int i = 0; i < 10; ++i)
        {
            fuels.Add(Instantiate(fuelTemplatePrefab));
            fuels[i].SetActive(false);
            fuels[i].transform.position = transform.position;
            fuels[i].transform.parent = FuelSpawnParent;
        }
    }

    void Update()
    {
        currentFuelRespawnTime += Time.fixedDeltaTime;
        if (currentFuelRespawnTime >= fuelRespawnTime)
        {
            SpawnFuel();
            currentFuelRespawnTime = 0;
        }
    }

    private void SpawnFuel()
    {
        if (!fuels[currentFuelObjectIndex].activeSelf)
        {
            fuelSprites.Shuffle();
            fuels[currentFuelObjectIndex].GetComponentInChildren<SpriteRenderer>().sprite = fuelSprites[0];
            fuels[currentFuelObjectIndex].SetActive(true);
            fuels[currentFuelObjectIndex].transform.position = transform.position;
        }
        currentFuelObjectIndex = ++currentFuelObjectIndex % fuels.Count;
    }

    public void ResetFuel(GameObject fuel)
    {
        fuel.SetActive(false);
    }
}
