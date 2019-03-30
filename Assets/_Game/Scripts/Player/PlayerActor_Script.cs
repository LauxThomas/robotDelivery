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
	[SerializeField] private GameObject gameObjectUpperPart;
	[SerializeField] private GameObject gameObjectHeadPart;
	[SerializeField] private GameObject gameObjectSpringPart;

	[SerializeField] private GameObject gameObjectThrusterLeft;
	[SerializeField] private GameObject gameObjectThrusterRight;

	[SerializeField] private Material robotHeadNeutral;
	[SerializeField] private Material robotHeadLeft;
	[SerializeField] private Material robotHeadRight;
	[SerializeField] private Material robotHeadFail;

	[SerializeField] private PackageList packageObject;
	[SerializeField] private RuntimeScore runtimeScore;

	[SerializeField] private float timeInvulnerableToPackageLoss;
	private float timeSinceLastPackageDropped = 0;

	// For The Height of the PlayerModel
	private int height = 1;
	private float totalHeight = 1;

	[SerializeField] private float speed = 1;
	[SerializeField] private float forceJet = 1;
	[SerializeField] private float gravityForceTop = 1;
	[SerializeField] private float gravityForceBottom = 1;
	[SerializeField] private float stableAngle = 10;
	[SerializeField] private float unstableAngle = 50;
	[SerializeField] private float packageHeight = 1;
	[SerializeField] private float jumpPower = 1;
	[SerializeField] private float maxjumpMultiplier = 1;

	public bool isGrounded = true;
	private bool pushingJetToLeft = false;
	private bool isJetActive = false;
	private bool isJumping = false;

	private int collectedJumpPower = 0;

	private ArrayList packageList = new ArrayList();

	private ArrayList packageObjectList = new ArrayList();

	private Vector3 directionalJetVector = Vector3.zero;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public SphereCollider ColliderBottom { get; private set; }
	public Transform PlayerTransform { get; private set; }
	public SkinnedMeshRenderer TopSkinnedMeshRenderer { get; private set; }

	private IInputProvider _playerInputProvider;
	private JumpSpringFX _playerSpringScript;

	public AnimationCurve plot = new AnimationCurve();

	private void Awake()
	{
		_playerInputProvider = GetComponent<IInputProvider>();
		_playerSpringScript = gameObjectSpringPart.GetComponent<JumpSpringFX>();

		RigidbodyTop = gameObjectTop.GetComponent<Rigidbody>();
		ColliderTop = gameObjectTop.GetComponent<Collider>();

		ColliderBottom = gameObjectBottom.GetComponent<SphereCollider>();
		RigidbodyBottom = gameObjectBottom.GetComponent<Rigidbody>();

		PlayerTransform = gameObjectPlayer.GetComponent<Transform>();
		TopSkinnedMeshRenderer = gameObjectTopModel.GetComponent<SkinnedMeshRenderer>();

	}

	private void Start()
	{
		setPackagesFromScriptableObjects();
	}


	private void Update()
	{

	}

	void FixedUpdate()
	{
		ProcessInput();
		ProcessGravity();
		CalculatePlayerPosition();
		ProcessCharacterAngle();
	}


	/**
	 *
	 */
	void ProcessInput()
	{
		Vector3 direction = _playerInputProvider.Direction();

	    // Takes input to change the rigidbody of the wheel and moves it around
	    if(isGrounded) RigidbodyBottom.MovePosition(RigidbodyBottom.position + Time.fixedDeltaTime * speed * direction);

	    // If the position of the toppart is the same direction from the BottomPart as the Input
	    // And if the Angle the character is pointing at is +/- stableAngle
	    if (((isTopRightOfBody() && direction.normalized.Equals(Vector3.right)) || (!isTopRightOfBody() && direction.normalized.Equals(Vector3.left)))
	        && (getAngleOfCharacter() <= stableAngle || getAngleOfCharacter() >= 360-stableAngle))
	    {
		    // Take out the force of the Gravity
		    RigidbodyTop.velocity = new Vector3(RigidbodyTop.velocity.x,  0);
	    }


	    // Jet Controll
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
			    if (RigidbodyTop.velocity.y < 0f && ((RigidbodyTop.velocity.x > 0f && pushingJetToLeft) || (RigidbodyTop.velocity.x < 0f && !pushingJetToLeft)))
				    RigidbodyTop.velocity = new Vector3(RigidbodyTop.velocity.x, 0, RigidbodyTop.velocity.z);
			    (isTopRightOfBody() ? gameObjectThrusterRight : gameObjectThrusterLeft).SetActive(true);
		    }
	    }
	    else
	    {
		    isJetActive = false;
		    directionalJetVector = Vector3.zero;
		    gameObjectThrusterRight.SetActive(false);
		    gameObjectThrusterLeft.SetActive(false);
	    }


	    // Jumping Controll

	    if (_playerInputProvider.JumpPower() > 0 && isGrounded)
	    {
		    isJumping = true;
		    if(collectedJumpPower < maxjumpMultiplier) collectedJumpPower += 1;
		    _playerSpringScript.ApplyTension(collectedJumpPower/maxjumpMultiplier);

	    }
	    else if (isJumping && isGrounded)
		{
			RigidbodyBottom.AddForce(Vector3.up * jumpPower * Time.fixedDeltaTime * collectedJumpPower, ForceMode.Impulse);
			RigidbodyTop.AddForce(Vector3.up * jumpPower * Time.fixedDeltaTime * collectedJumpPower, ForceMode.Impulse);
			isJumping = false;
			collectedJumpPower = 0;
			_playerSpringScript.ApplyTension(0);
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
		RigidbodyBottom.AddForce(Vector3.down * Time.fixedDeltaTime * gravityForceBottom);
		if (getAngleOfCharacter() < 360f && getAngleOfCharacter() > 0f)
		{
			RigidbodyTop.AddForce(Vector3.down * Time.fixedDeltaTime * gravityForceTop);
		}
    }

    void ProcessCharacterAngle()
    {
	    if (getAngleOfCharacter() <= stableAngle || getAngleOfCharacter() >= 360 - stableAngle)
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadNeutral,robotHeadNeutral,robotHeadNeutral};
	    }else if (getAngleOfCharacter() > stableAngle && getAngleOfCharacter() < unstableAngle)
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadLeft,robotHeadLeft,robotHeadLeft};
	    }else if (getAngleOfCharacter() < 360 - stableAngle && getAngleOfCharacter() > 360 - unstableAngle)
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadRight,robotHeadRight,robotHeadRight};
	    }else if (getAngleOfCharacter() >= unstableAngle || getAngleOfCharacter() <= 360 - stableAngle)
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadFail,robotHeadFail,robotHeadFail};
		    if (Time.time - timeSinceLastPackageDropped >= timeInvulnerableToPackageLoss)
		    {
			    GameObject lostPackage = popPackage();
			    timeSinceLastPackageDropped = Time.time;
			    if (lostPackage != null) lostPackage.GetComponent<Rigidbody>().isKinematic = false;
			    setHeight(height-1);
		    }

	    }
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


    public void setPackagesFromScriptableObjects()
    {
	    packageList.Clear();
	    foreach (Package package in packageObject.loadedPackages)
	    {
		    if (package != null)
		    {
			    packageList.Add(package);

		    }
	    }
	    setHeight((packageList.Count>0?packageList.Count:1));

	    for (int i = 0; i<packageList.Count; i++)
	    {
		    GameObject packageObjectInstantiate = Instantiate(((Package) packageList[i]).packageMesh, Vector3.zero, new Quaternion());
		    packageObjectInstantiate.transform.parent = gameObjectPlayer.transform;
		    packageObjectInstantiate.transform.eulerAngles = new Vector3(-90f, 90f, 0);
			packageObjectInstantiate.transform.position = gameObjectPlayer.transform.position + new Vector3(0, 1.3f + packageHeight * (0.5f + i) ,0);

			packageObjectList.Add(packageObjectInstantiate);
	    }

    }

    // Calculating the Height of the Player to keep the Rigidbodys close together
    private void setHeight(int newHeight)
    {
	    height = (newHeight!=0?newHeight:1);
	    totalHeight = 1.3f + packageHeight * height;
	    gameObjectUpperPart.transform.localScale = new Vector3(2.75f,0.3f * height * packageHeight,2.75f);
	    gameObjectHeadPart.transform.localScale = new Vector3(1f/2.75f, 1 / (0.3f * height * packageHeight), 1f/2.75f);

	    Vector3 positionLeft = gameObjectThrusterLeft.transform.localPosition;
	    Vector3 positionRight = gameObjectThrusterRight.transform.localPosition;
	    gameObjectThrusterLeft.transform.localPosition = new Vector3(positionLeft.x, 0.7f + totalHeight , positionLeft.z);
	    gameObjectThrusterRight.transform.localPosition = new Vector3(positionRight.x, 0.7f + totalHeight , positionRight.z);

    }

    public GameObject popPackage()
    {
	    if (packageList.Count > 0)
	    {
		    Package temp = (Package) packageList[packageList.Count - 1];
		    packageList.Remove(temp);
	    }

	    GameObject result = null;
	    if (packageObjectList.Count > 0)
	    {
		    result = (GameObject) packageObjectList[packageObjectList.Count - 1];
		    packageObjectList.Remove(result);
		    result.transform.parent = null;
	    }
	    return result;
    }
}
