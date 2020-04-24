using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class MenuSpawn : MonoBehaviour
{
    public GameObject[] Pieces;
    public bool stop = false;
    public float spawnTime;
    public float spawnDelay;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("NewPiece", spawnTime, spawnDelay);
    }

    public void NewPiece()
    {
        Instantiate(Pieces[Random.Range(0, Pieces.Length)], transform.position, Quaternion.identity);
        if (stop)
        {
            CancelInvoke("NewPiece");
        }
    }
}
