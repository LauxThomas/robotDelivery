using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{

		Debug.Log("Ende");

		SceneManager.LoadScene("TutorialLevelScene");		//Muss noch auf richtige Szene zeigen

	}
}
