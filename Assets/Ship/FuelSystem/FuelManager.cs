using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelManager : MonoBehaviour
{
    private static FuelManager pInstance;
    public static FuelManager Instance { get { return pInstance; } }

    [SerializeField] private GameObject fuelTemplatePrefab;
    [SerializeField] private List<GameObject> fuels;
    [SerializeField] private float fuelRespawnTime = 0;
    private float currentFuelRespawnTime = 0;
    private int currentFuelObjectIndex = 0;

    void Awake()
    {
        if (pInstance != null && pInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            pInstance = this;
        }

        fuels = new List<GameObject>();
        //fuels.Resize(10);
        for(int i = 0; i < 10; ++i)
        {
            fuels.Add(Instantiate(fuelTemplatePrefab));
            fuels[i].SetActive(false);
            fuels[i].transform.position = transform.position;
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
        fuels[currentFuelObjectIndex].SetActive(true);
        currentFuelObjectIndex = ++currentFuelObjectIndex % fuels.Count;
    }

    public void ResetFuel(GameObject fuel)
    {
        fuel.SetActive(false);
        fuel.transform.position = transform.position;
    }
}
