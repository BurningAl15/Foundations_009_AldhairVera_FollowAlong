using System.Collections;
using UnityEngine;

public enum DoorAnimation
{
    RotateDoor,
    UpDoor,
    DisappearDoor
}

public class DoorTrigger : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private GameObject _door;
    [SerializeField] private DoorAnimation _doorAnimation;

    [Header("Animation Settings")]
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private float _animationDuration = 0.5f;

    [Header("Specific Animation Variables")]
    [SerializeField] private Vector3 _rotationTarget = new Vector3(0, -90, 0);
    [SerializeField] private float _upPositionOffset = 3f;

    private bool isDoorOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            StartCoroutine(HandleDoorAnimation());
            isDoorOpen = true;
        }
    }

    private IEnumerator HandleDoorAnimation()
    {
        Transform doorTransform = _door.transform;
        float elapsedTime = 0;

        Vector3 initialPos = doorTransform.position;
        Quaternion initialRot = doorTransform.rotation;

        while (elapsedTime < _animationDuration)
        {
            float normalizedTime = elapsedTime / _animationDuration;
            float curveValue = _animationCurve.Evaluate(normalizedTime);

            switch (_doorAnimation)
            {
                case DoorAnimation.RotateDoor:
                    doorTransform.rotation = Quaternion.Slerp(initialRot, Quaternion.Euler(_rotationTarget), curveValue);
                    break;

                case DoorAnimation.UpDoor:
                    doorTransform.position = Vector3.Lerp(initialPos, initialPos + Vector3.up * _upPositionOffset, curveValue);
                    break;

                case DoorAnimation.DisappearDoor:
                    // Optionally fade or scale down for better visuals
                    doorTransform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, curveValue);
                    break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure final state
        switch (_doorAnimation)
        {
            case DoorAnimation.RotateDoor:
                doorTransform.rotation = Quaternion.Euler(_rotationTarget);
                break;

            case DoorAnimation.UpDoor:
                doorTransform.position = initialPos + Vector3.up * _upPositionOffset;
                break;

            case DoorAnimation.DisappearDoor:
                _door.SetActive(false);
                break;
        }
    }
}

