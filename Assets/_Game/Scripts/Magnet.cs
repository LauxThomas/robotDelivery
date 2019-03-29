using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class Magnet : MonoBehaviour
{
	[SerializeField] private GameObject waypoint1;
	[SerializeField] private GameObject waypoint2;
	[SerializeField] private float distance;
	[SerializeField] private float speed;
	[SerializeField] private float magnetForceTime;

	private Boolean direction;
	private Boolean test;
	private GameObject package;
	private Boolean cor;


	private void Update()
	{
		if (direction)
		{
			transform.position = (Vector3.MoveTowards(transform.position,waypoint1.transform.position, speed));
			if (Vector3.Distance(transform.position, waypoint1.transform.position) < distance)
			{
				SwitchDirection();
			}

		}
		else
		{
			transform.position =(Vector3.MoveTowards(transform.position,waypoint2.transform.position, speed));
			if (Vector3.Distance(transform.position, waypoint2.transform.position) < distance)
			{
				SwitchDirection();
			}
		}

		if (package != null)
		{
			package.transform.position = Vector3.MoveTowards(package.transform.position, transform.position + Vector3.down, speed );
		}
	}

	public void SwitchDirection()
	{
		direction = !direction;
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			if (!test)
			{
				if (!cor)
				{
					StartCoroutine(testPackage());
				}

			}
			else
			{

				package = other.gameObject.transform.parent.gameObject.GetComponent<PlayerActor_Script>().popPackage();
				StartCoroutine(destroyPackage());

				test = false;
			}
		}

	}

	private IEnumerator testPackage()
	{
		cor = true;
		yield return new WaitForSeconds(magnetForceTime);
		test = true;
		cor = false;
	}

	private IEnumerator destroyPackage()
	{
		yield return new WaitForSeconds(1);
		Destroy(package);
	}
}
