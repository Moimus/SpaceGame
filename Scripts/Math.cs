using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Math
{
    public static float cubed(float n)
    {
        float x = n * n * n;
        return x;
    }

    public static float squared(float n)
    {
        float x = n * n;
        return x;
    }


    //TODO
    //see https://en.wikipedia.org/wiki/Proportional_navigation
    public static Vector3 getProNavRotationVector(Vector3 origin, Vector3 target, Vector3 originVelocity, Vector3 targetVelocity)
    {
        Vector3 rangeR = target - origin;
        Vector3 rangeVelocityVr = targetVelocity - originVelocity;
        Vector3 rotationVector = (Vector3.Scale(rangeR,rangeVelocityVr)) / (rangeR.magnitude * rangeR.magnitude);
        return rotationVector;
    }

}
