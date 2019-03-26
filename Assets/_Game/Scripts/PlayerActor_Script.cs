﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerActor_Script : MonoBehaviour
{
	[SerializeField] private GameObject _gameObjectTop;
	[SerializeField] private GameObject _gameObjectBottom;
	[SerializeField] private GameObject _gameObjectPlayer;

	[SerializeField] [Range(1,10)] private int height = 1;
	private float totalHeight = 1;
	[SerializeField] private int speed = 1;
	[SerializeField] private int forceJet = 1;
	[SerializeField] private int gravityForceTop = 1;
	[SerializeField] private int gravityForceBottom = 1;

	private bool isGrounded = false;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public SphereCollider ColliderBottom { get; private set; }
	public Transform PlayerTransform { get; private set; }

	private IInputProvider _playerInputProvider;

	private void Awake()
	{
		_playerInputProvider = GetComponent<IInputProvider>();

		RigidbodyTop = _gameObjectTop.GetComponent<Rigidbody>();
		ColliderTop = _gameObjectTop.GetComponent<Collider>();

		ColliderBottom = _gameObjectBottom.GetComponent<SphereCollider>();
		RigidbodyBottom = _gameObjectBottom.GetComponent<Rigidbody>();

		PlayerTransform = _gameObjectPlayer.GetComponent<Transform>();
	}

	private void Start()
	{
		totalHeight = RigidbodyTop.position.y - RigidbodyBottom.position.y;

	}


	private void Update()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{
		ProcessInput();
		ProcessGravity();
		CalculatePlayerPosition();
	}

	void ProcessInput()
    {
	    RigidbodyBottom.MovePosition(RigidbodyBottom.position + Time.fixedDeltaTime * speed * (Vector3) _playerInputProvider.Direction());

	    if (isTopRightOfBody())
	    {
		    // getAngleOfCharacter() + 90;
	    }
	    else
	    {
		    // +getAngleOfCharacter() - 90;
	    }

    }

    void CalculatePlayerPosition()
    {
	    Vector3 bottomPosition = RigidbodyBottom.position;
	    Vector3 vectorBetweenRigids = RigidbodyTop.position - bottomPosition;
	    Vector3 newTopPosition = bottomPosition + vectorBetweenRigids.normalized * totalHeight;

	    PlayerTransform.position = new Vector3(bottomPosition.x, bottomPosition.y - ColliderBottom.radius, bottomPosition.z);
	    RigidbodyTop.position = newTopPosition;

	    float angleRadian = Mathf.Atan2(vectorBetweenRigids.y, vectorBetweenRigids.x);
	    TurnPlayerToAngle(RadianToDegree(angleRadian));
    }

    void ProcessGravity()
    {
		RigidbodyBottom.MovePosition(RigidbodyBottom.position + Vector3.down * Time.fixedDeltaTime * gravityForceBottom);
		RigidbodyTop.AddForce(Vector3.down * Time.fixedDeltaTime * gravityForceTop, ForceMode.Impulse);

    }

    private void OnCollisionEnter(Collision other)
    {
	    throw new System.NotImplementedException();
    }

    private void OnCollisionExit(Collision other)
    {
	    throw new System.NotImplementedException();
    }

    private void TurnPlayerToAngle(float angle)
    {
	    Vector3 euler = PlayerTransform.eulerAngles;
	    euler = new Vector3(0,0, angle);
		PlayerTransform.eulerAngles = euler;
    }

    private float RadianToDegree(float angle)
    {
	    return ((angle * (180f / Mathf.PI)) + 270) % 360;
    }

    private float getAngleOfCharacter()
    {
	    Vector3 vectorBetweenRigids = RigidbodyTop.position - RigidbodyBottom.position;
	    float angleRadian = Mathf.Atan2(vectorBetweenRigids.y, vectorBetweenRigids.x);
	    return RadianToDegree(angleRadian);
    }

    private bool isTopRightOfBody()
    {
	    return (RigidbodyTop.position.x > RigidbodyBottom.position.x);
    }

}
