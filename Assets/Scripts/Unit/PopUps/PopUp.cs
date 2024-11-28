using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    // Start is called before the first frame update

    private const float DISAPPEAR_TIMER_MAX = 1f;

    private TextMeshPro textMesh;
    private Color textCol;
    private float dissapearTimer = 0f;
    private void Awake()
    {
        this.textMesh = transform.GetComponent<TextMeshPro>();
        this.textCol = textMesh.color;
        this.dissapearTimer = DISAPPEAR_TIMER_MAX;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 0.4f;

        transform.position += new Vector3(0, moveYSpeed, 0) * Time.deltaTime;

        

        if(dissapearTimer > DISAPPEAR_TIMER_MAX * 0.5f)
        {
            float scaleINC = 1.0f;
            transform.localScale += Vector3.one * scaleINC * Time.deltaTime;
        }
        else
        {
            float scaleINC = 1.0f;
            transform.localScale -= Vector3.one * scaleINC * Time.deltaTime;
        }

        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer <= 0f)
        {
            float dissapearSpeed = 3f;
            textCol.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textCol;

            if (textCol.a <= 0) Destroy(gameObject);
            

        }
    }
}
