using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoor : MonoBehaviour
{
    public Transform door;
    private float openSpeed = 1f;
    private float liftHeight = 2.3f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = door.position;
        initialRotation = door.rotation;
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        float targetY = initialPosition.y + liftHeight;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0, 0, -90);
        float elapsedTime = 0f;
        float duration = liftHeight / openSpeed;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            
            door.position = Vector3.Lerp(initialPosition, new Vector3(initialPosition.x, targetY, initialPosition.z), t);
            door.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        door.position = new Vector3(initialPosition.x, targetY, initialPosition.z);
        door.rotation = targetRotation;
    }
}
