using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject lastRoom;
    public int health = 20;
    public GameObject sliderOb;
    Slider healthBar;

    void Start()
    {
        healthBar = sliderOb.GetComponent<Slider>();
    }

    // Update is called once per frame
    public void Damage(int dam)
    {
        health -= dam;
        if (health <= 0)
        {
            transform.position = new Vector2(28, -7);
            health = 20;
            lastRoom.GetComponent<SpawnEnemies>().Spawn();
        }
        healthBar.value = health;
    }
}
