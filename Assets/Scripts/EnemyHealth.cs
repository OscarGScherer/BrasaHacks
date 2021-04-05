using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth = 10;
    int maxHealth;
    public ParticleSystem onDeath;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
    }

    // Update is called once per frame
    public void Hit(int damage)
    {
        transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Instantiate(onDeath, transform.position, Quaternion.Euler(-90, 0, 0));
            Destroy(gameObject);
            GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().killCount += 1;
        }
    }

}
