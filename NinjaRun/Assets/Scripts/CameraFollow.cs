using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] float camSmoothFactor = 3.5f;

    private PlayerController pc;
    private float currentLane = 0f;

    private void Awake()
    {
        pc = gameObject.transform.parent.GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        currentLane = Mathf.Lerp(currentLane, pc.camCurrentLane, camSmoothFactor * Time.deltaTime);
        cam.transform.position = new Vector3(currentLane, transform.position.y, transform.position.z);
    }
}
