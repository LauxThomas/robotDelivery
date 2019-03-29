using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{

	//public ScorePickUp scorePickUp;
	//public ScoreController scoreController;
    // Update is called once per frame
    void Update()
    {
	    if (Time.timeScale>0)
	    {
		    toggleHUD(true);
	    }
	    else
	    {
		    toggleHUD(false);
	    }
    }


	public void toggleHUD(bool showHud)
	{
		gameObject.SetActive(showHud);
		//Debug.Log(showHud);
	}
}
