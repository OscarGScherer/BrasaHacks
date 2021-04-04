using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Pickup : MonoBehaviour{
    private Inventory inventory;
    public Tile blockTile;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     if (other.CompareTag("Player"))
        {
            for (int i = 0; i< inventory.slot.Length; i++)
            {
                if (inventory.slot[i] == null)
                {
                    inventory.slot[i] = blockTile;
                    inventory.UiSlots[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                    inventory.amount[i] = 1;
                    Destroy(gameObject);
                    return;
                }
                else if (inventory.slot[i]== blockTile)
                {
                    inventory.amount[i] += 1;
                    inventory.UiSlots[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText(inventory.amount[i].ToString());
                    Destroy(gameObject);
                    return;
                }
            }
        }  
    }
}
