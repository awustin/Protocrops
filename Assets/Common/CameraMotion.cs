using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    [SerializeField] private Transform _character;
    [SerializeField] private Vector3 _offset = new(0f, 4f, -8f);
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (_character == null)
            return;

        Vector3 characterDirection = (_character.transform.forward * -4f) + _offset;
        Vector3 targetPosition = _character.position + characterDirection;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        transform.LookAt(_character.position + Vector3.up * 2f);
    }
}
