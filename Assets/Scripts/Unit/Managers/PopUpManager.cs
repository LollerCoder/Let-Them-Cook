using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PopUpManager : MonoBehaviour
{
    [SerializeField]
    public GameObject popUpPrefab;

    [SerializeField]

    public GameObject healthBarPrefab;

    

   
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
            Destroy(this);
        } 
    }

    public void addPopUp(string text, Transform from)
    {
        
        
       
        GameObject popUp = Instantiate(popUpPrefab,from.position,Quaternion.identity);

        popUp.GetComponent<TextMeshPro>().text = text;

        //StartCoroutine(popUpDestroyer(popUp));

        
    }

    //IEnumerator popUpDestroyer(GameObject popUpp)
    //{
    //    float time = 1;
    //    float counter = 0;
    //    float interp = 0;

     
    //    while (counter < time)
    //    {
    //        counter += Time.deltaTime;
            

    //        if(interp <= 1.0f) popUpp.transform.Translate(new Vector3(0, Mathf.Lerp(0, 80, interp),0));
    //        interp += 0.1f * Time.deltaTime /1000;

            
    //        yield return null;
    //    }
    //    Destroy(popUpp);
     
    //    yield return null;

    //}

    //IEnumerator popUpHpDestroyer(GameObject popUpp)
    //{
    //    float time = 3;
    //    float counter = 0;
    //    while (counter < time)
    //    {
    //        counter += Time.deltaTime;
            
            


    //        yield return null;
    //    }
    //    Destroy(popUpp);
    //    Debug.Log("HP dead");
    //    yield return null;

    //}

    


    //private void Update()
    //{

    //    float time = Time.deltaTime;
    //}


    //Old popupHP Implementation keep just in case

    //public void addpopUpHealth( int UnitMaxHP, int UnitHP, Transform from)
    //{
    //    GameObject popUpHp = Instantiate(healthBarPrefab, from.position, Quaternion.identity);



    //    popUpHp.transform.Translate(new Vector3(0, 5, 0));

    //    Debug.Log("HP BAR POPPED");

    //    popUpHp.GetComponentInChildren<Slider>().maxValue = UnitMaxHP;
    //    popUpHp.GetComponentInChildren<Slider>().value = UnitHP;

    //    StartCoroutine(popUpHpDestroyer(popUpHp));

    //    //this.hpbarList.Add(popUpHp);

    //}

    //public void popUpDestroyHealth()
    //{

    //    if(hpbarList != null)
    //    {
    //        if (hpbarList.Count > 0)
    //        {
    //            for (int i = 0; i < hpbarList.Count; i++)
    //            {

    //                Destroy(hpbarList[i]);
    //            }
    //        }
    //    }
    //    hpbarList.Clear();
    //}


}
