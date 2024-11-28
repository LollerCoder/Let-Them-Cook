using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    //Broadcasting
    public const string UNIT = "UNIT";

    
    
    
    // Start is called before the first frame update
   

    public void hpPopUp(GameObject popUpp, int maxHp, int hp)
    {
        popUpp.SetActive(true);
        Slider hpSlide = popUpp.transform.Find("Slider").GetComponent<Slider>();
        Slider easeSlide = popUpp.transform.Find("EaseSlider").GetComponent<Slider>();

        hpSlide.value = hp;


        if (easeSlide.value != hpSlide.value)
        {
            
            StartCoroutine(easer(popUpp));
        }


    }

    public void hpHide(GameObject popUp)    
    {
        popUp.SetActive(false);
    }

    IEnumerator easer(GameObject popUpp)
    {

        Slider hpSlide = popUpp.transform.Find("Slider").GetComponent<Slider>();
        
        Slider easeSlide = popUpp.transform.Find("EaseSlider").GetComponent<Slider>();
        float interpSpeed = 0.018f;

       
        while (easeSlide.value > hpSlide.value)
        {
            //Debug.Log(easeSlide.value);
            easeSlide.value = Mathf.Lerp( easeSlide.value, hpSlide.value, interpSpeed);
            if(easeSlide.value - 0.5  < hpSlide.value)
            {
                easeSlide.value = Mathf.FloorToInt(easeSlide.value) / 1;
            }
            
            yield return null;
        }
      
        easeSlide.value = Mathf.CeilToInt(easeSlide.value)/1;
        //Debug.Log("Final: " + easeSlide.value);
        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
    }

}
