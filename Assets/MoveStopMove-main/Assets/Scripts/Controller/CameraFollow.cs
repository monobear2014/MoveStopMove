using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    Vector3 offset = new Vector3(0f, 6f, -6f);

    void Update()
    {
        if (target == null)
        {
            OnInit();
        }
        else
        {
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, target.position + offset, 0.1f);
            transform.position = smoothedPosition;
        }
    }

    void OnInit()
    {
        GameObject targetObject = GameObject.FindWithTag("CameraPoint");
        if (targetObject != null)
        {
            target = targetObject.transform;
            transform.position = target.position;
            Debug.Log("Found CameraPoint");
        }
        else
        {
            Debug.Log("No CameraPoint found in the scene");
        }
    }
}
