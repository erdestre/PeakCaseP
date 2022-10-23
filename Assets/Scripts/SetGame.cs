using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetGame : MonoBehaviour
{
    [Header("Game Settings")]
    public int boardWidth; // how many columns
    public int boardHeight; // how many rows
    public GameObject[] incomingCubes; // which ones are incoming
    
    [Header("TopUI Settings")]
    public List<Goals> goals;
    public int howManyMoves; // How many moves the player has
    public ParticleSystem collectParticle;
    public AudioClip collectSFX;
    
    [Header("BottomUI Settings")]
    GameObject[] Skills; //max 4; // Skills the player can use
    GameObject gameGridBg; // The grid of the game
    CubeSpawner CubeSpawner; // The spawner of the cubes
    ProgressController ProgressController; // The controller of the progress
    // Start is called before the first frame update
    void Start()
    {
        GameObject gameGridBg = GameObject.Find("GameGridBg");  // The grid of the game
        CubeSpawner CubeSpawner = GameObject.Find("CubeSpawner").GetComponent<CubeSpawner>(); // The spawner of the cubes
        ProgressController ProgressController = GameObject.Find("ProgressController").GetComponent<ProgressController>(); // The controller of the progress

        SetGameGridBg(gameGridBg, boardWidth , boardHeight); 
        SetCubeSpawner(CubeSpawner, incomingCubes, boardWidth , boardHeight);
        SetGoalBar(goals);
        SetProgressController(ProgressController, howManyMoves);
    }

    private void SetGoalBar(List<Goals> goals)
    {
        RectTransform goalBar = GameObject.Find("GoalBar").GetComponent<RectTransform>();
        foreach (var goal in goals)
        {
            // ---- Goal Object Transform ---- //
            RectTransform newGoal = new GameObject("Goal").AddComponent<RectTransform>(); 
            newGoal.transform.SetParent(goalBar.transform);
            newGoal.sizeDelta = new Vector2(100, 100);
            newGoal.localScale = new Vector3(1, 1, 1);
            
            // ---- Goal Object Texture ---- //
            newGoal.gameObject.AddComponent<UnityEngine.UI.Image>().sprite = goal.sprite;
            
            // ---- Goal Object Text ---- //
            goal.text = new GameObject("Text"){ 
                transform = {
                    parent = newGoal.transform,
                    localScale = new Vector3(1, 1, 1),
                    localPosition = new Vector3(newGoal.rect.width/2, -newGoal.rect.width/2, 0),
                },
            }.AddComponent<TMPro.TextMeshProUGUI>();
        };
        goalBar.GetComponentInParent<Resolution>().SetResolution(goalBar.gameObject);
    }

    private void SetProgressController(ProgressController progressController, int howManyMoves)
    {
        progressController.SetProgressController(howManyMoves, goals, collectParticle, collectSFX);   
    }

    private void SetGameGridBg(GameObject gameGridBg, int boardWidth, int boardHeight)
    {
        gameGridBg.transform.position = new Vector3(0,0.54f,0);
        gameGridBg.GetComponent<SpriteRenderer>().size = new Vector2(boardWidth+0.14f, boardHeight+0.3f);
        new GameObject("Floor"){ // Create a new game object to be the floor
            transform = { 
                localScale = new Vector3(boardWidth/2, 1, 1),
                localPosition = new Vector3(0, -boardHeight/4f, 0) 
            },
        }.AddComponent<BoxCollider>(); // Add a collider to the floor for cube to stand on
    }
    private void SetCubeSpawner(CubeSpawner cubeSpawner, GameObject[] incomingCubes, int boardWidth, int boardHeight)
    {
        cubeSpawner.GetComponent<CubeSpawner>().SetCubes(incomingCubes, boardWidth, boardHeight); // Set the cubes in the CubeSpawner and spawn cubes
    }
}

    

