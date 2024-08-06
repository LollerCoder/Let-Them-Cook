using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteImage_Manual : MonoBehaviour
{
    [SerializeField] Sprite newSprite;
    public void ChangeSprite()
    {
        var image = GetComponent<Image>();
        image.sprite = newSprite;
    }

}
