using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static int height = 20; //This needs to match the background height
    public static int width = 10; //This needs to match the background width
    public static Transform[,] grid = new Transform[width, height];

    public AudioSource source;
    public AudioClip deleteLine;

    private int rowsCleared = 0;
    public static int currentLevel = 0;
    public static float fallTime = 1.0f;
    public static bool hasStarted = false;
    private bool over = false;

    public Text levelText;
    public Text numLines;
    public Text score;
    public Text goal;

    public static int scoreValue = 0;
    public static int finalScoreVal;
    private static int nextGoal = 3;

    public void gameOver()
    {
        over = true;
        nextGoal = 3;
        scoreValue = 0;
        hasStarted = false;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public void updateScore()
    {
        if (!over)
        {
            scoreValue = (scoreValue + (100 * (currentLevel + 1)));
            score.text = scoreValue.ToString();

            finalScoreVal = scoreValue;
        }
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

        ++rowsCleared;
        numLines.text = rowsCleared.ToString();
        increaseSpeed();
    }

    public void updateLevel()
    {
        currentLevel = rowsCleared / 3;
        nextGoal = (currentLevel + 1) * 3;

        levelText.text = "Level " + (currentLevel + 1).ToString();
        goal.text = nextGoal.ToString();
    }

    public void increaseSpeed()
    {
        fallTime = 1.0f - ((float)currentLevel * 0.1f);
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

        updateScore();
    }
}
