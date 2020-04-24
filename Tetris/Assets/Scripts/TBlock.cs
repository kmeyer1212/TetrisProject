using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TBlock : MonoBehaviour
{
    private float previousTime;
    public float fallTime = 0.8f;
    public static int height = 20; //This needs to match the background height
    public static int width = 10; //This needs to match the background width
    public Vector3 rotationPoint;
    private static Transform[ , ] grid = new Transform[width, height];

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow)) //Moves block to the left
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) //Moves block to the right
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow)) //Rotates the block 90 degrees
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!ValidMove())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            }
        }

        if(Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? fallTime / 10 : fallTime)) //Pulls block down quickly
        {
            transform.position += new Vector3(0, -1, 0);
            if (!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<PieceSpawner>().NewPiece();
            }
            previousTime = Time.time;
        }
    }

    void CheckForLines()
    {
        for(int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                DeleteLine(i);
                RowDown(i);
            }
        }
    }

    bool HasLine(int i)
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    void DeleteLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for(int y = i; y < height; y++)
        {
            for(int j = 0; j < width; j++)
            {
                if(grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0); 
                }
            }
        }
    }

    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int boxWidth = Mathf.RoundToInt(children.transform.position.x);
            int boxHeight = Mathf.RoundToInt(children.transform.position.y);

            grid[boxWidth, boxHeight] = children;
        }

        isGameOver();
    }

    void isGameOver()
    {
        for(int j = 0; j < width; j++)
        {
            if(grid[j, height - 2] != null)
            {
                FindObjectOfType<GameManager>().gameOver();
            }
        }
    }

    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int boxWidth = Mathf.RoundToInt(children.transform.position.x);
            int boxHeight = Mathf.RoundToInt(children.transform.position.y);

            if(boxWidth < 0 || boxWidth >= width)
            {
                return false;
            }
            if(boxHeight < 0 || boxHeight >= height)
            {
                return false;
            }

            if(grid[boxWidth, boxHeight] != null)
            {
                return false;
            }
        }

        return true;
    }
}
