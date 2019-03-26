using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Package : MonoBehaviour
{
    
    [SerializeField]
    private Sprite image;
    [SerializeField]
    private int scoreValue;
    [SerializeField]
    private int slotsNeeded;
    [SerializeField]
    private string description;

    public Package(Sprite image, int scoreValue, int slotsNeeded, string description){
        this.image = image;
        this.scoreValue = scoreValue;
        this.slotsNeeded = slotsNeeded;
        this.description = description;
    }
    
    public int GetScoreValue(){
        return scoreValue;
    }

    public int GetSlotsNeeded(){
        return slotsNeeded;
    }
}
