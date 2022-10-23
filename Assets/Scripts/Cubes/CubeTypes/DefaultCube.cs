using System;
using System.Collections.Generic;
using UnityEngine;

public class DefaultCube : CubeObj
{
    public override void Feature() // just Pop
    {
        DestroyPhase();
    }
    public override bool CheckExecRequirement() // Pop if there is a cube neigboring it 
    {
        if(CheckNeighbors()){ //Check if there are any neighbors
            return true;
        }
        else {
            return false;
        }
    }

    private bool CheckNeighbors()
    {
        Collider[] hitCollider;
        
        hitCollider = Physics.OverlapSphere(new Vector2(transform.position.x,transform.position.y), 0.3f); //Check if there is a cube neigboring it
        foreach(Collider col in hitCollider){
            if(col.GetComponent<CubeObj>()){ // If it's a cube
                CubeObj neighborsCubeObjScript = col.GetComponent<CubeObj>(); //Get the CubeObj script of the neighbor
                if(!neighborsCubeObjScript._willFeatureExecute && gameObject != col.gameObject){ // If the neighbor is not going to execute its feature
                    if(neighborsCubeObjScript.texture == texture) {
                        _willFeatureExecute = true; // This cube will execute its feature
                        ExecuteNeighbor(neighborsCubeObjScript);
                    } // If the neighbor has the same texture with this object execute other DefaultCube
                    else if(neighborsCubeObjScript.tag == "BaloonCube" && _willFeatureExecute) ExecuteNeighbor(neighborsCubeObjScript); // If the neighbor is a baloon and this cube is going to execute its feature execute Baloon
                }
            }
        }
        return _willFeatureExecute;
    }

    private void ExecuteNeighbor(CubeObj neighborsCubeObjScript){
        neighborsCubeObjScript._willFeatureExecute = true;//Set the neighbor execute status true
        neighborsCubeObjScript.Execute();//Execute the neighbor
    }
}
