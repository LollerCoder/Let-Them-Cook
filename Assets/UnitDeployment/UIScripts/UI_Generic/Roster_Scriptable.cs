using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Roster_file", menuName = "Roster_fileArchive")]

public class Roster_Scriptable : ScriptableObject
{
    public string name;
    [TextArea(3, 15)]
    public string skillName;
    [TextArea(3, 15)]
    public string skillDescription;
    public Sprite sprite;

}
