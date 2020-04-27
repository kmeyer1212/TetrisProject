using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceSpawner : MonoBehaviour
{
    public GameObject[] Pieces;
    public static bool hasStarted = false;

    public GameObject previewBlock;
    public GameObject nextBlock;
    public Vector2 previewPos = new Vector3(-6.5f, 15, 0);

    // Start is called before the first frame update
    void Start()
    {
        spawnNext();
    }

    public void spawnNext()
    {
        if (!hasStarted)
        {
            hasStarted = true;

            nextBlock = Instantiate(Pieces[Random.Range(0, Pieces.Length)], transform.position, Quaternion.identity);
            previewBlock = Instantiate(Pieces[Random.Range(0, Pieces.Length)], previewPos, Quaternion.identity);
            previewBlock.GetComponent<TBlock>().enabled = false;
        }
        else
        {
            previewBlock.transform.localPosition = transform.position;
            nextBlock = previewBlock;
            nextBlock.GetComponent<TBlock>().enabled = true;

            previewBlock = Instantiate(Pieces[Random.Range(0, Pieces.Length)], previewPos, Quaternion.identity);
            previewBlock.GetComponent<TBlock>().enabled = false;

            ScoreScript.scoreValue += 100;
        }
    }
}
