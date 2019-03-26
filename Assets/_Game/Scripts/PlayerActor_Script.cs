using System;
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
	[SerializeField] private int speed = 1;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public Collider ColliderBottom { get; private set; }
	public Transform PlayerTransform { get; private set; }

	private IInputProvider _playerInputProvider;

	private void Awake()
	{
		_playerInputProvider = GetComponent<IInputProvider>();

		RigidbodyTop = _gameObjectTop.GetComponent<Rigidbody>();
		ColliderTop = _gameObjectTop.GetComponent<Collider>();

		ColliderBottom = _gameObjectBottom.GetComponent<Collider>();
		RigidbodyBottom = _gameObjectBottom.GetComponent<Rigidbody>();

		PlayerTransform = _gameObjectPlayer.GetComponent<Transform>();
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
    }

    void CalculatePlayerPosition()
    {
	    Vector3 vectorBetweenRigids = RigidbodyTop.position - RigidbodyBottom.position;
	    PlayerTransform.position = RigidbodyBottom.position + 0.5f * vectorBetweenRigids;
	    float angleRadian = Mathf.Atan2(vectorBetweenRigids.y, vectorBetweenRigids.x);
	    TurnPlayerToAngle(RadianToDegree(angleRadian));
    }

    void ProcessGravity()
    {

    }

    private void TurnPlayerToAngle(float angle)
    {
	    Vector3 euler = PlayerTransform.eulerAngles;
	    euler = new Vector3(0,0, angle - euler.z);
		PlayerTransform.eulerAngles = euler;
    }

    private float RadianToDegree(float angle)
    {
	    return ((angle * (180f / Mathf.PI)) + 270) % 360;
    }
}
