using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Inventory : MonoBehaviour {
    public bool[] isFull = new bool[5];
    public Tile[] slot = new Tile[5];
    public int[] amount = new int[5];

    private void Start()
    {
        for(int i = 0; i<5; i++)
        {
            slot[i] = null;
            amount[i] = 0;
        }
    }



}
