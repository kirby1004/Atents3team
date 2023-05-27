using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public float curHP;
    public Vector3 Portal0;
    public Vector3 Portal1;
    public Vector3 Portal2;
}


public class DataSaverManager : MonoBehaviour
{
    public PlayerData playerData;
    // Start is called before the first frame update
    void Start()
    {
        TextAsset textAsset = Resources.Load<TextAsset>("PlayerData/PlayerData");

        playerData = JsonUtility.FromJson<PlayerData>(textAsset.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
