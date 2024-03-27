using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour
{
    public int slotID;//ø’∏ÒID=ŒÔ∆∑ID
    public IteminBag slotItem;
    public Image slotImage;
    public TMP_Text slotNum;

    public string slotInfo;

    public GameObject itemInSlot;
    public void ItemOnclicked()
    {
        InventoryManager.UpdateItemInfo(slotInfo);
    }

    public void SetupSlot(IteminBag item)
    {
        if (item == null)
        {
            itemInSlot.SetActive(false);
            return;
        }

        slotImage.sprite = item.itemImage;
        slotNum.text = item.itemHeld.ToString();
        slotInfo = item.itemInfo;
    }
}
