using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UseTool : MonoBehaviour
{
    bool interrupt = false;

    public GameObject[] blockDrops = new GameObject[3];
    public TileBase[] rockBlocks = new TileBase[2];
    public TileBase woodBlock;

    public GameObject highlightBlock;
    public Sprite HighlightBlockSprite;
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

    private void Start()
    {
        equippedTool = GetComponent<ToolBar>().tools[0];
    }

    void Update()
    {
        mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 1);
        if (elevation.GetTile(cellHovered) != null)
        {
            highlightBlock.GetComponent<SpriteRenderer>().enabled = true;
            highlightBlock.transform.position = new Vector2(cellHovered.x + .5f, cellHovered.y + .5f);
        }
        else
        {
            highlightBlock.GetComponent<SpriteRenderer>().enabled = false;
        }
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
            cellHovered = Vector3Int.FloorToInt(mPos);
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
            StartCoroutine(HitPickaxe(pos));
        }
        else if(tool == 1)
        {
            StartCoroutine(HitAxeCoroutine(pos));
        }
        else if (tool == 2)
        {
            StartCoroutine(SwingSword());
        }
    }

    void PlaceBlock(Vector3Int pos)
    {
        Inventory inv = GetComponent<Inventory>();
        if(elevation.GetTile(pos) == null && colliders.GetTile(pos) == null && inv.amount[inv.activeSlot] > 0)
        {
            elevation.SetTile(pos, inv.slot[inv.activeSlot]);
            colliders.SetTile(pos, collTile);
            inv.Remove(inv.activeSlot);
        }

    }

    IEnumerator SwingSword()
    {
        interrupt = true;
        hands.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(.2f);
        Vector2 cPos = cursor.transform.position;
        Collider2D[] enemies = Physics2D.OverlapAreaAll(new Vector2(cPos.x - 1, cPos.y - .5f), new Vector2(cPos.x + 1, cPos.y + .5f));
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.tag == "enemy")
            {
                enemy.GetComponent<EnemyHealth>().Hit(4);
                enemy.GetComponent<SlimeMove>().Knockback(10, transform.position);
            }
        }
        interrupt = false;
    }

    IEnumerator HitAxeCoroutine(Vector3Int pos)
    {
        interrupt = true;
        hands.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(.2f);
        if (elevation.GetTile(pos) != null && elevation.GetTile(pos) == woodBlock)
        {

            if (breakPoint == null)
            {
                MakeBreakPoint(blockDrops[2]);
            }
            else if (breakPoint != null && cellHovered == Vector3Int.FloorToInt(breakPoint.transform.position - Vector3.up))
            {
                breakPoint.GetComponent<BreakBlock>().Hit();
            }
            else if (breakPoint != null && cellHovered != Vector3Int.FloorToInt(breakPoint.transform.position - Vector3.up))
            {
                Destroy(breakPoint);
                MakeBreakPoint(blockDrops[2]);
            }

        }
        else
        {
            Vector2 cPos = cursor.transform.position;
            Collider2D[] targets = Physics2D.OverlapAreaAll(new Vector2(cPos.x - .5f, cPos.y - .5f), new Vector2(cPos.x + .5f, cPos.y + .5f));
            foreach (Collider2D coll in targets)
            {
                if (coll.tag == "tree")
                {
                    coll.GetComponent<ChopTree>().Hit();
                }
                else if (coll.tag == "enemy")
                {
                    coll.GetComponent<EnemyHealth>().Hit(2);
                    coll.GetComponent<SlimeMove>().Knockback(4, transform.position);
                }
            }
        }
        interrupt = false;

    }

    IEnumerator HitPickaxe(Vector3Int pos)
    {
        interrupt = true;
        hands.GetComponent<Animator>().SetTrigger("Click");
        yield return new WaitForSeconds(.2f);
        if (elevation.GetTile(pos) != null && IndexOfOccurence(rockBlocks, elevation.GetTile(pos)) != -1)
        {
            if (breakPoint == null)
            {
                MakeBreakPoint(blockDrops[IndexOfOccurence(rockBlocks, elevation.GetTile(pos))]);
            }
            else if (breakPoint != null && cellHovered == Vector3Int.FloorToInt(breakPoint.transform.position - Vector3.up))
            {
                breakPoint.GetComponent<BreakBlock>().Hit();
            }
            else if (breakPoint != null && cellHovered != Vector3Int.FloorToInt(breakPoint.transform.position - Vector3.up))
            {
                Destroy(breakPoint);
                MakeBreakPoint(blockDrops[IndexOfOccurence(rockBlocks, elevation.GetTile(pos))]);
            }

        }
        interrupt = false;
    }

    void MakeBreakPoint(GameObject drop)
    {
        breakPoint = Instantiate(breakPointPrefab, new Vector3(cellHovered.x + .5f, cellHovered.y + 1.25f, 0), transform.rotation);
        BreakBlock bb = breakPoint.GetComponent<BreakBlock>();
        bb.dropPrefab = drop;
        bb.elevation = elevation;
        bb.colliders = colliders;
        bb.playerRef = gameObject;
    }

    int IndexOfOccurence(TileBase[] array, TileBase block)
    {
        for(int i = 0; i<array.Length; i++)
        {
            if (array[i] == block) return i;
        }
        return -1;
    }

}
