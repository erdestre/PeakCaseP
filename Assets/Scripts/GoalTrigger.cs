using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{
    private Texture2D cubeTexture;
    Goals aimedGoal; // Which goal object that this cube will be going to
    float animationTime = 0.7f; // Time of the animation

    ParticleSystem collectParticle;
    AudioClip collectSFX;


    public void StartGoalSequence(Goals goal, ParticleSystem collectParticle, AudioClip collectSFX){
        this.aimedGoal = goal;
        this.collectParticle = collectParticle;
        this.collectSFX = collectSFX;
        StartCoroutine("DoAnimation", goal.text.transform);
    }
    private IEnumerator DoAnimation(Transform goalPosition)
    {
        Animation anim = gameObject.AddComponent<Animation>();
        AnimationCurve curve; // Create a curve to move the GameObject and assign to the clip
        
        AnimationClip clip = new AnimationClip(); // create a new AnimationClip
        clip.legacy = true;

        
        Keyframe[] keys = new Keyframe[2]; 
        keys[0] = new Keyframe(0, transform.position.x); //First Position X of the cube
        keys[1] = new Keyframe(animationTime, goalPosition.position.x-0.25f); //Last Position X of the cube
        curve = new AnimationCurve(keys); 
        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);
        
        keys[0] = new Keyframe(0, transform.position.y); // First Position Y of the cube
        keys[1] = new Keyframe(animationTime, goalPosition.position.y+0.25f); // Last Position Y of the cube
        curve = new AnimationCurve(keys);
        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        // now animate the GameObject
        anim.AddClip(clip, clip.name);
        anim.Play(clip.name);

        yield return new WaitForSeconds(animationTime); // Wait for the animation to finish
        
        aimedGoal.SetGoalText(); // it has been already decreased in the progress controller. So just set the text.
        GetEffect();
        Destroy(gameObject);
        
    }

    private void GetEffect()
    {
            // ---- VFX ---- //
        ParticleSystem effectObject = Instantiate(this.collectParticle, transform.position, Quaternion.identity); // Create the particle effect object

            // ---- Sound Effect ---- //
        AudioSource mainSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        AudioSource audioSource = effectObject.gameObject.AddComponent<AudioSource>(); // Create an audio source for the particle effect inside the particle effect object
        audioSource.outputAudioMixerGroup = GameObject.Find("AudioManager").GetComponent<AudioManager>()._sfxAudioSource; // Set the audio source to the sfx audio mixer group
        audioSource.clip = this.collectSFX;
        audioSource.Play();
    }

    
}
