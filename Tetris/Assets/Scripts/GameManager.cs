using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static int height = 20; //This needs to match the background height
    public static int width = 10; //This needs to match the background width
    public static Transform[,] grid = new Transform[width, height];
    public AudioSource source;
    public AudioClip deleteLine;

    public void gameOver()
    {
        ScoreScript.scoreValue = 0;
        PieceSpawner.hasStarted = false;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void checkForRows()
    {
        for(int i = height - 1; i >= 0; i--)
        {
            if (HasLine(i))
            {
                clearLine(i);
                RowDown(i);
            }
        }
    }
    bool HasLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
            {
                return false;
            }
        }

        return true;
    }

    public void clearLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            source.PlayOneShot(deleteLine);
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
    }

    void RowDown(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    public void isGameOver()
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, height - 2] != null)
            {
                gameOver();
            }
        }
    }
}
