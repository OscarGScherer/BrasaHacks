using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Inventory : MonoBehaviour {

    public GameObject[] UiSlots = new GameObject[5];
    public GameObject diamonds;
    public int diamondAmount;
    TextMeshProUGUI diamondCounter;
    public bool[] isFull = new bool[5];
    public Tile[] slot = new Tile[5];
    public int[] amount = new int[5];
    public int activeSlot = 0;

    private void Start()
    {
        diamondCounter = diamonds.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        diamondAmount = 0;
        diamondCounter.SetText(diamondAmount.ToString());
        for(int i = 0; i<5; i++)
        {
            slot[i] = null;
            amount[i] = 0;
        }
    }

    public void AddDiamond(int num)
    {
        diamondAmount += num;
        diamondCounter.SetText(diamondAmount.ToString());
    }

    public void SwitchActiveSlot(int newSlot)
    {
        UiSlots[activeSlot].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
        activeSlot = newSlot;
        UiSlots[activeSlot].transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Remove(int i)
    {
        amount[i] -= 1;
        UiSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(amount[i].ToString());
        if (amount[i] == 0)
        {
            slot[i] = null;
            UiSlots[i].GetComponent<SpriteRenderer>().sprite = null;
            UiSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("");
        }
    }

}
