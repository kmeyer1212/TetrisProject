using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void gameOver()
    {
        ScoreScript.scoreValue = 0;
        PieceSpawner.hasStarted = false;
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }
}
