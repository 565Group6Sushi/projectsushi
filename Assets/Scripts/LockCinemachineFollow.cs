using UnityEngine;

using Cinemachine;
using static Cinemachine.CinemachineCore;

[SaveDuringPlay]
[ExecuteInEditMode]
public class LockCinemachineFollow : CinemachineExtension
{
    [SerializeField] private Vector3 _lockPosition;
    [SerializeField] private bool _lockX;
    [SerializeField] private bool _lockY;
    [SerializeField] private bool _lockZ;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, Stage stage, ref CameraState state, float deltaTime)
    {
        if (CanLockPosition(stage))
            LockPosition(ref state);
    }

    private bool CanLockPosition(Stage stage)
    {
        return enabled && stage == Stage.Body;
    }
    private void LockPosition(ref CameraState state)
    {
        state.RawPosition = GetLockedPosition(state.RawPosition);
    }
    private Vector3 GetLockedPosition(Vector3 position)
    {
        return new Vector3(
            _lockX ? _lockPosition.x : position.x,
            _lockY ? _lockPosition.y : position.y,
            _lockZ ? _lockPosition.z : position.z
        );
    }
}