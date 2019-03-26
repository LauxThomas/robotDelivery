using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator
	private Vector3 ventForce;

	private void Awake()
	{
		Vector3 ventForce = new Vector3(force, 0, 0);

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			Debug.Log("Enter");
		}
	}



    private void OnTriggerStay(Collider other)
    {
	    other.attachedRigidbody.AddForce(Vector3.right);
	    Debug.Log(other.attachedRigidbody.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
	    other.attachedRigidbody.velocity = Vector3.zero;
    }
}
