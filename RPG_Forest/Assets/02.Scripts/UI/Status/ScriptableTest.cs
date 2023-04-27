using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Test", menuName = "Scriptable Object/Test", order = int.MaxValue)]
public class ScriptableTest : ScriptableObject
{
    public Test myTest;

    

}
public enum Test
{
    a,b,c,d,e
}