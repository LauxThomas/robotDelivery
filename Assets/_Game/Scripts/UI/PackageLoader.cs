using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEditor;


public class PackageLoader : MonoBehaviour
{
    
    [SerializeField]
    private Image packageImage;
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button previous;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private Package[] package;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onClickNext(){
        description.SetText("Hallo");
    }
}
