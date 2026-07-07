using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Input;

[RequireComponent(typeof(Rigidbody))]
class FirstPersonPlayerMotion : MonoBehaviour, ProtocropsActions.IPlayerActions
{
    [SerializeField] Transform _navigation, _mainCamera;
    private Vector2 _moveInput;
    private ProtocropsActions _actions;
    private ProtocropsActions.PlayerActions _playerActions;
    [SerializeField] private float _groundCheckDistance = 0.3f, _moveSpeed = 5f, maxFallDistance = 20f, _lastGroundedY;
    [SerializeField] private LayerMask _groundMask;
    private bool _isGrounded;
    private Vector3 _groundNormal = Vector3.up;
    private Rigidbody _rb;

    void Awake()
    {
        _actions = new ProtocropsActions();
        _playerActions = _actions.Player;
        _playerActions.AddCallbacks(this);
        _rb = GetComponent<Rigidbody>();

        // Freeze rotation on all axes to prevent physics-based spinning
        _rb.freezeRotation = true;
    }

    void OnDestroy()
    {
        _actions.Dispose();
    }

    void OnEnable()
    {
        _actions.Enable();
    }

    void OnDisable()
    {
        _actions.Disable();
    }

    public void OnInteract(InputAction.CallbackContext context) { }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();
        Debug.Log(_moveInput);
    }

    private void FixedUpdate()
    {
        CheckGround();

        if (!_navigation) return;

        Vector3 forward = _navigation.forward;
        forward.y = 0f;
        forward.Normalize();

        transform.forward = forward;

        if (_moveInput == Vector2.zero)
            return;

        Vector3 moveDirection = transform.forward * _moveInput.y;
        moveDirection.Normalize();

        if (_isGrounded)
        {
            moveDirection = Vector3.ProjectOnPlane(moveDirection, _groundNormal).normalized;
        }

        Vector3 targetVelocity = moveDirection * _moveSpeed;
        Vector3 velocity = _rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;
        _rb.linearVelocity = velocity;

        if (moveDirection.sqrMagnitude > 0.001f)
        {
            transform.forward = moveDirection;
        }
    }

    private void Update()
    {
        if (!_isGrounded)
        {
            float fallDistance = _lastGroundedY - transform.position.y;

            if (fallDistance > maxFallDistance)
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
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
            _lastGroundedY = transform.position.y;
        }
        else
        {
            _isGrounded = false;
        }
    }

    public void OnQuickMenu(InputAction.CallbackContext context) {}

    void ProtocropsActions.IPlayerActions.OnMousePosition(InputAction.CallbackContext context) {}
}