using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameScreen : BaseScreen
{
    [SerializeField] private TextMeshProUGUI scoreTextMeshPro;
    
    private void OnEnable()
    {
        Level.OnScoreChanged += Level_OnScoreChanged;
    }

    private void OnDisable()
    {
        Level.OnScoreChanged -= Level_OnScoreChanged;
    }

    private void Level_OnScoreChanged(int score)
    {
        scoreTextMeshPro.text = score.ToString();
    }
}
