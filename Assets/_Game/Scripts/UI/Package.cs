using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Package : MonoBehaviour
{
    
    [SerializeField]
    private string name;
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private int scoreValue;
    [SerializeField]
    private int slotsNeeded;
    [SerializeField]
    private string description;

    public Package(string name, Sprite image, int scoreValue, int slotsNeeded, string description){
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
    }
}
