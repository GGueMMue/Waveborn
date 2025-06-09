using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UIManager;

public class RGBColor : MonoBehaviour
{
    float rgbTimer = 0;
    float rgbSpeed = 1f;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();    
    }

    // Update is called once per frame
    void Update()
    {

        rgbTimer = Mathf.Repeat(Time.time * rgbSpeed, 1f);
        Color color = Color.HSVToRGB(rgbTimer, 1f, 1f);
        image.color = color;
    }
}
