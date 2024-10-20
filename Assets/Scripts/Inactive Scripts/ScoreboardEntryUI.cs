using TMPro;
using UnityEngine;

namespace UI.Scoreboards
{
    public class ScoreboardEntryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI entryScoreText = null;

        public void Initialise(ScoreboardEntryData scoreboardEntryData)
        {
            entryScoreText.text = scoreboardEntryData.entryScore.ToString();
        }
    }
}
