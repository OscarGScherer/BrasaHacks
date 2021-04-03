using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseTool : MonoBehaviour
{
    public GameObject highlightBlock;
    public GameObject cursor;
    public GameObject hands;
    public Tilemap elevation;
    public Tilemap colliders;

    Vector3 mPos;
    Vector3Int cellHovered;
    Vector3Int playerCell;

    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
        //UpdateCursorPosition(mPos);
        cellHovered = Vector3Int.FloorToInt(mPos);
        highlightBlock.transform.position = new Vector2(cellHovered.x + .5f, cellHovered.y + .5f);
        playerCell = Vector3Int.FloorToInt(transform.position);
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<ToolBar>().SwitchTools(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<ToolBar>().SwitchTools(false);
        }

        if (Input.GetKey(KeyCode.Mouse0) && Vector3Int.Distance(playerCell, cellHovered)<=1.5)
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

        float angle = Vector2.SignedAngle(Vector2.up, new Vector2(cellHovered.x, cellHovered.y));

        hands.transform.rotation = Quaternion.Euler(0, 0, angle);
        cursor.transform.position = pPos + new Vector3(xDiff, yDiff, 0);
    }

}
