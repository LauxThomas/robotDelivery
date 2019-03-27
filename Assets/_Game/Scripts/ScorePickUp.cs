using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : MonoBehaviour
{

	[SerializeField] private int _score;
	[SerializeField] private ScoreController _scoreController;

	private void OnTriggerEnter(Collider other)
	{
		_scoreController.addScore(_score);
		Debug.Log("_score");
	}


	// Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
