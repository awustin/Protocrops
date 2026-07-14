using UnityEngine;

public class CameraYawAndPitch : MonoBehaviour
{
    [SerializeField] Transform _gyroscope, _player;

    void LateUpdate()
    {
        if (!_gyroscope) return;

        transform.rotation = _gyroscope.rotation;
    }
}