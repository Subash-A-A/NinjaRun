using UnityEngine;

public class DestroyFloor : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameObject parentFloor = gameObject.transform.parent.gameObject;
        Destroy(parentFloor, 0.5f);
    }
}
