using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCube : CubeObj
{
    public Vector2 rocketDirection;
    public ParticleSystem rocketVFX;
    public Transform currentVFX; // After this object destroyed this will be needed For Changing the VFX's parent object. Otherwise the VFX will be destroyed with this object.
    private GameObject tempCube = null; // This will be used for creating a new cube while the rocket cube doing its feature. // Otherwise the cubes above is starting the falling process.
    public override bool CheckExecRequirement()
    {
        if(_isTouched){
            CreateTempCube();
            return true;
        }
        else return false;
    }

    public override void Feature()
    {
        // ---- Rocket ---- //
        Rigidbody rb = GetComponent<Rigidbody>();
        BoxCollider bc = GetComponent<BoxCollider>();

        bc.isTrigger = true; // Make the cube trigger so it can pass through the other cubes.
        bc.size = new Vector3(0.1f, 0.1f, 0.1f); // Make the size of the cube smaller so it won't destroy the upper and lower cubes.
        
        rb.useGravity = false;
        if(this.rocketDirection.x != 0){ // If the rocket is moving in the x direction just x will not be freezing.
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.constraints &= ~RigidbodyConstraints.FreezePositionX;
        } // Not changing for the y direction because cubes are always moving in the y direction. So not need to change anything. (there is no rocket is working upward or downward right now)
        rb.AddForce(this.rocketDirection*350f);
        

        // ---- VFX ---- //
        StartRocketVFX();
    }

    private void CreateTempCube(){ // This will be used for creating a new cube while the rocket cube doing its feature. // Otherwise the cubes above is starting the falling process.
        tempCube = new GameObject("TempCube");
        tempCube.transform.position = gameObject.transform.position;
        tempCube.transform.rotation = gameObject.transform.rotation;
        tempCube.transform.localScale = gameObject.transform.lossyScale;
        BoxCollider tempCubebc = tempCube.AddComponent<BoxCollider>();
        tempCubebc.center = new Vector3(0,0.5f,0); //0.5 is the half of the cube's height 
        tempCubebc.size = new Vector3(10, 0, 1); //0 because without touching the other right and left cubes we need to hold the cubes above. 0 provides a flat area. // 10 is not a special number. it just should be enough to fill the board width.
    }
    private void StartRocketVFX()
    {
        this.currentVFX = Instantiate(rocketVFX, transform.position, Quaternion.identity).gameObject.transform;
        this.currentVFX.parent = transform;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "MapBoundaries"){
            if(tempCube != null) Destroy(tempCube);
            DestroyPhase();
        } else {
            other.gameObject.GetComponent<CubeObj>().Feature();
        }
    }
}
