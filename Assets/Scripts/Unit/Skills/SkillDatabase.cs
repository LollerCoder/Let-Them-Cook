using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
        Skill shove = new Shove();
        Skill heal = new Photosynthesis();
        Skill foilStackin = new FoilStackin();
        Skill defensiveWhack = new DefensiveWhack();
        Skill circularCut = new CircularCut();

        this.skillDatabase.Add(basic.SkillName,basic);
        this.skillDatabase.Add(trueStrike.SkillName, trueStrike);
        this.skillDatabase.Add(eyePoke.SkillName, eyePoke);
        this.skillDatabase.Add(tripUp.SkillName, tripUp);
        this.skillDatabase.Add(eagle.SkillName, eagle);
        this.skillDatabase.Add(shove.SkillName, shove);
        this.skillDatabase.Add(heal.SkillName,heal);
        this.skillDatabase.Add(foilStackin.SkillName,foilStackin);
        this.skillDatabase.Add(defensiveWhack.SkillName,defensiveWhack);
        this.skillDatabase.Add(circularCut.SkillName, circularCut);
    }

    // Update is called once per frame
    public void applySkill(string name, Unit target, Unit Origin)
    {
        if (skillDatabase.ContainsKey(name))
        {
           
            skillDatabase[name].SkillAction(target, Origin);
        }
    }

    public Skill findSkill(string name)
    {
        Skill skillToReturn = null;
        if (skillDatabase.ContainsKey(name))
        {

            skillToReturn = skillDatabase[name];
            
        }

        return skillToReturn;
       
    }
    
}
