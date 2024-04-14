using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    private Rotator _rotator;

    private void Start()
    {
        _rotator = transform.parent.GetComponentInChildren<Rotator>();
    }

    public void SetScore(int score)
    {
        if (!_rotator)
        {
            return;
        }
        _rotator.SetScore(score);
    }
}
