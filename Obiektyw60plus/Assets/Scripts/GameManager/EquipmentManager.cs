using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {

    #region Singleton

    public static EquipmentManager instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of EquipmentManager found!");
            return;
        }
        instance = this;
    }

    #endregion
    public bool IsItemEquipped = false;
    //public Item ActiveItem = null;
    //ItemHolder itemHolder;
    //WandHolder wandHolder;

    // Use this for initialization
    void Start () {
        //GameObject itemHolderObject = GameObject.Find("ItemHolder");
        //itemHolder = itemHolderObject.GetComponent<ItemHolder>();
        //GameObject wandHolderObject = GameObject.Find("WandHolder");
        //wandHolder = wandHolderObject.GetComponent<WandHolder>();
    }
	
	// Update is called once per frame
	void Update () {
		
   	}

    //public void ToggleScript(Item item, bool IsActive)
    //{
        
    //    ActiveItem = item;

    //    switch (item.name)
    //    {
    //        case "RedWand":
    //            {
    //                GameObject wand = GameObject.Find("RedWand");
    //                wand.GetComponent<UseRedWand>().enabled = IsActive;
    //                break;
    //            }
    //        case "GreenWand":
    //            {
    //                GameObject wand = GameObject.Find("GreenWand");
    //                wand.GetComponent<UseGreenWand>().enabled = IsActive;
    //                break;
    //            }
    //        case "PurpleWand":
    //            {
    //                GameObject wand = GameObject.Find("PurpleWand");
    //                wand.GetComponent<UsePurpleWand>().enabled = IsActive;
    //                break;
    //            }
    //        default:
    //            {
    //                print("some other item found");
    //                break;
    //            }
    //    }
    //}

    //public void EquipActiveItem()
    //{
    //    GameObject wand = GameObject.Find(ActiveItem.name);

    //    //rotate to the rotation of itemholder+
    //    wand.transform.Rotate(0, 0, 90f);
    //    wand.transform.position = itemHolder.transform.position + new Vector3(0, 0.1f, 0);
    //    //TO DO attach the wand to a socket
    //}

    //public void UnEquipActiveItem()
    //{
    //    GameObject wand = GameObject.Find(ActiveItem.name);
    //    wandHolder.OccupyFirstEmptySlot(wand);
    //    //TO DO make it work better
    //}
}
