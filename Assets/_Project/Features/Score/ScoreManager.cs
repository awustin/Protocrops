using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private BioScoreData _bioScore;
    private EventManager _eventManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
    }

    private void OnEnable()
    {
        _eventManager.UpdateScore += OnUpdateScore;

        // Reset because domain reload is disabled (dev)
        _bioScore.BioScore = 0f;
    }

    private void OnDisable()
    {
        _eventManager.UpdateScore -= OnUpdateScore;
    }

    private void OnUpdateScore(object sender, float value)
    {
        _bioScore.BioScore += value;
    }
}