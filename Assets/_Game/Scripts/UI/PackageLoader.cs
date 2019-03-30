using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;


public class PackageLoader : MonoBehaviour
{
    
    [SerializeField]
    private Button next;
    [SerializeField]
    private RuntimeScore nextLevel;
    [SerializeField]
    private Button previous;
    [SerializeField]
    private TextMeshProUGUI description;
    [SerializeField]
    private TextMeshProUGUI packageName;
    [SerializeField]
    private TextMeshProUGUI score;
    [SerializeField]
    private TextMeshProUGUI neededSlots;
    [SerializeField]
    private Package[] package;
    [SerializeField]
    private Image packageImage;
    [SerializeField]
    private Image[] loadedPackages;
    [SerializeField]
    private PackageList finalLoadedList;
    // Start is called before the first frame update
    private List<Package> packageList;
    private List<Package> loadedPackagesList;
    private int currentChoice;
    private int currentPackageSlot;

    void Start()
    {
        currentChoice = 0;
        currentPackageSlot = 0;
        for(int i = 0; i < finalLoadedList.loadedPackages.Length; i++){
            finalLoadedList.loadedPackages[i] = null;
        }

        if(package.Length > 0){
            packageList = new List<Package>();
            loadedPackagesList = new List<Package>();

            foreach(Package p in package){
                packageList.Add(p);
            }

            //Init first Package
           SetPackageUI(currentChoice);
        }
    }

    void SetPackageUI(int index){
        packageName.SetText(packageList[index].name);
        description.SetText(packageList[index].description);
        score.SetText(packageList[index].scoreValue + "");
        neededSlots.SetText(packageList[index].slotsNeeded + "");
        packageImage.sprite = packageList[index].image;
    }

    public void onClickNext(){
        if(package.Length > 0){
            currentChoice += 1;
            currentChoice = currentChoice% (packageList.Count);
            SetPackageUI(currentChoice);
        }
    }

    public void onClickPrevious(){
        if(package.Length > 0){
            if(currentChoice - 1 < 0)
                currentChoice = packageList.Count-1;
            else
            {
                currentChoice -= 1;
            }
            SetPackageUI(currentChoice);
        }
    }

    public void onClickAdd(){
        if(package.Length > 0 && currentPackageSlot < 8){
           
            if(currentPackageSlot + packageList[currentChoice].slotsNeeded <= 8){
                loadedPackages[currentPackageSlot].gameObject.SetActive(true);
                loadedPackages[currentPackageSlot].sprite = packageList[currentChoice].image;
                loadedPackagesList.Add(packageList[currentChoice]);
                currentPackageSlot += packageList[currentChoice].slotsNeeded;
            }
            
        }
    }

    public void onClickDelete(){
         if(package.Length > 0 && currentPackageSlot <= 8 && loadedPackagesList.Count > 0){
             currentPackageSlot -= loadedPackagesList[loadedPackagesList.Count -1].slotsNeeded;
             if(currentPackageSlot < 0)
                currentPackageSlot = 0;
             loadedPackagesList.RemoveAt(loadedPackagesList.Count-1);
             loadedPackages[currentPackageSlot].sprite = null;
             loadedPackages[currentPackageSlot].gameObject.SetActive(false);
         }
    }

    public void onClickClear(){
        foreach(Image i in loadedPackages){
            i.sprite = null;
        }
        currentPackageSlot = 0;
        loadedPackagesList.Clear();
        foreach(Image i in loadedPackages){
            i.gameObject.SetActive(false);
        }
    }

    public void onClickMenu(){
       //Scene wechseln
    }

    public void onClickStart(){
        if(loadedPackagesList.Count > 0 ){
            for(int i = 0; i < loadedPackagesList.Count; i++){
                finalLoadedList.loadedPackages[i] = loadedPackagesList[i];
            }
            
            switch(nextLevel.level){
                case 0 : SceneManager.LoadScene("Blockout");
                        break;
                case 1 : SceneManager.LoadScene("Blockout Level 1");
                        break;
                case 2 : SceneManager.LoadScene("Blockout Level 2");
                        break;
                case 3 : SceneManager.LoadScene("Blockout Level 3");
                        break;
                default: break;

            }
        } else {
            Debug.Log("No packages loaded!!!");
        }
        //To DO:
        //Scene wechseln
    }
}
