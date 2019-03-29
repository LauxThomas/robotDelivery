using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ventilator : MonoBehaviour
{

	[SerializeField] private float force;			//Force of the  Ventilator
	[Range( 0, 1)][SerializeField] private int direction;			//0 links / 1 rechts


	private void OnTriggerStay(Collider other)
	{
		if (direction == 0)
		{
			if (other.name == "BottomPart")
			{
				other.attachedRigidbody.MovePosition(other.attachedRigidbody.position + Time.fixedDeltaTime * force * Vector3.left);
			}
			else
			{
				other.attachedRigidbody.AddForce((Vector3.left*force/40), ForceMode.Impulse);
			}

		}
		else
		{
			if (other.name == "BottomPart")
			{
				other.attachedRigidbody.MovePosition(other.attachedRigidbody.position + Time.fixedDeltaTime * force * Vector3.right);
			}
			else
			{
				other.attachedRigidbody.AddForce((Vector3.right*force/40 ), ForceMode.Impulse);
			}
		}
	}


}

