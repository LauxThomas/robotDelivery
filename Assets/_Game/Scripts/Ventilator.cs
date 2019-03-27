using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator



	private void OnTriggerEnter(Collider other)
	{

		other.attachedRigidbody.AddForce(Vector3.right*force * Time.fixedDeltaTime, ForceMode.Impulse);
		if (other.tag == "player")
		{

		}

		//Debug.Log("Enter");

	}



    private void OnTriggerStay(Collider other)
    {

	    //Debug.Log(other.attachedRigidbody.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
	    other.attachedRigidbody.AddForce(-(Vector3.right*force * Time.fixedDeltaTime), ForceMode.Impulse);
    }
}
