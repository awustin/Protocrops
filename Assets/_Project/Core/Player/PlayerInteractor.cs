using System;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private EventManager _eventManager;
    private bool _isInteracting;
    private Vector3 _raycastPosOffset = new(0f, 0.7f, 0f);
    [SerializeField] Transform _gyroscope;

    private void Awake()
    {
        _eventManager = EventManager.Instance;
        _isInteracting = false;
    }

    private void OnEnable()
    {
        _eventManager.Interact += OnInteractEvent;
    }

    private void OnDisable()
    {
        _eventManager.Interact -= OnInteractEvent;
    }

    void Update()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = _gyroscope.forward;

        Debug.DrawRay(
            rayOrigin + _raycastPosOffset,
            rayDirection * 4.0f,
            Color.yellow, 0.1f
        );
    }

    private void OnInteractEvent()
    {
        if (_isInteracting)
            return;

        _isInteracting = true;
        ForwardRaycast();
    }

    private void ForwardRaycast()
    {
        if (Physics.Raycast(
            transform.position + _raycastPosOffset, _gyroscope.forward, out RaycastHit hit, 4.0f
        ))
        {
            if (hit.collider.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(transform);
            }
        }

        _isInteracting = false;
    }

}