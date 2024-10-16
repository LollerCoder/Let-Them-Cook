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

    public Skill findSkill(string name)
    {
        Skill skillToReturn = null;
        if (skillDatabase.ContainsKey(name))
        {

            skillToReturn = skillDatabase[name];
            
        }

        return skillToReturn;
       
    }
    public void addSkill(EffectInfo effects, string skillName,float cost) //////Mainly for adding tile effects and the like
    {
        if (!skillDatabase.ContainsKey(skillName)) //just add the tile in
        {
            Skill toAdd = new Skill(effects,cost);
            skillDatabase.Add(skillName, toAdd);
           
        }

        else if(skillName == "RandomTile")// you can only be debuff by tiles once, might have to change.
        {
            List<string> toDelete = new List<string>();
            
                foreach (string key in skillDatabase.Keys)
                {

                   if(key == "RandomTile")
                    {
                        toDelete.Add(key);
                    }
                }
            
            foreach (string keyDelete in toDelete)
            {
                skillDatabase.Remove(keyDelete);
            }
            Skill toAdd = new Skill(effects);
            skillDatabase.Add(skillName, toAdd);
        }

        Debug.Log(skillDatabase.Count);
    }
}
