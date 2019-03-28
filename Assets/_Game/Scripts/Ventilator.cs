using System.Collections;
using System.Collections.Generic;
using Boo.Lang;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator
	[Range( 0, 1)][SerializeField] private int direction;			//0 links / 1 rechts



	private void OnTriggerEnter(Collider other)
	{

		if (direction == 0)
		{
			other.attachedRigidbody.AddForce((Vector3.left*force * Time.fixedDeltaTime), ForceMode.Impulse);
		}
		else
		{
			other.attachedRigidbody.AddForce((Vector3.right*force * Time.fixedDeltaTime), ForceMode.Impulse);
		}

		//Debug.Log("Enter");

	}

    private void OnTriggerExit(Collider other)
    {
	    if (direction == 0)
	    {
		    other.attachedRigidbody.AddForce(-(Vector3.left*force * Time.fixedDeltaTime), ForceMode.Impulse);
	    }
	    else
	    {
		    other.attachedRigidbody.AddForce(-(Vector3.right*force * Time.fixedDeltaTime), ForceMode.Impulse);
	    }

    }
}
