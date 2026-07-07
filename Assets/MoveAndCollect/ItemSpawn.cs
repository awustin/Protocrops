using UnityEngine;
using System.Collections.Generic;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField]
    private GameObject _baseItem;
    [SerializeField]
    private GameObject _ground;
    private readonly int _spawnCount = 15;

    void Awake()
    {
        List<Vector2> positions = new();

        for (int i = 0; i < _spawnCount; i++)
        {
            Instantiate(_baseItem, RandomPoint(positions), Quaternion.identity, transform);
        }
    }

    private Vector3 RandomPoint(List<Vector2> placed)
    {
        if (_ground == null)
            return Vector3.zero;

        Renderer renderer = _ground.GetComponent<Renderer>();
        Bounds bounds = renderer.bounds;

        for (int i = 0; i < 5; i++)
        {
            float x = Mathf.Round(Random.Range(bounds.min.x, bounds.max.x) * 2f) / 2f;
            float z = Mathf.Round(Random.Range(bounds.min.z, bounds.max.z) * 2f) / 2f;

            Vector3 point = new(x, 0.5f, z);

            if (!placed.Contains(point))
                return point;
        }

        return Vector3.zero;
    }
}