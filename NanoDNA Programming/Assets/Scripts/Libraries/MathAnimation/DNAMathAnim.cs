using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DNAMathAnimation
{
    public class DNAMathAnim
    {
        public delegate void MyFunc(int val);

        public enum Axis
        {
            //Replace the index with the axis
            All,
            x,
            y,
            z,
        }

        public static int getFrameNumber (float elapsedTime)
        {
            if (Application.targetFrameRate == -1)
            {
                //Choose 60 
                return Mathf.FloorToInt(60 * elapsedTime);
            } else
            {
                return Mathf.CeilToInt(Application.targetFrameRate * elapsedTime);
            }
           
        }

        //Alright Well I can't tell what is happening 

        //Maybe pass initial position inside and keep calling it and stack the additions?

        //
        //Relocation
        //

        //Add this to a animation 
        public static IEnumerator animateLinearRelocationLocal(Transform trans, Vector3 OGPos, int frameCount, int index, bool singleAxis)
        {
            //Index determines axis
            //x = 0
            //y = 1
            //z = 0

            Vector3 startPos = trans.localPosition;
            Vector3 fullAdd = Vector3.zero;

            float singleSlope = 0;
            Vector3 slope = Vector3.zero;
            if (singleAxis)
            {
                switch (index)
                {
                    case 0:
                        singleSlope = (float)(OGPos.x - trans.localPosition.x) / frameCount;
                        break;
                    case 1:
                        singleSlope = (float)(OGPos.y - trans.localPosition.y) / frameCount;
                        break;
                    case 2:
                        singleSlope = (float)(OGPos.z - trans.localPosition.z) / frameCount;
                        break;
                }
            }
            else
            {
                slope = (OGPos - trans.localPosition) / frameCount;
            }


            for (int i = 0; i < frameCount; i++)
            {
                if (singleAxis)
                {
                    switch (index)
                    {
                        case 0:
                            fullAdd = new Vector3(fullAdd.x + singleSlope, fullAdd.y, fullAdd.z);
                            break;
                        case 1:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y + singleSlope, fullAdd.z);
                            break;
                        case 2:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y, fullAdd.z + singleSlope);
                            break;
                    }
                }
                else
                {
                    fullAdd = fullAdd + slope;
                }
                trans.localPosition = startPos + fullAdd;

                yield return null;
            }

            trans.localPosition = OGPos;

        }

        public static IEnumerator animateSinusoidalRelocationLocal(Transform trans, Vector3 OGPos, int frameCount, int index, bool singleAxis)
        {
            //Index determines axis
            //x = 0
            //y = 1
            //z = 0

            Vector3 startPos = trans.localPosition;
            Vector3 fullAdd = Vector3.zero;

            //Length coefficient
            float B = (Mathf.PI) / frameCount;
            //Amplitude
            float A = 0;
            Vector3 As = Vector3.zero;

            if (singleAxis)
            {
                switch (index)
                {
                    case 0:
                        A = calcSinAmplitude(OGPos.x - trans.localPosition.x, B, frameCount);
                        break;
                    case 1:
                        A = calcSinAmplitude(OGPos.y - trans.localPosition.y, B, frameCount);
                        break;
                    case 2:
                        A = calcSinAmplitude(OGPos.z - trans.localPosition.z, B, frameCount);
                        break;
                }
            }
            else
            {
                As = new Vector3(calcSinAmplitude(OGPos.x - trans.localPosition.x, B, frameCount), calcSinAmplitude(OGPos.y - trans.localPosition.y, B, frameCount), calcSinAmplitude(OGPos.z - trans.localPosition.z, B, frameCount));
            }

            for (int i = 0; i < frameCount; i++)
            {
                if (singleAxis)
                {
                    float add = sinEQ(A, B, 0, 0, i);
                    switch (index)
                    {
                        case 0:
                            fullAdd = new Vector3(fullAdd.x + add, fullAdd.y, fullAdd.z);
                            break;
                        case 1:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y + add, fullAdd.z);
                            break;
                        case 2:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y, fullAdd.z + add);
                            break;
                    }
                }
                else
                {
                    Vector3 add = new Vector3(sinEQ(As[0], B, 0, 0, i), sinEQ(As[1], B, 0, 0, i), sinEQ(As[2], B, 0, 0, i));
                    fullAdd = fullAdd + add;
                }
                trans.localPosition = startPos + fullAdd;
                yield return null;
            }

            trans.localPosition = OGPos;
        }

        public static IEnumerator animateCosinusoidalRelocationLocal(Transform trans, Vector3 OGPos, int frameCount, int index, bool singleAxis)
        {
            //Index determines axis
            //x = 0
            //y = 1
            //z = 0

            Vector3 startPos = trans.localPosition;
            Vector3 fullAdd = Vector3.zero;

            //Length coefficient
            float B = (Mathf.PI) / (frameCount * 2);
            //Amplitude
            float A = 0;
            Vector3 As = Vector3.zero;

            if (singleAxis)
            {
                switch (index)
                {
                    case 0:
                        A = calcCosAmplitude(OGPos.x - trans.localPosition.x, B, frameCount);
                        break;
                    case 1:
                        A = calcCosAmplitude(OGPos.y - trans.localPosition.y, B, frameCount);
                        break;
                    case 2:
                        A = calcCosAmplitude(OGPos.z - trans.localPosition.z, B, frameCount);
                        break;
                }
                Debug.Log(A);
            }
            else
            {
                As = new Vector3(calcCosAmplitude(OGPos.x - trans.localPosition.x, B, frameCount), calcCosAmplitude(OGPos.y - trans.localPosition.y, B, frameCount), calcCosAmplitude(OGPos.z - trans.localPosition.z, B, frameCount));
            }

            for (int i = 0; i < frameCount; i++)
            {
                if (singleAxis)
                {
                    float add = sinEQ(A, B, 0, 0, i);
                    switch (index)
                    {
                        case 0:
                            fullAdd = new Vector3(fullAdd.x + add, fullAdd.y, fullAdd.z);
                            break;
                        case 1:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y + add, fullAdd.z);
                            break;
                        case 2:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y, fullAdd.z + add);
                            break;
                    }
                }
                else
                {
                    Vector3 add = new Vector3(cosEQ(As[0], B, 0, 0, i), cosEQ(As[1], B, 0, 0, i), cosEQ(As[2], B, 0, 0, i));
                    fullAdd = fullAdd + add;
                }
                trans.localPosition = startPos + fullAdd;
                yield return null;
            }

            trans.localPosition = OGPos;

        }

        public static IEnumerator animateReboundRelocationLocal(Transform trans, Vector3 OGPos, int frameCount, int index, bool singleAxis)
        {
            Vector3 startPos = trans.localPosition;
            Vector3 fullAdd = Vector3.zero;

            //Amplitude
            float A = 0;
            Vector3 As = Vector3.zero;

            if (singleAxis)
            {
                switch (index)
                {
                    case 0:

                        A = calcReboundAmplitude(OGPos.x - trans.localPosition.x, frameCount);
                        break;
                    case 1:
                        A = calcReboundAmplitude(OGPos.y - trans.localPosition.y, frameCount);
                        break;
                    case 2:
                        A = calcReboundAmplitude(OGPos.z - trans.localPosition.z, frameCount);
                        break;
                }
            }
            else
            {
                As = new Vector3(calcReboundAmplitude(OGPos.x - trans.localPosition.x, frameCount), calcReboundAmplitude(OGPos.y - trans.localPosition.y, frameCount), calcReboundAmplitude(OGPos.z - trans.localPosition.z, frameCount));
            }

            for (int i = 0; i < frameCount; i++)
            {
                if (singleAxis)
                {
                    float add = reboundEQ(A, frameCount, i);
                    switch (index)
                    {
                        case 0:
                            fullAdd = new Vector3(fullAdd.x + add, fullAdd.y, fullAdd.z);
                            break;
                        case 1:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y + add, fullAdd.z);
                            break;
                        case 2:
                            fullAdd = new Vector3(fullAdd.x, fullAdd.y, fullAdd.z + add);
                            break;
                    }
                }
                else
                {
                    Vector3 add = new Vector3(reboundEQ(As[0], frameCount, i), reboundEQ(As[1], frameCount, i), reboundEQ(As[2], frameCount, i));
                    fullAdd = fullAdd + add;
                }
                trans.localPosition = startPos + fullAdd;
                yield return null;
            }


            trans.localPosition = OGPos;

        }

        //
        //Increment Number/Timer
        //

        public static IEnumerator animateLinearSliderFill(Slider slider, float final, int frameCount)
        {
            Debug.Log("Hello");

            float change = final - slider.value;

            float slope = (float)(change) / frameCount;

            float addition = 0;
            float startVal = slider.value;

            for (int i = 0; i < frameCount; i++)
            {
                addition = addition + slope;

                slider.value = startVal + addition;
                yield return null;
            }

            slider.value = final;
        }

        public static IEnumerator animateSineSliderFill(Slider slider, float final, int frameCount)
        {
            float change = final - slider.value;

            //Length coefficient
            float B = (Mathf.PI) / (frameCount);

            //Amplitude
            float A = 0;

            //Get Amplittude
            A = calcSinAmplitude(change, B, frameCount);

            float addition = 0;
            float startVal = slider.value;

            for (int i = 0; i < frameCount; i++)
            {
                addition = addition + sinEQ(A, B, 0, 0, i);

                slider.value = startVal + addition;
                yield return null;
            }

            slider.value = final;

        }

        public static IEnumerator animateCosineSliderFill(Slider slider, float final, int frameCount)
        {
            float change = final - slider.value;

            //Length coefficient
            float B = (Mathf.PI) / (frameCount * 2);

            //Amplitude
            float A = 0;

            //Get Amplittude
            A = calcCosAmplitude(change, B, frameCount);

            float addition = 0;
            float startVal = slider.value;

            for (int i = 0; i < frameCount; i++)
            {
                addition = addition + cosEQ(A, B, 0, 0, i);

                slider.value = startVal + addition;
                yield return null;
            }

            slider.value = final;

        }

        public static IEnumerator animateLinearFloatCount (MyFunc func, float final, int frameCount)
        {
            float change = final - 0;

            float slope = (float)(change) / frameCount;

            float addition = 0;
            float startVal = 0;
            float total = startVal;

            for (int i = 0; i < frameCount; i++)
            {
                addition = addition + slope;
                total += addition + slope;
                int difference = 0;

                if (addition > 1)
                {
                    while(addition >= 1)
                    {
                        addition--;
                        difference++;   
                    }
                }

                func(difference);
                yield return null;
            }

            func(1);

        }

        //Shake

        public static IEnumerator animateShake(Transform trans, int frameCount)
        {
            //Let's do 5 cycles of a sinusoidal eq

            //Length coefficient
            float B = (2f * Mathf.PI) / (frameCount / 5);
            //Amplitude
            float A = 0;
           
            Vector3 startPos = trans.localPosition;
         
            //Movement of 10 pixels? and 10 degrees? 

            float xMov = 0;
            float yMov = 0;
            float rot = 0;


            A = calcSinAmplitude(120, (Mathf.PI) / frameCount, frameCount);
           // rotA = calcCosAmplitude(40, (Mathf.PI) / (frameCount * 2), frameCount);

            //As = new Vector3(, calcSinAmplitude(10, B, frameCount), calcSinAmplitude(10, B, frameCount));

            for (int i = 0; i < frameCount; i++)
            {

                xMov = xMov + sinEQ(A, B, 30, 0, i);
                yMov = yMov + sinEQ(A, B, 60, 0, i);
             
                trans.localPosition = startPos + new Vector3(xMov, yMov, 0);
             
                yield return null;
            }

            trans.localPosition = startPos;
           
        }

        public static float calcReboundAmplitude(float total, float finalTime)
        {
            float a = 1;
            float b = -0.97222f;
            float c = 5.5972222f;
            float d = -14.65277777f;
            float e = 9.027777777f;


            float intFun = (a * finalTime) + getPolInt(b, 2, finalTime, finalTime) + getPolInt(c, 3, finalTime, finalTime) + getPolInt(d, 4, finalTime, finalTime) + getPolInt(e, 5, finalTime, finalTime);

            return total / intFun;
        }

        public static float getPolInt(float coef, float pow, float T, float j)
        {
            return (coef * Mathf.Pow(T, pow)) / (pow * Mathf.Pow(j, pow - 1));
        }

        public static float reboundEQ(float A, float P, float x)
        {
            float a = 1;
            float b = -0.97222f;
            float c = 5.5972222f;
            float d = -14.65277777f;
            float e = 9.027777777f;

            //P is the period/total time of the function
            return A * (a + b * Mathf.Pow(x / P, 1) + c * Mathf.Pow(x / P, 2) + d * Mathf.Pow(x / P, 3) + e * Mathf.Pow(x / P, 4));
        }


        public static float calcSinAmplitude(float value, float period, float finalTime)
        {
            return (value * period) / (-Mathf.Cos(period * finalTime) + 1);
        }

        public static float calcCosAmplitude(float value, float period, float finalTime)
        {
            return (value * period) / (Mathf.Sin(period * finalTime));
        }

        public static float sinEQ(float A, float B, float C, float D, float x)
        {
            return A * Mathf.Sin((B * x) + C) + D;
        }

        public static float cosEQ(float A, float B, float C, float D, float x)
        {
            return A * Mathf.Cos((B *x) + C) + D;
        }





    }
}

