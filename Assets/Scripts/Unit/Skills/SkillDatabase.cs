using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SkillDatabase : MonoBehaviour
{
    //SINGLETON
    
    public static SkillDatabase Instance;

    private Dictionary<string, 
        (Sprite highlight, Sprite unHighlight)> skillSprites = new Dictionary<string, (Sprite highlight, Sprite unHighlight)>();

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

        Sprite[] highlightSkill = Resources.LoadAll<Sprite>("Skills/Skills (Highlighted)");
        Sprite[] unhighlightSkill = Resources.LoadAll<Sprite>("Skills/Skills (UnHighlighted)");

        for (int i = 0; i < highlightSkill.Length; i++) {
            this.skillSprites[highlightSkill[i].name] = (highlightSkill[i], unhighlightSkill[i]);
        }
    }

    private Dictionary<string, Skill> skillDatabase = new Dictionary<string, Skill>();
    public Dictionary<string, Skill> SKILLDATABASE { get { return skillDatabase; } }

    // Start is called before the first frame update
    void Start() 
    {
        this.skillDatabase = new Dictionary<string, Skill>();

        Skill basic = new BasicAttack();
        Skill shove = new Shove();
        Skill heal = new Photosynthesis();
        Skill circularCut = new CircularCut();
        Skill rotten = new Rotten();
        Skill daze = new Daze();
        Skill foilThrow = new FoilThrow();
        Skill harvest = new Harvest();
        Skill yell = new Yell();
        Skill popCorn = new PopCorn();

        this.skillDatabase.Add(basic.SkillName, basic);//
        this.skillDatabase.Add(circularCut.SkillName, circularCut);//
        this.skillDatabase.Add(daze.SkillName, daze);//
        this.skillDatabase.Add(foilThrow.SkillName, foilThrow); // 
        this.skillDatabase.Add(harvest.SkillName, harvest);//
        this.skillDatabase.Add(heal.SkillName, heal);//
        this.skillDatabase.Add(shove.SkillName, shove);//
        this.skillDatabase.Add(rotten.SkillName, rotten);
        this.skillDatabase.Add(yell.SkillName, yell);//
        this.skillDatabase.Add(popCorn.SkillName, popCorn);
       
    }
    public void GetSkillSprite(Skill skill) {

        if (this.skillSprites.ContainsKey(skill.SkillName)) {
            Debug.Log(skill.SkillName);
            skill.highlightedIcon = this.skillSprites[skill.SkillName].highlight;
            skill.unHighlightIcon = this.skillSprites[skill.SkillName].unHighlight;
        }
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
