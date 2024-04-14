using UnityEngine;

public class ScoreArea : MonoBehaviour
{
    public int score;

    void OnTriggerEnter2D(Collider2D col)
    {
        var scoreIndicator = col.gameObject.GetComponentInParent<ScoreIndicator>();
        if (!scoreIndicator)
        {
            return;
        }
        scoreIndicator.SetScore(score);
    }
}
