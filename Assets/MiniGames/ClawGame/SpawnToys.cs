using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToys : MonoBehaviour
{
    [SerializeField] private List<GameObject> toyPrefabs;
    [SerializeField] private float toyRespawnTime;
    [SerializeField] private float toyStartTorque;
    [SerializeField] private float toyFlySpeed;
    private List<GameObject> toys = new List<GameObject>();
    private float currentRespawnTime = 0.0f;
    private int currentToyIndex = 0;

    void Start()
    {
        currentRespawnTime = toyRespawnTime;
        for (int i = 0; i < 10; ++i)
        {
            toys.Add(Instantiate(toyPrefabs[Random.Range(0, toyPrefabs.Count)], transform));
            toys[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentRespawnTime += Time.fixedDeltaTime;
        if(currentRespawnTime >= toyRespawnTime)
        {
            toys[currentToyIndex].SetActive(true);
            toys[currentToyIndex].transform.position = new Vector3(transform.position.x, Random.Range(transform.position.y - 1.0f, transform.position.y + 1.0f), transform.position.z);
            toys[currentToyIndex].GetComponent<Rigidbody2D>().AddTorque(toyStartTorque);
            currentToyIndex = ++currentToyIndex % toys.Count;
            currentRespawnTime = 0.0f;
        }
        foreach(var toy in toys)
        {
            toy.GetComponent<Rigidbody2D>().velocity = new Vector2(-toyFlySpeed * Time.fixedDeltaTime, 0.0f);
        }
    }
}
