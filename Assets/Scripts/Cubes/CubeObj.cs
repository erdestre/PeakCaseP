using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class CubeObj : MonoBehaviour
{
    public Texture2D texture;

    public ParticleSystem destroyVFX;
    public Color VFXColor;

    public AudioClip destroySFX;
    public AudioClip collectSFX;
    public AudioManager audioManager;

    bool willFeatureExecute = false;
    public bool _willFeatureExecute{
        get{
            return willFeatureExecute;
        }
        set{
            willFeatureExecute = value;
        }
    }
    bool isTouched;
    public bool _isTouched{
        get{
            return isTouched;
        }
    }

    private void Start() {
        GetComponentInChildren<MeshRenderer>().material.mainTexture = texture; //Set the texture of the cube
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>(); //Get the AudioManager 
    }
    public void Execute() { // Execute the feature of the cube if the requirement is met
        if(CheckExecRequirement()){
            Feature();
            if(this.isTouched) DecreaseMovesText(); // If the cube is executing itself because of the neighbor saying it, "Moves" text will not change. Else decrease the moves text
        }
        else this.isTouched = false;
    }

    public abstract void Feature(); // What the cube does when it is executed

    public abstract bool CheckExecRequirement(); // Check if the cube can be executed

    public virtual void OnMouseDown() { // Execute the cube when it is clicked
        this.isTouched = true; 
        Execute();
    } 

    //bool quitting;
    //void OnApplicationQuit() {this.quitting = true;} // OnDestroy method should not called when the application is quit. Otherwise it will cause an error.
    public void DestroyPhase() {
            // ---- Spawn New Cubes ---- //
            GameObject.Find("CubeSpawner").GetComponent<CubeSpawner>().SpawnCube(transform.parent, transform.position.y);

            // ---- Spawn VFX ---- //
            CreateEffects(this.destroyVFX, this.VFXColor, this.destroySFX);

            // ---- Goal Animation ---- //
            GameObject.Find("ProgressController").GetComponent<ProgressController>().CheckGoalStatus(transform.GetChild(0).gameObject); //GetChild(0) is Body of the cube // Collect SFX is played when the cube is collected by the goal

            // ---- Destroy the cube ---- //
            Destroy(gameObject);
    }

    private void CreateEffects(ParticleSystem destroyVFX, Color effectColor, AudioClip destroySFX){
            
            // ---- Particle Effect ---- //
        ParticleSystem effectObject = Instantiate(destroyVFX, transform.position, Quaternion.identity); // Create the particle effect object
        effectObject.GetComponent<Renderer>().material.color = effectColor;
        /*
            Color pixel_colour = texture.GetPixels()[320];                      // This is the code that I want to work. It would take the color of the pixel at the center of the texture 
            effect.GetComponent<Renderer>().material.color = pixel_colour;      // and set it as the color of the particle effect. It worked but colors were not accurate.
        */

            // ---- Sound Effect ---- //
        AudioSource audioSource = effectObject.gameObject.AddComponent<AudioSource>(); // Create an audio source for the particle effect inside the particle effect object
        audioSource.outputAudioMixerGroup = audioManager._sfxAudioSource; // Set the audio source to the sfx audio mixer group
        audioSource.clip = destroySFX;
        audioSource.Play();
    }
    private void DecreaseMovesText()
    {
        GameObject.Find("ProgressController").GetComponent<ProgressController>().DecreaseMoves();
        isTouched = false;
    }
    private void OnCollisionEnter(Collision other) {
        float layerDifference = -transform.position.y/10f; // The difference between the layers of the cubes
        transform.position = new Vector3(transform.position.x, transform.position.y, layerDifference);
    }
}
