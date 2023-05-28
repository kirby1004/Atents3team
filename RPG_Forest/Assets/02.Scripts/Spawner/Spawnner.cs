using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms;

public class Spawnner : MonoBehaviour 
{
    public GameObject orgObject;
    public int TotalCount = 3;
    public float Width = 5.0f;
    public float Height = 5.0f;
    public float ReSpawnDelay = 10.0f;

    public int mySpawnnerIndex = -1;


    // Start is called before the first frame update
    void Start()
    {
        Gamemanager.Inst.mySpawnner.Add(GetComponent<Spawnner>());
        mySpawnnerIndex = FindMyIndex(GetComponent<Spawnner>());
        for(int i = 0; i < TotalCount; ++i)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z += Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<CharacterProperty>().DeathAlarm += 
                Gamemanager.inst.mySpawnner[mySpawnnerIndex].ReSpawn;
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

        if (TotalCount < 3)
        {
            Vector3 pos = transform.position;
            pos.x += Random.Range(-Width * 0.5f, Width * 0.5f);
            pos.z += Random.Range(-Height * 0.5f, Height * 0.5f);
            GameObject obj = Instantiate(orgObject, pos, Quaternion.Euler(0, Random.Range(0.0f, 360.0f), 0));
            obj.GetComponent<CharacterProperty>().DeathAlarm +=
                Gamemanager.inst.mySpawnner[mySpawnnerIndex].ReSpawn;
            obj.GetComponent<Monster>().mySpawnnerIndex = mySpawnnerIndex;
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

}
