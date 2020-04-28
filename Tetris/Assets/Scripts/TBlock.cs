using UnityEngine;

public class TBlock : MonoBehaviour
{
    private float previousTime;
    private float fallTime = GameManager.fallTime;
    public Vector3 rotationPoint;

    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<GameManager>().updateLevel();
        //FindObjectOfType<GameManager>().increaseSpeed();

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
                FindObjectOfType<GameManager>().checkForRows();
                this.enabled = false;
                FindObjectOfType<PieceSpawner>().spawnNext();
            }
            previousTime = Time.time;
        }
    }

    void AddToGrid()
    {
        foreach(Transform children in transform)
        {
            int boxWidth = Mathf.RoundToInt(children.transform.position.x);
            int boxHeight = Mathf.RoundToInt(children.transform.position.y);
            GameManager.grid[boxWidth, boxHeight] = children;
        }

        FindObjectOfType<GameManager>().isGameOver();
    }

    bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int boxWidth = Mathf.RoundToInt(children.transform.position.x);
            int boxHeight = Mathf.RoundToInt(children.transform.position.y);

            if(boxWidth < 0 || boxWidth >= GameManager.width)
            {
                return false;
            }
            if(boxHeight < 0 || boxHeight >= GameManager.height)
            {
                return false;
            }
            if(GameManager.grid[boxWidth, boxHeight] != null)
            {
                return false;
            }
        }

        return true;
    }
}
