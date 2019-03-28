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
 
   
   /* public Package(string name, Sprite image, int scoreValue, int slotsNeeded, string description){
        this.name = name;
        this.image = image;
        this.scoreValue = scoreValue;
        this.slotsNeeded = slotsNeeded;
        this.description = description;
    }
    
    public string GetName(){
        return name;
    }

    public string GetDescription(){
        return description;
    }
    
    public int GetScoreValue(){
        return scoreValue;
    }

    public int GetSlotsNeeded(){
        return slotsNeeded;
    } */
}
