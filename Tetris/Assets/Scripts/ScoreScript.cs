using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int scoreValue = 0;
    public Text playerScore;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreTextUpdate();
    }

    public void scoreTextUpdate()
    {
        playerScore.text = scoreValue.ToString();
    }
}
