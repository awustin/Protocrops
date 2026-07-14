using UnityEngine;

class MoveOnGround : MonoBehaviour
{
    [SerializeField] Transform _gyroscope;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private LayerMask _groundMask;
    private readonly float _groundCheckDistance = 0.3f, _moveSpeed = 5f;
    private InputBroker _inputBroker;
    private bool _isGrounded;
    private Vector3 _groundNormal = Vector3.up;

    private void Awake()
    {
        _inputBroker = InputBroker.Instance;    
    }

    private void LateUpdate()
    {
        CheckGround();

        if (!_gyroscope || _inputBroker.Move == Vector2.zero)
            return;

        // Follow the gyroscope's direction and ignore vertical components
        Vector3 moveDirection =
            _gyroscope.forward * _inputBroker.Move.y +
            _gyroscope.right * _inputBroker.Move.x;
        moveDirection.y = 0f;
        moveDirection.Normalize();

        if (_isGrounded)
            moveDirection = Vector3.ProjectOnPlane(moveDirection, _groundNormal).normalized;

        // Rigidbody's kinematic physics
        Vector3 velocity = _rb.linearVelocity;
        Vector3 targetVelocity = moveDirection * _moveSpeed;

        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;

        _rb.linearVelocity = velocity;
    }

    private void CheckGround()
    {
        Ray ray = new(
            transform.position + Vector3.up * 0.1f,
            Vector3.down
        );

        if (Physics.Raycast(ray, out RaycastHit hit, _groundCheckDistance, _groundMask))
        {
            _isGrounded = true;
            _groundNormal = hit.normal;
        }
        else
        {
            _isGrounded = false;
        }
    }
}