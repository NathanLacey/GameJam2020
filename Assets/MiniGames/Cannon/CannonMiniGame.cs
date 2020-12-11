using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMiniGame : MonoBehaviour, IMiniGame
{
	public bool IsFinished { get { return isFinished; } }

	[SerializeField] GameObject CannonballPrefab;
    [SerializeField] GameObject BetterCannonballPrefab;
    [SerializeField] GameObject ConfettiPrefab;
    [SerializeField] GameObject DustPrefab;
	[SerializeField] float CannonForce = 1.0f;
	GameObject SpawnedCannonball;
	private bool isFinished = false;

	public void OnMalfunctionStart()
	{
		isFinished = false;
		if (SpawnedCannonball)
		{
			Destroy(SpawnedCannonball);
		}
	}

	int GetCannonball()
	{
		return Random.Range(0, 10);
	}

	public void StartMiniGame()
	{
		int cannonBall = GetCannonball();
        GameObject spawnedCannonball;
		GameObject particles;
		if (cannonBall == 0)
        {
			spawnedCannonball = Instantiate(BetterCannonballPrefab, transform);
			particles = Instantiate(ConfettiPrefab, transform);
        }
        else
        {
			spawnedCannonball = Instantiate(CannonballPrefab, transform);
			particles = Instantiate(DustPrefab, transform);
		}
        spawnedCannonball.GetComponent<Rigidbody2D>().AddForce(transform.right * CannonForce, ForceMode2D.Impulse);
		particles.GetComponent<ParticleSystem>().Play();

		isFinished = true;
	}
}
