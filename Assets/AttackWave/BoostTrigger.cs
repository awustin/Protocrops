using UnityEngine;

public class BoostTrigger : MonoBehaviour
{
    [SerializeField] private Transform _character;
    private bool _boosted;

    void Awake()
    {
        _boosted = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!_boosted)
        {
            _character.GetComponent<CharacterAttack>().BoostDamage(10);
            _boosted = true;
        }
    }
}