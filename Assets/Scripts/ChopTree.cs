using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopTree : MonoBehaviour
{

    int state = 0;
    public Sprite choppedSprite;
    public GameObject drop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Hit()
    {
        state += 1;
        if(state == 5)
        {
            GetComponent<SpriteRenderer>().sprite = choppedSprite;
            Instantiate(drop, transform.position - Vector3.up, transform.rotation);
            gameObject.tag = "chopped tree";
        }
    }
}
