using Assets.CaptainCatSparrow.ArmorAndJewelry.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcSpawnner : MonoBehaviour
{
    public GameObject[] orgNpc;
    public int Total = 10;

    [Serializable]
    public class WayPoints
    {
        public Transform[] wayPoint;
    }

    public WayPoints[] npcWay;

    private void Start()
    {
        StartCoroutine(FirstRespawning());
    }

    Transform[] RandomWayPoint()
    {
        Transform[] temp = { };
        int rnd = UnityEngine.Random.Range(0, npcWay.Length);
        int reverse = UnityEngine.Random.Range(1, 3);
        switch (reverse)
        {
            case 1:
                temp = npcWay[rnd].wayPoint;
                break;
            case 2:
                temp = Enumerable.Reverse(npcWay[rnd].wayPoint).ToArray();
                break;

        }
        return temp;
    }

    GameObject RandomNpcObject()
    {
        int rnd= UnityEngine.Random.Range(0, orgNpc.Length);
        return orgNpc[rnd];
    }

    float RandomRespawnTime()
    {
        float delay= UnityEngine.Random.Range(1.0f, 2.5f);
        return delay;
    }

    IEnumerator FirstRespawning()
    {
        for (int i = 0; i < Total; i++)
        {
            Transform[] wayPoint = RandomWayPoint();
            Vector3 pos = wayPoint[0].position;
            GameObject obj = Instantiate(RandomNpcObject(), pos, Quaternion.identity);
            obj.GetComponent<NpcMovement>()?.SetPoints(wayPoint);
            obj.GetComponent<NpcMovement>().RespawnSetting += ReSpawn;

            yield return new WaitForSeconds(RandomRespawnTime());
        }
    }
    void ReSpawn()
    {
        StartCoroutine(ReSpawnning());
    }

    IEnumerator ReSpawnning()
    {
        yield return new WaitForSeconds(3.0f);

        if (NpcMovement.WalkAbleCount < Total)
        {
            Transform[] wayPoint = RandomWayPoint();
            Vector3 pos = wayPoint[0].position;
            GameObject obj = Instantiate(RandomNpcObject(), pos, Quaternion.identity);
            obj.GetComponent<NpcMovement>()?.SetPoints(wayPoint);
            obj.GetComponent<NpcMovement>().RespawnSetting += ReSpawn;
        }
    }
}
