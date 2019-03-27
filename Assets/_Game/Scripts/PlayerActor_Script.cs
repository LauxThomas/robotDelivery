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

	// For The Height of the PlayerModel
	[SerializeField] [Range(1,10)] private int height = 1;
	private float totalHeight = 1;

	[SerializeField] private int speed = 1;
	[SerializeField] private int forceJet = 1;
	[SerializeField] private int gravityForceTop = 1;
	[SerializeField] private int gravityForceBottom = 1;
	[SerializeField] private int stableAngle = 10;

	private bool isGrounded = false;
	private bool pushingJetToLeft = false;
	private bool isJetActive = false;

	private Vector3 directionalJetVector = Vector3.zero;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public SphereCollider ColliderBottom { get; private set; }
	public Transform PlayerTransform { get; private set; }

	private IInputProvider _playerInputProvider;

	public AnimationCurve plot = new AnimationCurve();

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
		// Calculating the Height of the Player to keep the Rigidbodys close together
		totalHeight = RigidbodyTop.position.y - RigidbodyBottom.position.y;

	}


	private void Update()
	{

	}

	void FixedUpdate()
	{
		ProcessInput();
		ProcessGravity();
		CalculatePlayerPosition();
	}


	/**
	 *
	 */
	void ProcessInput()
	{
		Vector3 direction = _playerInputProvider.Direction();

	    // Takes input to change the rigidbody of the wheel and moves it around
	    RigidbodyBottom.MovePosition(RigidbodyBottom.position + Time.fixedDeltaTime * speed * direction);
	    if (((isTopRightOfBody() && direction.normalized.Equals(Vector3.right)) || (!isTopRightOfBody() && direction.normalized.Equals(Vector3.left)))
	        && (getAngleOfCharacter() <= stableAngle || getAngleOfCharacter() >= 360-stableAngle))
	    {
		    RigidbodyTop.velocity = Vector3.zero;
		    Debug.Log("STABLE" + (isTopRightOfBody()?"Right":"Left"));
	    }


	    //Debug.Log(directionalJetVector);
	    if (_playerInputProvider.ForceFromJet() > 0)
	    {
		    if (isJetActive)
		    {
			    RigidbodyTop.MovePosition(RigidbodyTop.position + directionalJetVector * Time.fixedDeltaTime * forceJet);

			    Vector3 bottomPosition = RigidbodyBottom.position;
			    Vector3 vectorBetweenRigids = RigidbodyTop.position - bottomPosition;

			    directionalJetVector = new Vector3(vectorBetweenRigids.y*(pushingJetToLeft?-1:1), vectorBetweenRigids.x*(pushingJetToLeft?1:-1));
		    }
		    else
		    {
			    pushingJetToLeft = isTopRightOfBody();
			    isJetActive = true;
			    RigidbodyTop.velocity = Vector3.zero;
		    }
	    }
	    else
	    {
		    isJetActive = false;
		    directionalJetVector = Vector3.zero;
	    }

    }

    void CalculatePlayerPosition()
    {
	    Vector3 bottomPosition = RigidbodyBottom.position;
	    Vector3 vectorBetweenRigids = RigidbodyTop.position - bottomPosition;
	    Vector3 newTopPosition = bottomPosition + vectorBetweenRigids.normalized * totalHeight;

	    PlayerTransform.position = new Vector3(bottomPosition.x, bottomPosition.y - ColliderBottom.radius, bottomPosition.z);
	    RigidbodyTop.MovePosition(newTopPosition);
	    // RigidbodyTop.velocity = Vector3.zero;

	    float angleRadian = Mathf.Atan2(vectorBetweenRigids.y, vectorBetweenRigids.x);
	    TurnPlayerToAngle(RadianToDegree(angleRadian));
    }

    void ProcessGravity()
    {
		RigidbodyBottom.MovePosition(RigidbodyBottom.position + Vector3.down * Time.fixedDeltaTime * gravityForceBottom);
		RigidbodyTop.AddForce(Vector3.down * Time.fixedDeltaTime * gravityForceTop);

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
