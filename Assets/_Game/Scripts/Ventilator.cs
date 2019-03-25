using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator


	private void Awake()
	{
	}

	private void OnCollisionEnter(Collision other)
    {
	    other.transform.LookAt(-transform.position);
	    Debug.Log("Enter");
    }

    private void OnCollisionStay(Collision other)
    {
	    other.transform.Translate(-transform.position);
	    Debug.Log("Stay");
    }

    private void OnCollisionExit(Collision other)
    {
	    other.transform.Translate(0,0,0);
	    Debug.Log("Exit");
    }
}
