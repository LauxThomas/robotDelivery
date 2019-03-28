using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePickUp : MonoBehaviour
{

	[SerializeField] private int score;
	[SerializeField] private UseRuntimeScore scoreController;

	private void OnTriggerEnter(Collider other)
	{
		scoreController.AddScore(score);
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
