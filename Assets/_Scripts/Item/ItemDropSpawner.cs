using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropSpawner : Spawner
{
    private static ItemDropSpawner instance;
    public static ItemDropSpawner Instance => instance;
    protected override void Awake()
    {
        base.Awake();
        if (ItemDropSpawner.Instance != null) Debug.LogError("Only 1 ItemDropSpawner allow to exist!");
        ItemDropSpawner.instance = this;
    }
    public virtual void Drop(List<ItemDropRate> dropList, Vector3 pos, Quaternion rot)
    {
        if (dropList == null || dropList.Count < 1) return;
        ItemCode itemCode = RandomItemFromList(dropList).itemSO.itemCode;
        Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
        if (itemDrop == null) return;
        itemDrop.gameObject.SetActive(true);
    }
    public virtual Transform Drop(ItemInventory itemI, Vector3 pos, Quaternion rot)
    {
        ItemCode itemCode = itemI.itemProfile.itemCode;
        Transform itemDrop = this.Spawn(itemCode.ToString(), pos, rot);
        if (itemDrop == null) return null;
        itemDrop.gameObject.SetActive(true);
        ItemCtrl itemCtrl = itemDrop.GetComponent<ItemCtrl>();
        itemCtrl.SetItemIventory(itemI);

        return itemDrop;
    }
    protected virtual ItemDropRate RandomItemFromList(List<ItemDropRate> dropList)
    {
        int dropRate = Random.Range(1, 101);
        for (int i = 0; i < dropList.Count - 1; i++)
        {
            if (dropRate <= dropList[i].dropRate) return dropList[i];
            else dropRate -= dropList[i].dropRate;
        }
        return dropList[dropList.Count - 1];
    }
}
