using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Dialogue {
    public string[] names;

    public Sprite[] sprites;

    [TextArea(3, 10)]
    public string[] sentences;
}
