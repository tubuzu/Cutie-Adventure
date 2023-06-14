using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyableObject", menuName = "SO/DestroyableObjectSO")]
public class DestroyableObjectSO : ScriptableObject
{
    public string objName = "Destroyable Object";
    public ObjectType objType = ObjectType.NoType;
    public int hpMax = 1;
    public List<ItemDropRate> dropList;
}
