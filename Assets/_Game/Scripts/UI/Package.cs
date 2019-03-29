using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Package", menuName = "Package")]
public class Package : ScriptableObject
{

    public new string name;

     public string description;
   
    public Sprite image;
 
    public int scoreValue;
   
    public int slotsNeeded;

    public GameObject packageMesh;
 
}
