using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
	public float score;
	public float slowingFactor=5;

	//public Transform scoreText;

	// Start is called before the first frame update
	void Start()
	{
		updateScoreText();
	}

	// Update is called once per frame
	void Update()
	{
		score += Time.deltaTime*1/slowingFactor;
		updateScoreText();
	}

	private void updateScoreText()
	{
		gameObject.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString("F2");
		//scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + (int) score;
	}

	public void addScore(int val)
	{
		score += val;
		updateScoreText();
	}
}
