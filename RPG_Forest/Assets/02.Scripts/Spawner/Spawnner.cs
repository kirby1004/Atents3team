using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class Spawnner : MonoBehaviour 
{
    public GameObject orgObject;
    // 생성가능한 몬스터 최대숫자
    public int TotalCount = 3;

    //몬스터 생성범위
    public float Width = 5.0f;
    public float Height = 5.0f;

    // 부활대기시간
    public float ReSpawnDelay = 10.0f;

    public int mySpawnnerIndex = -1;
    public List<GameObject> monsters = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        //CurCount = TotalCount;
        Gamemanager.Inst.mySpawnner.Add(GetComponent<Spawnner>());
        mySpawnnerIndex = FindMyIndex(GetComponent<Spawnner>());
        for(int i = 0; i < TotalCount; ++i)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z += Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<CharacterProperty>().DeathAlarm += 
                Gamemanager.Inst.mySpawnner[mySpawnnerIndex].ReSpawn;
            obj.GetComponent<Monster>().mySpawnnerIndex = mySpawnnerIndex;
            monsters.Add(obj);
            obj.GetComponent<CharacterProperty>().DeathAlarm +=
                () => Gamemanager.Inst.mySpawnner[mySpawnnerIndex].monsters.RemoveAt(FindMonsterIndex(obj));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReSpawn()
    {
        StartCoroutine(ReSpawnning());
    }

    IEnumerator ReSpawnning()
    {
        yield return new WaitForSeconds(ReSpawnDelay);

        if (monsters.Count < TotalCount)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z += Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<CharacterProperty>().DeathAlarm +=
                Gamemanager.inst.mySpawnner[mySpawnnerIndex].ReSpawn;
            obj.GetComponent<Monster>().mySpawnnerIndex = mySpawnnerIndex;
            obj.GetComponent<CharacterProperty>().DeathAlarm +=
                () => Gamemanager.Inst.mySpawnner[mySpawnnerIndex].monsters.RemoveAt(FindMonsterIndex(obj));
            obj.GetComponent<Monster>().mySpawnnerIndex = mySpawnnerIndex;
            monsters.Add(obj);
        }
    }

    public int FindMyIndex(Spawnner mySpawnner)
    {
        for (int i = 0; i < Gamemanager.inst.mySpawnner.Count; i++)
        {
            if (Gamemanager.inst.mySpawnner[i] == mySpawnner)
            {
                return i;
            }
        }
        return -1;
    }
    public int FindMonsterIndex(GameObject obj)
    {
        for(int i = 0;0< monsters.Count; i++)
        {
            if (monsters[i] == obj)
                return i;
        }
        return -1;
    }
}
