using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMiniGame : MonoBehaviour, IMiniGame
{
	public bool IsFinished { get { return isFinished; } }

	[SerializeField] GameObject CannonballPrefab;
	[SerializeField] GameObject BetterCannonballPrefab;
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

	GameObject GetCannonball()
	{
		return Random.Range(0, 10) == 0 ? BetterCannonballPrefab : CannonballPrefab;
	}

	public void StartMiniGame()
	{
		GameObject spawnedCannonball = Instantiate(GetCannonball(), transform);
		spawnedCannonball.GetComponent<Rigidbody2D>().AddForce(transform.right * CannonForce, ForceMode2D.Impulse);
		AudioSource fireSound;
		if (gameObject.TryGetComponent<AudioSource>(out fireSound))
		{
			fireSound.Play();
		}
		isFinished = true;
	}
}
