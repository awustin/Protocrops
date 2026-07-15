using Unity.Properties;
using UnityEngine;

[CreateAssetMenu(fileName = "BioScoreData", menuName = "Scriptable Objects/BioScoreData")]
public class BioScoreData : ScriptableObject
{
    [CreateProperty] public float BioScore = 0f;
}
