using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
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
        healthBar.value = health;
    }
}
