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
    public LayerMask enemyLayer;

    public GameObject breakPointPrefab;
    public GameObject breakPoint = null;

    Vector3 mPos;
    Vector3Int cellHovered;
    Vector3Int playerCell;

    int equippedTool = 0;

    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
        cellHovered = Vector3Int.FloorToInt(mPos);
        highlightBlock.transform.position = new Vector2(cellHovered.x + .5f, cellHovered.y + .5f);
        playerCell = Vector3Int.FloorToInt(transform.position);

        float angle = Vector2.SignedAngle(Vector2.up, new Vector2(mPos.x, mPos.y) - new Vector2(transform.position.x, transform.position.y));
        hands.transform.rotation = Quaternion.Euler(0, 0, angle);
        if (angle < 0)
        {
            hands.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            hands.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!interrupt)
        {
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
                Use(equippedTool, cellHovered);
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
            StartCoroutine(BreakCoroutine(pos));
        }
        else if(tool == 1)
        {
            StartCoroutine(HitAxeCoroutine());
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

    IEnumerator HitAxeCoroutine()
    {
        interrupt = true;
        hands.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(.2f);
        Vector2 cPos = cursor.transform.position;
        Collider2D coll = Physics2D.OverlapArea(new Vector2(cPos.x - .5f, cPos.y - .5f), new Vector2(cPos.x + .5f, cPos.y + .5f));
        if(coll.tag == "tree")
        {
            coll.GetComponent<ChopTree>().Hit();
        }
        interrupt = false;

    }

    IEnumerator BreakCoroutine(Vector3Int pos)
    {
        interrupt = true;
        //float angle = Vector2.SignedAngle(Vector2.up, new Vector2(cellHovered.x, cellHovered.y) - new Vector2(playerCell.x, playerCell.y));
        //hands.transform.rotation = Quaternion.Euler(0, 0, angle);
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

}
