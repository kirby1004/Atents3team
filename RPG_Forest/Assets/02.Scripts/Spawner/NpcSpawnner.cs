using Assets.CaptainCatSparrow.ArmorAndJewelry.Demo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcSpawnner : MonoBehaviour
{
    #region NPC 스포너 멤버변수
    public int Total = 10;

    enum NpcList
    {
        NpcFemale1, NpcFemale2, NpcMale
    }
    NpcList npcList;

    [Serializable]
    public class WayPoints
    {
        public Transform[] wayPoint;
    }

    public WayPoints[] npcWay;
    #endregion

    public int NpcCount = 0;

    private void Update()
    {
        NpcCount = NpcMovement.WalkAbleCount;
    }
    private void Start()
    {
        for (int i = 0; i < Total; i++)
        {
            Transform[] wayPoint = RandomWayPoint();
            Vector3 pos = wayPoint[0].position;
            GameObject obj = ObjectPoolingManager.instance.GetObject(RandomNpcObject(), pos, Quaternion.identity);
            obj.GetComponent<NpcMovement>()?.SetPoints(wayPoint);
            obj.GetComponent<NpcMovement>().RespawnSetting += ReSpawn;
        }
    }

    #region NPC 랜덤 스폰 함수
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

    string RandomNpcObject()
    {
        int rnd = UnityEngine.Random.Range(0, System.Enum.GetValues(typeof(NpcList)).Length);
        npcList = (NpcList)rnd;
        return npcList.ToString();
    }


    float RandomRespawnTime()
    {
        float delay= UnityEngine.Random.Range(1.0f, 2.5f);
        return delay;
    }
    #endregion

    #region 스폰 함수
    void ReSpawn()
    {
        StartCoroutine(ReSpawnning());
    }

    IEnumerator ReSpawnning()
    {
        yield return new WaitForSeconds(RandomRespawnTime());
        if (NpcMovement.WalkAbleCount < Total)
        {
            Transform[] wayPoint = RandomWayPoint();
            Vector3 pos = wayPoint[0].position;
            GameObject obj = ObjectPoolingManager.instance.GetObject(RandomNpcObject(), pos, Quaternion.identity);
            obj.GetComponent<NpcMovement>()?.SetPoints(wayPoint);
            obj.GetComponent<NpcMovement>().RespawnSetting += ReSpawn;
        }
    }
    #endregion
}
