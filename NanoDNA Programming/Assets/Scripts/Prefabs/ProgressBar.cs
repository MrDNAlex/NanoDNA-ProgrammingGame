using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DNAMathAnimation;

public class ProgressBar : MonoBehaviour
{
    [System.Serializable]
    public struct ColorPercent
    {
        public float percent;
        public Color color;
    }

    public enum ColorChangeFunction
    {
        None, //If percent is more than a color use that color
        Linear,
    }

    public enum FillType
    {
        None, 
        Linear, 
        Sinusoidal, 
        Cosinusoidal
    }


    public Slider slider;

    public float total;
    public float value;

    public FillType fillType;

    // public ColorChangeFunction colorFunc;

    //  public List<ColorPercent> colors;

    // public Gradient gradient;


    Image fillArea;
    //Design a linear color change function?


    // Start is called before the first frame update
    void Start()
    {
        slider = this.GetComponent<Slider>();
        fillArea = slider.fillRect.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void initProgressBar(float total)
    {
        this.total = total;
        slider.maxValue = total;
        this.value = 0;
    }

    public void setValue(float value)
    {
        this.value = value;

        updateProgresBar(value);
    }

    public void setValuePercent(float percent)
    {
        value = total * percent;

        updateProgresBar(value);
    }

    public void updateProgresBar(float value)
    {
        //Update progressBar
        switch (fillType)
        {
            case FillType.None:
                slider.value = value;
                break;
            case FillType.Linear:
                StartCoroutine(DNAMathAnim.animateLinearSliderFill(slider, value, DNAMathAnim.getFrameNumber(1)));
                break;
            case FillType.Sinusoidal:
                StartCoroutine(DNAMathAnim.animateSineSliderFill(slider, value, DNAMathAnim.getFrameNumber(1)));
                break;
            case FillType.Cosinusoidal:
                StartCoroutine(DNAMathAnim.animateCosineSliderFill(slider, value, DNAMathAnim.getFrameNumber(1)));
                break;
        }

        //Update fill color
        setFillColor(value);
    }

    public void setFillColor(float value)
    {
        //Convert to percent
        float percent = ((float)value / total) * 100f;

        /*
        switch (colorFunc)
        {
            case ColorChangeFunction.None:
                foreach (ColorPercent col in colors)
                {
                    if (percent >= col.percent)
                    {
                        fillArea.color = col.color;
                    }
                }
                break;
            case ColorChangeFunction.Linear:

                  Debug.Log("Hello");

                //   Color b = colors[0].color;

                //  Color slope = new Color((colors[1].color.r - colors[0].color.r) / 100f, (colors[1].color.g - colors[0].color.g) / 100f, (colors[1].color.b - colors[0].color.b) / 100f);

                //  fillArea.color = b + slope * percent;

                fillArea.color = gradient.Evaluate(percent);

                break;
        }
        */



    }



}
