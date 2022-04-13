using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    public float forceMultiply = 1;
    public float dropMultiply = 1;
    public float drop = 1;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("Ω¯»Î‰ˆŒ–");
            Vector3 shipPos = new Vector3(other.transform.position.x, 0, other.transform.position.z);
            Vector3 whirlPos = new Vector3(transform.position.x, 0, transform.position.z);
            var force = (whirlPos - shipPos).normalized * forceMultiply;
            other.GetComponent<Rigidbody>().AddForce(force);
        }
    }
}
