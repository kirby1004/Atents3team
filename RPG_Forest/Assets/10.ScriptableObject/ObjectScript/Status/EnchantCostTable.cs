using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "CostTable", menuName = "Scriptable Object/CostTable")]
public class EnchantCostTable : ScriptableObject
{
    public int[] CostTable;

    public float[] SuccessRate;
}
