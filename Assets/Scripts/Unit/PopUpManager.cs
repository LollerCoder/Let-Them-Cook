using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpManager : MonoBehaviour
{
    [SerializeField]
    public GameObject popUpPrefab;

   
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
            Debug.Log("Distance: " + (popUpp.transform.position.y - this.transform.position.y));

            yield return null;
        }
        Destroy(popUpp);
        Debug.Log("Should Destroy");
        yield return null;

    }

    
}
