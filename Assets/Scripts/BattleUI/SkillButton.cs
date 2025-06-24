using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SkillButton : MonoBehaviour {
    [SerializeField]
    private Button button;
    public Button Button { get { return this.button; } }

    [SerializeField]
    private Image image;
    public Image Image { get { return this.image; } }

    [SerializeField]
    private Text text;
    public Text Text { get { return this.text; } }
}
