using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerSoundController))]
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
	[SerializeField] private AnimationCurve difficultyCurve;
	[SerializeField] private float jetPower;


	[SerializeField] private float timeInvulnerableToPackageLoss;
	private float timeSinceLastPackageDropped = 0;

	// For The Height of the PlayerModel
	private int height = 1;
	private float totalHeight = 1;

	[SerializeField] private float speed = 1;
	[SerializeField] private float maxSpeed = 1;
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

	public int collectedJumpPower = 0;
	public float currentScoreDifficulty = 0;

	private ArrayList packageList = new ArrayList();

	private ArrayList packageObjectList = new ArrayList();

	private Vector3 directionalJetVector = Vector3.zero;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public SphereCollider ColliderBottom { get; private set; }
	public Transform PlayerTransform { get; private set; }
	public SkinnedMeshRenderer TopSkinnedMeshRenderer { get; private set; }

	public ParticleSystem ThrusterSystemLeft { get; private set; }
	public ParticleSystem ThrusterSystemRight { get; private set; }

	private IInputProvider _playerInputProvider;
	private JumpSpringFX _playerSpringScript;

	private PlayerSoundController SoundCtrl;

	public WheelRotation WheelRotator;


	private void Awake()
	{
		SoundCtrl = GetComponent<PlayerSoundController>();

		_playerInputProvider = GetComponent<IInputProvider>();
		_playerSpringScript = gameObjectSpringPart.GetComponent<JumpSpringFX>();

		RigidbodyTop = gameObjectTop.GetComponent<Rigidbody>();
		ColliderTop = gameObjectTop.GetComponent<Collider>();

		ColliderBottom = gameObjectBottom.GetComponent<SphereCollider>();
		RigidbodyBottom = gameObjectBottom.GetComponent<Rigidbody>();

		PlayerTransform = gameObjectPlayer.GetComponent<Transform>();
		TopSkinnedMeshRenderer = gameObjectTopModel.GetComponent<SkinnedMeshRenderer>();
		ThrusterSystemLeft = gameObjectThrusterLeft.GetComponent<ParticleSystem>();
		ThrusterSystemRight = gameObjectThrusterRight.GetComponent<ParticleSystem>();

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
		// if(isGrounded) RigidbodyBottom.MovePosition(RigidbodyBottom.position + Time.fixedDeltaTime * speed * direction);
		if (isGrounded) RigidbodyBottom.AddForce(Time.fixedDeltaTime * speed * direction, ForceMode.Impulse);
		if (Mathf.Abs(RigidbodyBottom.velocity.x) >= maxSpeed)
		{
			RigidbodyBottom.velocity = RigidbodyBottom.velocity.normalized * maxSpeed;
		}

		if (isGrounded)
		{
			WheelRotator.SetTurnRate(RigidbodyBottom.velocity.x / maxSpeed);
		}
		else
		{
			WheelRotator.SetTurnRate(0.0F);
		}

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

			    Vector3 bottomPosition = RigidbodyBottom.position;
			    Vector3 vectorBetweenRigids = RigidbodyTop.position - bottomPosition;

			    directionalJetVector = new Vector3(vectorBetweenRigids.y*(pushingJetToLeft?-1:1), vectorBetweenRigids.x*(pushingJetToLeft?1:-1));
			    RigidbodyTop.AddForce(directionalJetVector * Time.fixedDeltaTime * jetPower, ForceMode.Impulse);
		    }
		    else
		    {
			    pushingJetToLeft = isTopRightOfBody();

				SoundCtrl.SetThruster(true);


			    isJetActive = true;
			    if (RigidbodyTop.velocity.y < 0f && ((RigidbodyTop.velocity.x > 0f && pushingJetToLeft) || (RigidbodyTop.velocity.x < 0f && !pushingJetToLeft)))
				    RigidbodyTop.velocity = new Vector3(RigidbodyTop.velocity.x, 0, RigidbodyTop.velocity.z);
			    (isTopRightOfBody() ? gameObjectThrusterRight : gameObjectThrusterLeft).SetActive(true);
		    }
	    }
	    else
	    {
		    isJetActive = false;
				SoundCtrl.SetThruster(false);
			directionalJetVector = Vector3.zero;
		    gameObjectThrusterRight.SetActive(false);
		    gameObjectThrusterLeft.SetActive(false);
	    }


	    // Jumping Controll

	    if (_playerInputProvider.JumpPower() > 0)
	    {
		    isJumping = true;
		    if(collectedJumpPower < maxjumpMultiplier) collectedJumpPower += 1;
		    _playerSpringScript.ApplyTension(collectedJumpPower/maxjumpMultiplier);
				SoundCtrl.BeginJumpCharge();
	    }
	    else if (isJumping && isGrounded)
		{
			Vector3 vectorBetweenRigids = RigidbodyTop.position - RigidbodyBottom.position;
			RigidbodyBottom.AddForce(vectorBetweenRigids.normalized * jumpPower * Time.fixedDeltaTime * collectedJumpPower, ForceMode.Impulse);
			RigidbodyTop.AddForce(vectorBetweenRigids.normalized * jumpPower * Time.fixedDeltaTime * collectedJumpPower, ForceMode.Impulse);
			isJumping = false;
			collectedJumpPower = 0;
			_playerSpringScript.ApplyTension(0);
			SoundCtrl.ReleaseJump();
		}
	    else if(isJumping)
	    {
		    isJumping = false;
		    collectedJumpPower = 0;
		    _playerSpringScript.ApplyTension(0);
		    SoundCtrl.ReleaseJump();
	    }

	    if (_playerInputProvider.ResetButton() > 0)
	    {
		    runtimeScore.score = 0;
		    SceneManager.LoadScene(SceneManager.GetActiveScene().name);

	    }
	}


	void CalculatePlayerPosition()
    {
	    Vector3 bottomPosition = RigidbodyBottom.position;
	    Vector3 vectorBetweenRigids = RigidbodyTop.position - bottomPosition;
	    Vector3 newTopPosition = bottomPosition + vectorBetweenRigids.normalized * totalHeight;

	    PlayerTransform.position = new Vector3(bottomPosition.x, bottomPosition.y, bottomPosition.z);
	    RigidbodyTop.MovePosition(newTopPosition);
	    // RigidbodyTop.velocity = Vector3.zero;

	    float angleRadian = Mathf.Atan2(vectorBetweenRigids.y, vectorBetweenRigids.x);
	    TurnPlayerToAngle(RadianToDegree(angleRadian));
	    //ThrusterSystemLeft.startRotation3D = new Vector3(0,0,90 - getAngleOfCharacter());
	    //ThrusterSystemRight.startRotation3D = new Vector3(0,0,90 + getAngleOfCharacter());
    }

    void ProcessGravity()
    {
		RigidbodyBottom.AddForce(Vector3.down * Time.fixedDeltaTime * gravityForceBottom);
		if (getAngleOfCharacter() < 360f && getAngleOfCharacter() > 0f)
		{
			RigidbodyTop.AddForce(Vector3.down * Time.fixedDeltaTime * gravityForceTop * difficultyCurve.Evaluate(currentScoreDifficulty));
		}
    }

    void ProcessCharacterAngle()
    {
	    float currentAngle = getAngleOfCharacter();

	    if (currentAngle <= stableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)) || currentAngle >= 360 - stableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)))
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadNeutral,robotHeadNeutral,robotHeadNeutral};
	    }else if (currentAngle > stableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)) && currentAngle < unstableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)))
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadLeft,robotHeadLeft,robotHeadLeft};
	    }else if (currentAngle < 360 - stableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)) && currentAngle > 360 - unstableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)))
	    {
		    TopSkinnedMeshRenderer.materials = new []{robotHeadRight,robotHeadRight,robotHeadRight};
	    }else if (currentAngle >= unstableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)) || currentAngle <= 360 - stableAngle*(1-difficultyCurve.Evaluate(currentScoreDifficulty)))
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
	    currentScoreDifficulty = 0;
	    foreach (Package package in packageObject.loadedPackages)
	    {
		    if (package != null)
		    {
			    packageList.Add(package);
			    runtimeScore.AddScore(package.scoreValue);
			    currentScoreDifficulty += package.scoreValue / 1000000f;
		    }
	    }
	    setHeight(/*(packageList.Count>0?packageList.Count:1)*/packageList.Count);

	    for (int i = 0; i<packageList.Count; i++)
	    {

		    GameObject packageObjectInstantiate = Instantiate(((Package) packageList[i]).packageMesh, Vector3.zero, new Quaternion());
		    packageObjectInstantiate.transform.parent = gameObjectPlayer.transform;
		    packageObjectInstantiate.transform.eulerAngles = new Vector3(-90f, 90f, 0);
			packageObjectInstantiate.transform.position = gameObjectPlayer.transform.position + new Vector3(0, 1f + packageHeight * (0.5f + i) ,0);

			packageObjectList.Add(packageObjectInstantiate);
	    }

    }

    // Calculating the Height of the Player to keep the Rigidbodys close together
    private void setHeight(int newHeight)
    {
	    height = (newHeight!=0?newHeight:1);
	    totalHeight = 1f + packageHeight * height;

	    gameObjectUpperPart.transform.localScale = new Vector3(2.75f,0.3f * height * packageHeight,2.75f);
	    gameObjectHeadPart.transform.localScale = new Vector3(1f/2.75f, 1 / (0.3f * height * packageHeight), 1f/2.75f);

	    Vector3 positionLeft = gameObjectThrusterLeft.transform.localPosition;
	    Vector3 positionRight = gameObjectThrusterRight.transform.localPosition;
    }

    public GameObject popPackage()
    {
	    if (packageList.Count > 0)
	    {
		    Package temp = (Package) packageList[packageList.Count - 1];
		    runtimeScore.SubScore(temp.scoreValue);
		    currentScoreDifficulty -= temp.scoreValue / 1000000f;
		    packageList.Remove(temp);

				SoundCtrl.LosePackage();
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
