using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ڴ����������͵ı���
[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class Inventory_bag : ScriptableObject
{
    public List<IteminBag> itemList = new List<IteminBag>();
}
