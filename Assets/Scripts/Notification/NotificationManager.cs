using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour
{
    [SerializeField] private GameObject reference;

    public static NotificationManager Instance { get; private set; }

    public void OnKill(EIngredientType ingredient)
    {
        GameObject notif = Instantiate(reference, this.transform);
        notif.GetComponent<NotifIngredientObtained>().SetUpNotif(ingredient.ToString());


        Destroy(notif, 4.0f);
        GameManager.Instance.OnAddItem(ingredient);
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.OnDiedCallback += OnKill;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
