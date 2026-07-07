using UnityEngine;

class FirstPersonCameraMotion : MonoBehaviour
{
    [SerializeField] Transform _navigation, _player;
    [SerializeField] Vector3 _offset = new(0f, 1.5f, 0f);

    void LateUpdate()
    {
        if (!_navigation) return;

        transform.SetPositionAndRotation(
            _player.position + _offset,
            _navigation.rotation
        );
    }
}