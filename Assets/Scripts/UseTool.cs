using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseTool : MonoBehaviour
{
    public GameObject cursor;
    public GameObject hands;
    Vector3 mPos;
    Vector3Int cellHovered;

    public Tilemap elevation;
    public Tilemap colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdateCursorPosition(mPos);
        cellHovered = Vector3Int.FloorToInt(cursor.transform.position);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            StartCoroutine(BreakCoroutine(cellHovered));
        }

    }

    IEnumerator BreakCoroutine(Vector3Int pos)
    {
        hands.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(.3f);
        if (elevation.GetTile(pos) != null)
        {
            Debug.Log(elevation.GetTile(pos));
            elevation.SetTile(pos, null);
            colliders.SetTile(pos, null);
        }
    }

    void UpdateCursorPosition(Vector3 pos)
    {
        Vector3 pPos = transform.position;
        float yDiff = pos.y - pPos.y;
        float xDiff = pos.x - pPos.x;

        yDiff = (yDiff > .5f) ? 1 : (yDiff < -.5f) ? -1 : 0;
        xDiff = (xDiff > .5f) ? 1 : (xDiff < -.5f) ? -1 : 0;

        float angle = Vector2.SignedAngle(Vector2.up, new Vector2(xDiff, yDiff));

        hands.transform.rotation = Quaternion.Euler(0, 0, angle);
        cursor.transform.position = pPos + new Vector3(xDiff, yDiff, 0);
    }

}
