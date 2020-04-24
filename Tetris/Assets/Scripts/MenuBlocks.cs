using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBlocks : MonoBehaviour
{
    public float fallSpeed = 0.9f;
    public float lifeTime = 30.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime, Space.World);
        Destroy(gameObject, lifeTime);
    }
}
