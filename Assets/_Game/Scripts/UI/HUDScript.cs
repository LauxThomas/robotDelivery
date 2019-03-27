using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDScript : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	    //TODO:TOGGLEFUNKTIONEN!
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
		Debug.Log(showHud);
	}
}
