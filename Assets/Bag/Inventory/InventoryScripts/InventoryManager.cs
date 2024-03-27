using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory_bag myBag;
    public GameObject slotGrid;
    //public Slot slotPrefab;
    public GameObject emptySlot;
    public TextMeshProUGUI itemInformation;

    public List<GameObject> slots = new List<GameObject>();
    private void Awake()//private?
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.itemInformation.text = "";
    }

    public static void UpdateItemInfo(string itemDescription)
    {
        instance.itemInformation.text = itemDescription;
    }

    /* public static void CreateNewItem(Item_in_bag item)
     {
         Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
         newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
         newItem.slotItem = item;
         newItem.slotImage.sprite = item.itemImage;
         newItem.slotNum.text = item.itemHeld.ToString();
     }
    */
    public static void RefreshItem()
    {
        //ѭ��ɾ��slotGrid�µ��Ӽ�����
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
            instance.slots.Clear();
        }

        //�������ɶ�Ӧmybag�������Ʒslot
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            // CreateNewItem(instance.myBag.itemList[i]);
            instance.slots.Add(Instantiate(instance.emptySlot));
            instance.slots[i].transform.SetParent(instance.slotGrid.transform);
            instance.slots[i].GetComponent<Slot>().slotID = i;
            instance.slots[i].GetComponent<Slot>().SetupSlot(instance.myBag.itemList[i]);
        }
    }
}