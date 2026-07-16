using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Mission", menuName = "Scriptable Objects/Mission")]
public class Mission : ScriptableObject, IDefaultable<Mission>
{
    public bool IsEnabled { get; private set; } = false;

    [SerializeField] private List<MissionStep> _stepSequence;
    private int _inProgress = 0;
    public bool IsComplete => _stepSequence.TrueForAll(step => step.status == MissionStatus.Complete);

    public static Mission GetDefault() => CreateInstance<Mission>();

    public void StartMission()
    {
        _inProgress = 0;
        _stepSequence[_inProgress].status = MissionStatus.InProgress;
        IsEnabled = true;
    }

    public MissionStep GetInProgress()
    {
        MissionStep defaultStep = MissionStep.GetDefault();

        if (!IsEnabled)
            return defaultStep;

        MissionStep step = _stepSequence[_inProgress];

        return step && step.status == MissionStatus.InProgress
            ? step
            : defaultStep;
    }

    public void CompleteStep()
    {
        if (!IsEnabled)
            return;

        _stepSequence[_inProgress].status = MissionStatus.Complete;

        if (_inProgress >= _stepSequence.Count - 1)
            return;

        _inProgress++;
        _stepSequence[_inProgress].status = MissionStatus.InProgress;
    }

    public void SetEnabled(bool value) => IsEnabled = value;
}