using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using TMPro;

public class Pickup : MonoBehaviour{
    private Inventory inventory;
    private ToolBar toolBar;
    public Tile blockTile;
    public bool isDiamond = false;
    public bool isTool = false;
    public int tool = -1;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        toolBar = GameObject.FindGameObjectWithTag("Player").GetComponent<ToolBar>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
     if (other.CompareTag("Player"))
        {
            if (isTool)
            {
                toolBar.AddTool(tool);
                Destroy(gameObject);
            }
            else if (isDiamond)
            {
                inventory.AddDiamond(1);
                Destroy(gameObject);
            }
            else
            {
                for (int i = 0; i < inventory.slot.Length; i++)
                {
                    if (inventory.slot[i] == null)
                    {
                        inventory.slot[i] = blockTile;
                        inventory.UiSlots[i].GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;
                        inventory.amount[i] = 1;
                        Destroy(gameObject);
                        return;
                    }
                    else if (inventory.slot[i] == blockTile)
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
}
