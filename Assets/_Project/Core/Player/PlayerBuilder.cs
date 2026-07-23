using UnityEngine;

public class PlayerBuilder : MonoBehaviour
{
    [SerializeField] private Transform _gyroscope; // _gyroscope.forward marks the forward direction
    [SerializeField] private float _buildDistance = 2f;
    [SerializeField] private LayerMask _placementMask = ~0;

    private GameObject _selectedPrefab;
    private bool _isInteracting;
    private readonly Vector3 _originOffset = new(0f, 0.7f, 0f);
    private EventManager _eventManager;
    private GameModeManager _gameModeManager;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _gameModeManager = GameModeManager.Instance;
        _isInteracting = false;
    }

    private void OnEnable()
    {
        _eventManager.Interact += OnInteractEvent;
        _eventManager.InventorySelectModule += OnModuleSelected;
    }

    private void OnDisable()
    {
        _eventManager.Interact -= OnInteractEvent;
        _eventManager.InventorySelectModule -= OnModuleSelected;
    }

    private void OnInteractEvent()
    {
        if (_isInteracting)
            return;

        TryPlaceModule();
    }

    private void OnModuleSelected(object sender, string prefabPath)
    {
        _selectedPrefab = Resources.Load<GameObject>(prefabPath);

        if (_selectedPrefab == null)
        {
            Debug.LogError($"Failed to load prefab from Resources path: {prefabPath}");
            _gameModeManager.SetMode(GameMode.Normal);
        }
    }

    private bool TryPlaceModule()
    {
        if (_isInteracting || _selectedPrefab == null)
            return false;

        Vector3 origin = transform.position + _originOffset;
        Vector3 placementPosition = origin + _gyroscope.forward * _buildDistance;

        // Find the ground beneath the placement position.
        Vector3 rayOrigin = placementPosition + Vector3.up * 5f;
        Vector3 up = Vector3.up;

        if (Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hit, 10f, _placementMask))
        {
            placementPosition = hit.point;
            up = hit.normal;
        }

        // Preserve the player's facing direction while aligning with the ground.
        Vector3 forward = Vector3.ProjectOnPlane(_gyroscope.forward, up).normalized;

        if (forward.sqrMagnitude < 0.001f)
            forward = Vector3.Cross(transform.right, up).normalized;

        _eventManager.SendGameEvent(EventName.PlaceModule,
            (_selectedPrefab, placementPosition, Quaternion.LookRotation(forward, up))
        );

        _isInteracting = false;

        return true;
    }
}