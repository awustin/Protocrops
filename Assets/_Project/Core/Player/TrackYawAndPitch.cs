using UnityEngine;

public class TrackYawAndPitch : MonoBehaviour
{
    private InputBroker _inputBroker;
    private Vector2 _screenOrigin;
    private float _xPitch, _yYaw;

    private void Awake()
    {
        _inputBroker = InputBroker.Instance;

        // Origin of movement is the center of screen
        float xOrigin = Screen.width * 0.5f;
        float yOrigin = Screen.height * 0.5f;
        _screenOrigin = new(xOrigin, yOrigin);

        // For a controlled rotation
        _yYaw = (float)360 / Screen.width;
        _xPitch = (float)(90 * .9f) / Screen.height;
    }

    private void Update()
    {
        PlanarToAngularDistance(_inputBroker.Look);
    }

    private void PlanarToAngularDistance(Vector2 screenCoordinates)
    {
        // Distance to screen origin becomes angular translation (rotation)
        Vector2 translatedCoordinates = Translate(screenCoordinates);

        transform.localEulerAngles = new(
            _xPitch * -translatedCoordinates.y,
            _yYaw * translatedCoordinates.x,
            0f
        );
    }

    private Vector2 Translate(Vector2 x) => x - _screenOrigin;
}