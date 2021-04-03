using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteAndAnim : MonoBehaviour
{

    SpriteRenderer sr;
    public Animation currAnim;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public void SwitchSprite(Sprite spr)
    {
        sr.sprite = spr;
    }

    public void ToolAnim()
    {
        currAnim.Play();
    }
}
