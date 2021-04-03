using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolBar : MonoBehaviour
{
    public GameObject toolBar;
    public GameObject hands;
    public Sprite[] toolTypes = new Sprite[4];

    public int[] tools = new int[4];


    private void Start()
    {
        tools[0] = 0;
        tools[1] = 1;
        tools[2] = -1;
        tools[3] = -1;
        UpdateToolBar();
    }

    public void SwitchTools(bool left)
    {
        int[] newTools = new int[4];
        if (left)
        {
            for (int i = 0; i < 3; i++)
            {
                newTools[i] = tools[i + 1];
            }
            newTools[3] = tools[0];
        }
        else
        {
            for (int i = 3; i > 0; i--)
            {
                newTools[i] = tools[i - 1];
            }
            newTools[0] = tools[3];
        }
        tools = newTools;
        UpdateToolBar();
    }

    public void AddTool(int tool)
    {
        for(int i = 2; i<4; i++)
        {
            if(tools[i] == -1)
            {
                tools[i] = tool;
                UpdateToolBar();
                return;
            }
        }
    }

    void UpdateToolBar()
    {

        for (int i = 0; i < 4; i++)
        {
            if (tools[i] != -1)
            {
                toolBar.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = toolTypes[tools[i]];
            }
            else
            {
                toolBar.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = null;
            }

        }
        hands.GetComponent<Animator>().SetFloat("Tool", tools[0]);

    }

}
