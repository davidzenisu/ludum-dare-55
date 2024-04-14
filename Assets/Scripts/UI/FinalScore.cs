using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = scoreText ?? GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        scoreText.text = WheelController.Instance.GetScore().ToString();
    }
}