using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = scoreText ?? GetComponent<TextMeshProUGUI>();
    }

    public void SetScore(int score)
    {
        if (!scoreText)
        {
            return;
        }
        scoreText.text = score.ToString();
    }
}