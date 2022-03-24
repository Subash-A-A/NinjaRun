using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera cam;

    private void Update()
    {
        cam.transform.position = new Vector3(0f, transform.position.y, transform.position.z);
    }

}
