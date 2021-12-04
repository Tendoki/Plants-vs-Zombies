using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Zombie
{
    public int spawnTime;
    public ZombieType zombieType;
    public int Spawner;
    public bool RandomSpawn;
    public bool isSpawned;
    public bool isFirst;
    public bool isFirstInWave;
}
public enum ZombieType
{
    Zombie_Basic,
    Zombie_Cone,
    Zombie_Bucket
}
