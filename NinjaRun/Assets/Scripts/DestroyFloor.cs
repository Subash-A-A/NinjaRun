using UnityEngine;

public class DestroyFloor : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        GameObject parentFloor = gameObject.transform.parent.gameObject;
        Destroy(parentFloor);
    }
}
