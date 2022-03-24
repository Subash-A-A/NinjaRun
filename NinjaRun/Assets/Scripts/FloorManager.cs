using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public GameObject[] floors;

    public GameObject randomFloor()
    {
        int index = Random.Range(0, floors.Length);
        return floors[index];
    }

}
