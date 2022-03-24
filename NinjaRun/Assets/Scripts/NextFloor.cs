using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextFloor : MonoBehaviour
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
}
