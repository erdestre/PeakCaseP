using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonCube : CubeObj
{
    public override bool CheckExecRequirement()
    {
        if(_isTouched){
            return false; // If checking because of the touch, return false
        }
        else return true; // If checking because of the neighbor, return true
    }

    public override void Feature()
    {
        DestroyPhase();
    }
}
