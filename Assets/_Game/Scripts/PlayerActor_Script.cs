using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor_Script : MonoBehaviour
{
	[SerializeField] private GameObject _gameObjectTop;
	[SerializeField] private GameObject _gameObjectBottom;
	[SerializeField] private GameObject _gameObjectPlayer;

	[SerializeField] [Range(1,10)] private int height;

	public Rigidbody RigidbodyTop { get; private set; }
	public Rigidbody RigidbodyBottom { get; private set; }
	public Collider ColliderTop { get; private set; }
	public Collider ColliderBottom { get; private set; }


	private IInputProvider _playerInputProvider;

	private void Awake()
	{
		_playerInputProvider = GetComponent<IInputProvider>();
		RigidbodyTop = _gameObjectTop.GetComponent<Rigidbody>();
		ColliderTop = _gameObjectTop.GetComponent<Collider>();
		ColliderBottom = _gameObjectBottom.GetComponent<Collider>();
		RigidbodyBottom = _gameObjectBottom.GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
		processInput();
		processGravity();
		calculatePlayerPosition();
    }

    void processInput()
    {
	    // Debug.Log(_playerInputProvider.Direction());
	    RigidbodyBottom.MovePosition(RigidbodyBottom.position + (Vector3) _playerInputProvider.Direction());
    }

    void calculatePlayerPosition()
    {
	    Vector2 vectorBetweenRigids = RigidbodyTop.position - RigidbodyBottom.position;
    }

    void processGravity()
    {

    }
}
