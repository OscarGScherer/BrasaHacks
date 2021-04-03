using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Pickup : MonoBehaviour{
    private Inventory inventory;
    public Tile block;
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
                    inventory.slot[i] = block;
                    inventory.amount[i] = 1;
                    break;
                }
                else if (inventory.slot[i]== block)
                {
                    inventory.amount[i] += 1;
                }
            }
        }  
    }
}
