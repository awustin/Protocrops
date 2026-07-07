using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
    private EventManager _eventManager;
    private bool _battleActive;
    [SerializeField] private Transform _battleCanvas;
    [SerializeField] private Transform _character;
    [SerializeField] private Transform _enemy;

    void Awake()
    {
        _eventManager = EventManager.Instance;
        _battleActive = false;
    }

    void OnEnable()
    {
        _eventManager.OnBattleStatus += OnBattleStatus;
    }

    void OnDisable()
    {
        _eventManager.OnBattleStatus -= OnBattleStatus;
    }

    public bool IsBattleActive() { return _battleActive; }

    private void OnBattleStatus(object sender, string status)
    {
        if (status == "ON")
        {
            _battleCanvas.gameObject.SetActive(true);
            _battleActive = true;
            _character.GetComponent<Character>().enabled = false;
            _character.GetComponent<CharacterAttack>().enabled = true;
            _enemy.GetComponent<EnemyAttack>().enabled = true;
        }
        else
        {
            _battleCanvas.gameObject.SetActive(false);
            _battleActive = false;
            _character.GetComponent<Character>().enabled = true;
            _character.GetComponent<CharacterAttack>().enabled = false;
            _enemy.GetComponent<EnemyAttack>().enabled = false;
        }
    }
}