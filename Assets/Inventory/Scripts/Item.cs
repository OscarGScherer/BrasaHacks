using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item{
    public enum ItemType {
    DirtBlock,
    StoneBlock,
    Pickaxe,

    }
    public ItemType itemType;
    public int amount;
}
