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
    public GameObject savedBlock;

    public Vector3 previewPos;
    public Vector3 savedPos;

    public int maxSwaps = 2;
    private int currentSwapNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        hasStarted = GameManager.hasStarted;
        previewPos = new Vector3(-5f, 14.5f, 0);
        savedPos = new Vector3(-5f, 8f, 0);
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
            nextBlock.tag = "currentBlock";
        }
        else
        {
            previewBlock.transform.localPosition = transform.position;
            nextBlock = previewBlock;
            nextBlock.GetComponent<TBlock>().enabled = true;
            nextBlock.tag = "currentBlock";

            previewBlock = Instantiate(Pieces[Random.Range(0, Pieces.Length)], previewPos, Quaternion.identity);
            previewBlock.GetComponent<TBlock>().enabled = false;
        }

        currentSwapNum = 0;
    }

    public void saveBlock (Transform t)
    {
        ++currentSwapNum;

        if(currentSwapNum > maxSwaps)
        {
            return;
        }

        if(savedBlock != null)
        {
            GameObject temp = GameObject.FindGameObjectWithTag("savedBlock");
            temp.transform.localPosition = transform.position;

            savedBlock = Instantiate(t.gameObject);
            savedBlock.GetComponent<TBlock>().enabled = false;
            savedBlock.transform.localPosition = savedPos;
            savedBlock.tag = "savedBlock";

            nextBlock = Instantiate(temp);
            nextBlock.GetComponent<TBlock>().enabled = true;
            temp.transform.localPosition = transform.position;
            nextBlock.tag = "currentBlock";

            DestroyImmediate(t.gameObject);
            DestroyImmediate(temp);
        }
        else
        {
            savedBlock = Instantiate(GameObject.FindGameObjectWithTag("currentBlock"));
            savedBlock.GetComponent<TBlock>().enabled = false;
            savedBlock.transform.localPosition = savedPos;
            savedBlock.tag = "savedBlock";

            DestroyImmediate(GameObject.FindGameObjectWithTag("currentBlock"));
            spawnNext();
        }
    }
}
