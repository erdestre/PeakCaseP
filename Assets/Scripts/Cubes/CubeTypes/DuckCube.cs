using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckCube : CubeObj
{
    public override bool CheckExecRequirement()
    {
        return false;
    }

    public override void Feature()
    {
        DestroyPhase();
    }

    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "Floor"){
            Feature();
        }
    }
}
