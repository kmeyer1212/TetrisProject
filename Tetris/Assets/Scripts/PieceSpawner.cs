using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieceSpawner : MonoBehaviour
{
    public GameObject[] Pieces;
    private bool hasStarted;

    public GameObject previewBlock;
    public GameObject nextBlock;
    //public Vector3 previewPos = new Vector3(500f, 4.1f, 0);
    public Vector2 previewPos = new Vector2(-3.5f, 1);

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = GameManager.hasStarted;
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
        }
    }
}
