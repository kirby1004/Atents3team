using UnityEngine;
using UnityEngine.Events;

public class LootingManager : Singleton<LootingManager>
{

    private void Awake()
    {
        base.Initialize();
    }


    public UnityAction ItemDrop;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject LootWindow;

    
    //public void SpawnLootWindow(ItemDropTable itemDropTable, Transform transform)
    //{
    //    GameObject obj = Instantiate(LootWindow, transform);
    //    obj.GetComponent<DropList>().myDropTable = itemDropTable;
    //    ItemDrop += () => obj.SetActive(true);
    //    //GameObject obj = Instantiate(Resources.Load("") as GameObject, transform);
    //}

    public void SpawnLootWindow(ItemDropTable itemDropTable)
    {
        //GameObject obj = Instantiate(LootWindow, transform);
        GameObject obj = Instantiate(Resources.Load("UIResource/Looting/LootWindow") as GameObject, transform);
        LootWindow = obj;
        obj.GetComponentInChildren<DropList>().myDropTable = itemDropTable;
        ItemDrop += () => obj.SetActive(true);
    }

    public bool ProbabilityChoose(float Rate)
    {
        //float Percentge = Rate / 100;
        if (Random.Range(0, 100) < Rate)
        {
            return true;
        }
        return false;
    }


}
