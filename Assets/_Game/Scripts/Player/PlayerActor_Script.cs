using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class PlayerActor_Script : MonoBehaviour
{
	[SerializeField] private GameObject gameObjectTop;
	[SerializeField] private GameObject gameObjectBottom;
	[SerializeField] private GameObject gameObjectPlayer;
	[SerializeField] private GameObject gameObjectTopModel;


	[SerializeField] private Material robotHeadNeutral;
	[SerializeField] private Material robotHeadLeft;
	[SerializeField] private Material robotHeadRight;
	[SerializeField] private Material robotHeadFail;



	// For The Height of the PlayerModel
	[SerializeField] [Range(1,10)] private int height = 1;
	private float totalHeight = 1;

	[SerializeField] private float speed = 1;
	[SerializeField] private float forceJet = 1;
	[SerializeField] private float gravityForceTop = 1;
	[SerializeField] private float gravityForceBottom = 1;
	[SerializeField] private float stableAngle = 10;

	private bool isGrounded = false;
	private bool pushingJetToLeft = false;
	private bool isJetActive = false;

	private Vector3 directionalJetVector = Vector3.zero;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public SphereCollider ColliderBottom { get; private set; }
	public Transform PlayerTransform { get; private set; }
	public SkinnedMeshRenderer TopSkinnedMeshRenderer { get; private set; }

	private IInputProvider _playerInputProvider;

	public AnimationCurve plot = new AnimationCurve();

	private void Awake()
	{
		_playerInputProvider = GetComponent<IInputProvider>();

		RigidbodyTop = gameObjectTop.GetComponent<Rigidbody>();
		ColliderTop = gameObjectTop.GetComponent<Collider>();

		ColliderBottom = gameObjectBottom.GetComponent<SphereCollider>();
		RigidbodyBottom = gameObjectBottom.GetComponent<Rigidbody>();

		PlayerTransform = gameObjectPlayer.GetComponent<Transform>();
		TopSkinnedMeshRenderer = gameObjectTopModel.GetComponent<SkinnedMeshRenderer>();
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
		ChangeTopTexture();
	}


	/**
	 *
	 */
	void ProcessInput()
	{
		Vector3 direction = _playerInputProvider.Direction();

	    // Takes input to change the rigidbody of the wheel and moves it around
	    RigidbodyBottom.MovePosition(RigidbodyBottom.position + Time.fixedDeltaTime * speed * direction);

	    // If the position of the toppart is the same direction from the BottomPart as the Input
	    // And if the Angle the character is pointing at is +/- stableAngle
	    if (((isTopRightOfBody() && direction.normalized.Equals(Vector3.right)) || (!isTopRightOfBody() && direction.normalized.Equals(Vector3.left)))
	        && (getAngleOfCharacter() <= stableAngle || getAngleOfCharacter() >= 360-stableAngle))
	    {
		    // Take out the force of the Gravity
		    RigidbodyTop.velocity = new Vector3(RigidbodyTop.velocity.x,  0);
	    }


	    //Debug.Log(directionalJetVector);
	    if (_playerInputProvider.ForceFromJet() > 0)
	    {
		    if (isJetActive)
		    {
			    RigidbodyTop.AddForce(directionalJetVector * Time.fixedDeltaTime * forceJet, ForceMode.Impulse);

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

    void ChangeTopTexture()
    {
		Debug.Log(TopSkinnedMeshRenderer.materials.Length);
	    if (getAngleOfCharacter() <= stableAngle || getAngleOfCharacter() >= 360 - stableAngle)
	    {
		    Debug.Log("stable");
		    TopSkinnedMeshRenderer.materials = new []{robotHeadNeutral,robotHeadNeutral,robotHeadNeutral};
	    }else if (getAngleOfCharacter() > stableAngle && getAngleOfCharacter() <= 180)
	    {
		    Debug.Log("left");
		    TopSkinnedMeshRenderer.materials = new []{robotHeadLeft,robotHeadLeft,robotHeadLeft};
	    }else if (getAngleOfCharacter() < 360 - stableAngle && getAngleOfCharacter() >= 180)
	    {
		    Debug.Log("right");
		    TopSkinnedMeshRenderer.materials = new []{robotHeadRight,robotHeadRight,robotHeadRight};
	    }
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
	    euler = new Vector3(euler.x, euler.y, angle);
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
