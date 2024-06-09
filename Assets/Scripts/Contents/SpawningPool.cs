using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    int monsterCount = 0;
    int reserveCount = 0;
    [SerializeField]
    int keepMonsterCount = 0;

    [SerializeField]
    Vector3 spawnPos;
    [SerializeField]
    float spawnRadius = 15.0f;
    [SerializeField]
    float spawnTime = 5.0f;

    public void AddMonsterCount(int value) { monsterCount += value; }
    public void SetKeepMonsterCount(int count) { keepMonsterCount = count; }


    void Start()
    {
        Managers.Game.OnSpawnEvent -= AddMonsterCount;
        Managers.Game.OnSpawnEvent += AddMonsterCount;
    }

    void Update()
    {
        while(reserveCount + monsterCount < keepMonsterCount)
        {
            StartCoroutine("ReserveSpawn");
        }
    }

    IEnumerator ReserveSpawn()
    {
        reserveCount++;
        yield return new WaitForSeconds(Random.Range(0, spawnTime));
        GameObject go = Managers.Game.Spawn(Define.WorldObject.Monster, "Knight");
        NavMeshAgent agent = go.GetOrAddComponent<NavMeshAgent>();

        Vector3 randPos;
        while (true)
        {
            Vector3 randDir = Random.insideUnitSphere * spawnRadius;
            randDir.y = 0;
            randPos = spawnPos + randDir;

            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(randPos, path))
                break;
        }

        go.transform.position = randPos;
        reserveCount--;
    }
}
