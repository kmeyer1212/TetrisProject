using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceSpawner : MonoBehaviour
{
    public GameObject[] Pieces;
    public static bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        NewPiece();
    }

    public void NewPiece()
    {
        Instantiate(Pieces[Random.Range(0, Pieces.Length)], transform.position, Quaternion.identity);
        if (hasStarted)
        {
            ScoreScript.scoreValue += 100;
        }

        hasStarted = true;
    }
}
