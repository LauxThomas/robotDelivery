using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator
	private Vector3 vecForce;


	private void Awake()
	{
		vecForce = new Vector3(force, 0, 0);
	}

	private void OnCollisionEnter(Collision other)
    {
	    other.rigidbody.AddForce(vecForce);
    }

    private void OnCollisionStay(Collision other)
    {
	    other.rigidbody.AddForce(vecForce);
    }

    private void OnCollisionExit(Collision other)
    {
	    other.rigidbody.AddForce(Vector3.zero);
    }
}
