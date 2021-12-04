using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class ZombiesSpawner : MonoBehaviour
{
	public List<GameObject> zombiesPrefabs;
	public List<Zombie> zombies;
	public GameManager gameManager;
	public AudioClip awoogaAudio;
	public AudioClip sirenAudio;

	private void Start()
	{
		gameManager = GameManager.instance;
		gameManager.zombiesLevel1 = zombies.Count;
	}
	private void Update()
	{
		if (!gameManager.isLose && !gameManager.isWin)
		{
			foreach (Zombie zombie in zombies)
			{
				if (zombie.isSpawned == false && zombie.spawnTime <= Time.timeSinceLevelLoad)
				{
					if (zombie.RandomSpawn)
					{
						zombie.Spawner = Random.Range(0, transform.childCount);
					}
					GameObject zombieInstance = Instantiate(zombiesPrefabs[(int)zombie.zombieType], transform.GetChild(zombie.Spawner).transform);
					transform.GetChild(zombie.Spawner).GetComponent<SpawnPoint>().zombies.Add(zombieInstance);
					zombie.isSpawned = true;
					if (zombie.isFirst)
					{
						this.GetComponent<AudioSource>().PlayOneShot(awoogaAudio);
					}
					if (zombie.isFirstInWave)
					{
						this.GetComponent<AudioSource>().PlayOneShot(sirenAudio);
					}
					string layername = (zombie.Spawner).ToString();
					zombieInstance.GetComponent<SpriteRenderer>().sortingLayerName = "Zombie" + layername;
				}
			}
		}
	}
}