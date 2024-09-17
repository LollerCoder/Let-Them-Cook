using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpManager : MonoBehaviour
{
    [SerializeField]
    public GameObject popUpPrefab;

    [SerializeField]

    public GameObject healthBarPrefab;

    private List<GameObject> hpbarList = new List<GameObject>();

   
    public static PopUpManager Instance { get; private set; }

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

    public void addPopUp(string text, Transform from)
    {
        
        
       
        GameObject popUp = Instantiate(popUpPrefab,from.position,Quaternion.identity);

        popUp.GetComponent<TextMesh>().text = text;

        StartCoroutine(popUpDestroyer(popUp));

        
    }

    IEnumerator popUpDestroyer(GameObject popUpp)
    {
        while (popUpp.transform.position.y - this.transform.position.y < 23)
        {
            popUpp.transform.Translate(new Vector3(0,1,0) * Time.deltaTime);
           

            yield return null;
        }
        Destroy(popUpp);
        
        yield return null;

    }

    public void addpopUpHealth( int UnitMaxHP, int UnitHP, Transform from)
    {
        GameObject popUpHp = Instantiate(healthBarPrefab, from.position, Quaternion.identity);

        

        popUpHp.GetComponentInChildren<Slider>().maxValue = UnitMaxHP;
        popUpHp.GetComponentInChildren<Slider>().value = UnitHP;

        this.hpbarList.Add(popUpHp);
        
    }

    public void popUpDestroyHealth()
    {
        
        if(hpbarList != null)
        {
            if (hpbarList.Count > 0)
            {
                for (int i = 0; i < hpbarList.Count; i++)
                {
                   
                    Destroy(hpbarList[i]);
                }
            }
        }
        hpbarList.Clear();
    }

    
}
