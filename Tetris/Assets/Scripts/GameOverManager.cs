using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public Text finalScoreText;

    // Start is called before the first frame update
    void Start()
    {
        finalScoreText.text = "Score: " + GameManager.finalScoreVal.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
