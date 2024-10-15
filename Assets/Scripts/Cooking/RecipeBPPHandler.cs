using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handler script for the recipe book popup
public class RecipeBPPHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject holder;

    public void ToggleDisplay(bool isDisplayed)
    {
        this.holder.SetActive(isDisplayed);
    }
    // Start is called before the first frame update
    void Start()
    {
        this.ToggleDisplay(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
