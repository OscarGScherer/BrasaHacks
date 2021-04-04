using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    int diamonds;
    public int requiredDiamonds;

    private void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            diamonds = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().diamondAmount;
            if(diamonds >= requiredDiamonds)
            {
                Destroy(gameObject);
            }
        }
    }
}
