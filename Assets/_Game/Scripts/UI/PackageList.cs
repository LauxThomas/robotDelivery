using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Package", menuName = "PackageList")]
public class PackageList : ScriptableObject
{
    public Package[] loadedPackages;
}
