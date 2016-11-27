using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonManager_Plane : MonoBehaviour {

	void Awake () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("boo" + other);
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("foo" + other);
    }

    void OnCollisionEnter(Collider other)
    {
        Debug.Log("soo" + other);
    }
}
