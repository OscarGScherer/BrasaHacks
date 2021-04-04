using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BreakBlock : MonoBehaviour
{
    public GameObject playerRef;
    public Tilemap elevation;
    public Tilemap colliders;
    public Sprite[] states = new Sprite[10];

    Vector3Int blockPos;
    public int state = 0;
    SpriteRenderer sr;
    public GameObject dropPrefab;
    // Start is called before the first frame update
    void Start()
    {
        blockPos = Vector3Int.FloorToInt(transform.position - Vector3.up);
        sr = GetComponent<SpriteRenderer>();
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(10);
        playerRef.GetComponent<UseTool>().breakPoint = null;
        Destroy(gameObject);
    }

    // Update is called once per frame
    public void Hit()
    {
        state += 1;
        if(state == 10)
        {
            elevation.SetTile(blockPos, null);
            colliders.SetTile(blockPos, null);
            playerRef.GetComponent<UseTool>().breakPoint = null;
            GameObject drop = Instantiate(dropPrefab, new Vector2(blockPos.x + .5f, blockPos.y + .5f), transform.rotation);
            Destroy(gameObject);
            return;
        }
        sr.sprite = states[state];
    }
}
