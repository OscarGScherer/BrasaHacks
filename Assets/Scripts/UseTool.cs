using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseTool : MonoBehaviour
{
    bool interrupt = false;

    public GameObject highlightBlock;
    public Sprite breakBlockHighlight, placeBlockHighlight;
    public GameObject cursor;
    public GameObject hands;
    public Tilemap elevation;
    public Tilemap colliders;
    public Tile collTile;

    public GameObject breakPointPrefab;
    public GameObject breakPoint = null;

    Vector3 mPos;
    Vector3Int cellHovered;
    Vector3Int playerCell;

    int equippedTool = 0;

    void Update()
    {
        if (!interrupt)
        {
            mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
            //UpdateCursorPosition(mPos);
            cellHovered = Vector3Int.FloorToInt(mPos);
            highlightBlock.transform.position = new Vector2(cellHovered.x + .5f, cellHovered.y + .5f);
            playerCell = Vector3Int.FloorToInt(transform.position);
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                GetComponent<ToolBar>().SwitchTools(true);
                equippedTool = GetComponent<ToolBar>().tools[0];
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                GetComponent<ToolBar>().SwitchTools(false);
                equippedTool = GetComponent<ToolBar>().tools[0];
            }

            if (Input.GetKey(KeyCode.Mouse0))
            {
                StartCoroutine(BreakCoroutine(cellHovered));
            }
            if (Input.GetKey(KeyCode.Mouse1))
            {
                PlaceBlock(cellHovered);
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                GetComponent<Inventory>().SwitchActiveSlot(0);
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                GetComponent<Inventory>().SwitchActiveSlot(1);
            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                GetComponent<Inventory>().SwitchActiveSlot(2);
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                GetComponent<Inventory>().SwitchActiveSlot(3);
            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                GetComponent<Inventory>().SwitchActiveSlot(4);
            }

        }

    }

    void Use(int tool, Vector3Int pos)
    {
        if(tool == 0)
        {
            BreakCoroutine(pos);
        }
        else if(tool == 1)
        {

        }
    }

    void PlaceBlock(Vector3Int pos)
    {
        Inventory inv = GetComponent<Inventory>();
        if(elevation.GetTile(pos) == null && colliders.GetTile(pos) == null)
        {
            elevation.SetTile(pos, inv.slot[inv.activeSlot]);
            colliders.SetTile(pos, collTile);
            inv.Remove(inv.activeSlot);
        }

    }

    IEnumerator BreakCoroutine(Vector3Int pos)
    {
        interrupt = true;
        float angle = Vector2.SignedAngle(Vector2.up, new Vector2(cellHovered.x, cellHovered.y) - new Vector2(playerCell.x, playerCell.y));
        hands.transform.rotation = Quaternion.Euler(0, 0, angle);
        hands.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(.2f);
        if (elevation.GetTile(pos) != null && breakPoint == null)
        {
            MakeBreakPoint();
        }
        else if(breakPoint != null && cellHovered == Vector3Int.FloorToInt(breakPoint.transform.position - Vector3.up))
        {
            breakPoint.GetComponent<BreakBlock>().Hit();
        }
        else if(elevation.GetTile(pos) != null && breakPoint != null && cellHovered != Vector3Int.FloorToInt(breakPoint.transform.position - Vector3.up))
        {
            Destroy(breakPoint);
            MakeBreakPoint();
        }
        interrupt = false;
    }

    void MakeBreakPoint()
    {
        breakPoint = Instantiate(breakPointPrefab, new Vector3(cellHovered.x + .5f, cellHovered.y + 1.25f, 0), transform.rotation);
        BreakBlock bb = breakPoint.GetComponent<BreakBlock>();
        bb.elevation = elevation;
        bb.colliders = colliders;
        bb.playerRef = gameObject;
    }

    void UpdateCursorPosition(Vector3 pos)
    {
        Vector3 pPos = transform.position;
        float yDiff = pos.y - pPos.y;
        float xDiff = pos.x - pPos.x;

        yDiff = (yDiff > .5f) ? 1 : (yDiff < -.5f) ? -1 : 0;
        xDiff = (xDiff > .5f) ? 1 : (xDiff < -.5f) ? -1 : 0;

        cursor.transform.position = pPos + new Vector3(xDiff, yDiff, 0);
    }

}
