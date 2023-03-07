using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DNAMathAnimation
{
    public class DNAMathAnim 
    {

        //Alright Well I can't tell what is happening 

        //Maybe pass initial position inside and keep calling it and stack the additions?


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
            float B = (Mathf.PI) / (frameCount*2);
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
            return A * Mathf.Sin(B * (x + C)) + D;
        }

        public static float cosEQ(float A, float B, float C, float D, float x)
        {
            return A * Mathf.Cos(B * (x + C)) + D;
        }





    }
}

