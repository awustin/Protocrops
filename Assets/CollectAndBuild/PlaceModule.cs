using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlaceModule : MonoBehaviour , ProtocropsActions.IBuildActions
{
    private ProtocropsActions _actions;
    private ProtocropsActions.BuildActions _buildActions;

    [Header("References")]
    [SerializeField] private Transform _player;
    [SerializeField] private float _placeDistance = 3f;
    
    [Header("Ghost Preview")]
    [SerializeField] private Material _validMaterial;
    [SerializeField] private Material _invalidMaterial;
    [SerializeField] private float _ghostAlpha = 0.5f;
    
    private GameObject _ghostPreview;
    private Transform _selectedPrefab;
    private bool _isPlacing = false;
    private QuickMenuManager _quickMenu;
    private ResourceManager _resourceManager;

    void Awake()
    {
        _player = transform;
        _quickMenu = QuickMenuManager.Instance;
        _resourceManager = ResourceManager.Instance;
        _actions = new ProtocropsActions();
        _buildActions = _actions.Build;
        _buildActions.AddCallbacks(this);
        _quickMenu.OnModuleSelected += SelectModule;
    }

    void OnDestroy()
    {
        _actions.Dispose();
        _quickMenu.OnModuleSelected -= SelectModule;
    }

    void OnEnable()
    {
        _actions.Enable();
    }

    void OnDisable()
    {
        _actions.Disable();
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        RotateGhost(45f);
    }

    public void OnPlace(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        TryPlaceModule();
    }

    public void OnCancelBuild(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        CancelPlacement();
    }
    
    void Update()
    {
        if (_selectedPrefab == null || !_isPlacing) return;

        UpdateGhostPosition();
    }

    public void SelectModule(Transform prefab)
    {
        if (prefab == null)
        {
            CancelPlacement();
            return;
        }

        _selectedPrefab = prefab;
        _isPlacing = true;

        CreateGhostPreview();
    }
    
    void CreateGhostPreview()
    {
        if (_ghostPreview != null) Destroy(_ghostPreview);
        
        if (_selectedPrefab == null) return;
        
        _ghostPreview = Instantiate(_selectedPrefab.gameObject);
        _ghostPreview.name = $"Ghost_{_selectedPrefab.name}";
    
        foreach (var renderer in _ghostPreview.GetComponentsInChildren<Renderer>())
        {
            renderer.material = new Material(renderer.material);
            Color color = renderer.material.color;
            color.a = _ghostAlpha;
            renderer.material.color = color;

            renderer.material.renderQueue = 3000;
        }

        foreach (var collider in _ghostPreview.GetComponentsInChildren<Collider>())
        {
            collider.enabled = false;
        }
        
        UpdateGhostPosition();
    }
    
    void UpdateGhostPosition()
    {
        if (_ghostPreview == null || _player == null) return;

        float playerHeight = 1.8f;
        Vector3 feetPosition = _player.position - Vector3.up * (playerHeight / 2f);
        Vector3 position = feetPosition + _player.forward * _placeDistance;
        _ghostPreview.transform.position = position;
    
        bool isValid = ValidatePlacement(_ghostPreview);

        UpdateGhostColor(isValid);
    }
    
    bool ValidatePlacement(GameObject ghost)
    {
        if (_selectedPrefab.GetComponent<ModuleCost>().cost > _resourceManager.resources)
        {
            // Show message
            return false;
        }

        Collider[] colliders = Physics.OverlapSphere(ghost.transform.position, 2f);
        foreach (var collider in colliders)
        {
            if (collider.transform == _player) continue;
            if (collider.transform.IsChildOf(ghost.transform)) continue;

            if (collider.gameObject.layer == LayerMask.NameToLayer("Unbuildable"))
            {
                return false;
            }
        }

        float maxBuildDistance = 10f;
        if (Vector3.Distance(ghost.transform.position, _player.position) > maxBuildDistance)
        {
            return false;
        }
        
        return true;
    }
    
    void UpdateGhostColor(bool isValid)
    {
        if (_ghostPreview == null) return;
        
        Color color = isValid ? Color.green : Color.red;
        color.a = _ghostAlpha;
        
        foreach (var renderer in _ghostPreview.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = color;
        }
    }
    
    void TryPlaceModule()
    {
        if (_ghostPreview == null || _selectedPrefab == null) return;
        
        if (!ValidatePlacement(_ghostPreview))
        {
            return;
        }

        GameObject placedModule = Instantiate(_selectedPrefab.gameObject, _ghostPreview.transform.position, _ghostPreview.transform.rotation);
        placedModule.name = _selectedPrefab.name;

        foreach (var collider in placedModule.GetComponentsInChildren<Collider>())
        {
            collider.enabled = true;
        }

        // Add to build system (optional)
        // BuildManager.Instance.RegisterModule(placedModule);

        _resourceManager.TryConsumeResources(_selectedPrefab.GetComponent<ModuleCost>().cost);
        CleanupGhost();
        _isPlacing = false;
        _selectedPrefab = null;
    }

    void RotateGhost(float degrees)
    {
        if (_ghostPreview == null) return;
        _ghostPreview.transform.Rotate(Vector3.up, degrees);
    }
    
    void CancelPlacement()
    {
        CleanupGhost();
        _isPlacing = false;
        _selectedPrefab = null;
    }
    
    void CleanupGhost()
    {
        if (_ghostPreview != null)
        {
            Destroy(_ghostPreview);
            _ghostPreview = null;
        }
    }
}