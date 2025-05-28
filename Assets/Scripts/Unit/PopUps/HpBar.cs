using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class HpBar : MonoBehaviour
{
    //Broadcasting
    public const string UNIT = "UNIT";


 
    


    // Start is called before the first frame update
   

    public void setColor(EUnitType unitType, bool isItYou)
    {

        if (unitType == EUnitType.Enemy)//enemy
        {
            //Debug.Log("Red applied");   
            this.gameObject.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.8941177f, 0, 0.05098039f, 1);

        }

        if (unitType == EUnitType.Ally)//ally
        {
            //Debug.Log("Blue applied");

            this.gameObject.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.0619223f, 0.2870282f, 0.8415094f, 1);
        }

        if (isItYou)//its you
        {
            //Debug.Log("Green applied");

            this.gameObject.transform.Find("Slider").GetComponentInChildren<Image>().color = new Color(0.2638531f, 0.8943396f, 0.2008044f, 1);
        }
    }


    public void hpPopUp(GameObject popUpp, int maxHp, int hp)
    {
        Debug.Log(hp + "/" + maxHp);
        popUpp.SetActive(true);
        
        Slider hpSlide = popUpp.transform.Find("Slider").GetComponent<Slider>();
        Slider easeSlide = popUpp.transform.Find("EaseSlider").GetComponent<Slider>();
        hpSlide.enabled = true;
        easeSlide.enabled = true;

        hpSlide.value = hp;
        hpSlide.maxValue = maxHp;
        easeSlide.maxValue = maxHp;



        //Debug.Log(easeSlide.value + " vs " + hpSlide.value);
        if (easeSlide.value != hpSlide.value)
        {
            
            if (popUpp.activeSelf)
            {
                StartCoroutine(easer(popUpp));
            }
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
        //Debug.Log("Easing Final: " + easeSlide.value);
        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
    }

    private void Start()
    {
     
    }

}
