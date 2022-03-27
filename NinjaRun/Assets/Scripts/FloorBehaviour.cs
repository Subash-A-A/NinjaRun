using UnityEngine;

public class FloorBehaviour : MonoBehaviour
{
    [SerializeField] Transform FloorEnd;

    private FloorManager fm;

    private void Awake()
    {
        fm = FindObjectOfType<FloorManager>().GetComponent<FloorManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Instantiate(fm.randomFloor(), FloorEnd.position, Quaternion.identity);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GameObject parentFloor = gameObject.transform.parent.gameObject;
        Destroy(parentFloor);
    }
}
