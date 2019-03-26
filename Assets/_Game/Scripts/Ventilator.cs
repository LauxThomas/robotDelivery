using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator
	private Vector3 ventForce;
	private Rigidbody rbd;

	private void Awake()
	{
		Vector3 ventForce = new Vector3(force, 0, 0);

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "player")
		{
			Rigidbody rbd = other.GetComponent<Rigidbody>();
			rbd.AddForce(ventForce);
			Debug.Log("Enter");
		}
	}



    private void OnTriggerStay(Collider other)
    {


	    Debug.Log("Stay");
    }

    private void OnTriggerExit(Collider other)
    {
	    if (other.tag == "player")
	    {
		    rbd.AddForce(ventForce);
		    Debug.Log("Exit");
	    }

    }
}
