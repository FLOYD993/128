using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用于创建其他类型的背包
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory_bag : ScriptableObject
{
    public List<IteminBag> itemList = new List<IteminBag>();
}
