using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDatabase : MonoBehaviour
{
    //SINGLETON
    public static SkillDatabase Instance;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(this.gameObject);
        }
    }



    private Dictionary<string, Skill> skillDatabase = new Dictionary<string, Skill>();
    public Dictionary<string, Skill> SKILLDATABASE { get { return skillDatabase; } }

    // Start is called before the first frame update
    void Start()
    {
        this.skillDatabase = new Dictionary<string, Skill>();

        //Carrot skills
        Skill basic = new BasicAttack();
        Skill trueStrike = new TrueStrike();
        Skill eyePoke = new EyePoke();
        Skill tripUp = new TripUp();
        Skill eagle = new EagleEye();

        this.skillDatabase.Add(basic.SkillName,basic);
        this.skillDatabase.Add(trueStrike.SkillName, trueStrike);
        this.skillDatabase.Add(eyePoke.SkillName, eyePoke);
        this.skillDatabase.Add(tripUp.SkillName, tripUp);
        this.skillDatabase.Add(eagle.SkillName, eagle);
    }

    // Update is called once per frame
    public void applySkill(string name, Unit target, Unit Origin)
    {
        if (skillDatabase.ContainsKey(name))
        {
           
            skillDatabase[name].SkillAction(target, Origin);
        }
    }
}
