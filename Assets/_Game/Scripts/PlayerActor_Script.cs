using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor_Script : MonoBehaviour
{
	[SerializeField] private GameObject _gameObjectTop;
	[SerializeField] private GameObject _gameObjectBottom;
	[SerializeField] private GameObject _gameObjectPlayer;

	public Rigidbody _rigidbodyTop { get; private set; }
	public Rigidbody _rigidbodyBottom { get; private set; }

	private void Awake()
	{
		_rigidbodyTop = _gameObjectTop.GetComponent<Rigidbody>();
		_rigidbodyBottom = _gameObjectBottom.GetComponent<Rigidbody>();
	}

	// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
