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
    private TextMeshProUGUI packageName;
    [SerializeField]
    private Package[] package;
    // Start is called before the first frame update
    void Start()
    {
        Package p1 = new Package("Paket1", GameObject.Find("Sprite1").GetComponent<Sprite>(),10000,1, "Ich bin ein Paket");
    }

    public void onClickNext(){
        description.SetText("Hallo");
    }
}
