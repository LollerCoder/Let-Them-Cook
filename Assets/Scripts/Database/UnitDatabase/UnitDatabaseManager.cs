using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor.SearchService;
using UnityEngine;

public class UnitDatabaseManager : MonoBehaviour
{
    [SerializeField] List<Unit> unitList = new List<Unit>();

    private string filePath = "Assets/Scripts/Database/UnitDatabase/UnitDatabase.txt";
    private StreamWriter writer = null;

    private string[] content = null;
    private System.Random rng = new System.Random();

    private Unit unit = null;
    [SerializeField] Transform allySpawnZone;


    public static UnitDatabaseManager Instance { get; private set; }


    public void addDatabase(string item)
    {
        using (var writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(item);
            writer.Flush();
        }
    }

    public void saveDatabase()
    {
        //for loops of the list of allies
        writer = new StreamWriter(filePath, true);
        /*writer.WriteLine("CARROT");
        writer.Flush();*/
    }

    private void loadDatabase()
    {
        content = File.ReadAllLines(filePath);
        int prefabIndex = -1;

        for(int i = 0; i < content.Length; i++)
        {
            for(int j = 0; j < unitList.Count; j++)
            {
                string strContent = content[i].ToLower();
                string strList = unitList[j].name.ToLower();

                if (strContent == strList)
                {
                    prefabIndex = j;
                }
            }

            int x = UnityEngine.Random.Range(1, 17);
            int z = UnityEngine.Random.Range(1, 17);
            unit = Instantiate(this.unitList[prefabIndex], new Vector3(x, 0.6f, z), Quaternion.identity, this.allySpawnZone.transform);
            unit.transform.rotation = Quaternion.Euler(0, -180, 0);
            unit.Type = EUnitType.Ally;
            unit.gameObject.layer = this.gameObject.layer;
        }
    }

    private void lineChanger(string newText, string fileName, int line_to_edit)
    {
        string[] arrLine = File.ReadAllLines(fileName);
        arrLine[line_to_edit - 1] = newText;
        File.WriteAllLines(fileName, arrLine);
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //loadDatabase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
