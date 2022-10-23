using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    private int howManyMoves;
    private ParticleSystem collectParticle;
    private AudioClip collectSFX;
    private TextMeshProUGUI MovesText;
    private List<Goals> goals;
    GameObject GameEndPanel; // The panel that shows when the game ends

    private void Awake() {
        MovesText = GameObject.Find("Moves").GetComponentInChildren<TextMeshProUGUI>();
        goals = new List<Goals>();
        GameEndPanel = GameObject.Find("GameEnd");
        GameEndPanel.SetActive(false);
    }

    public void SetProgressController(int howManyMoves , List<Goals> goals, ParticleSystem collectParticle, AudioClip collectSFX) 
    {
        this.goals = goals;
        this.howManyMoves = howManyMoves;
        this.collectParticle = collectParticle;
        this.collectSFX = collectSFX;
        SetMovesText();
        foreach (Goals goal in goals) SetGoalsText(goal); 
    }
    private void SetMovesText(){ //Set the Moves Text 
        MovesText.text = howManyMoves.ToString();
    }
    public void DecreaseMoves(){ //Decrease the moves
        howManyMoves--;
        SetMovesText(); //Decrease the Moves Text
        if(howManyMoves <= 0){
            LoseGame();
        }
    }
    private void SetGoalsText(Goals goal){
        goal.text.text = goal.howManyCubes.ToString();
    }
    public void CheckGoalStatus(GameObject cube){ // Check if the cube is a goal cube and decrease the goal if it is
        foreach (Goals goal in goals) {
            if(goal.sprite.texture == cube.GetComponent<Renderer>().material.mainTexture){
                goal.DecreaseGoal();
                if(goal.howManyCubes <= 0) WinGame(); // If the goal count is 1 or less, the player wins. // 1 because the goal is decreasing number after this function is called.
                ArrangeGoalCubeObject(cube, goal);
                break;
            }
        } 
    }

    private void ArrangeGoalCubeObject(GameObject cube, Goals goal)
    {
        GoalTrigger goalCube = Instantiate(cube).AddComponent<GoalTrigger>(); // Instantiate body of the cube
        goalCube.transform.position = cube.transform.position;
        goalCube.transform.localScale /= 2; // Make it half the size // Normally the parent object is half the size. After detach it it's instantiating with full size.
        goalCube.StartGoalSequence(goal, collectParticle, collectSFX); // Start the goal animation //goal is for the goal position // collectParticle is for the particle effect // collectSFX is for the sound effect
    }

    private void WinGame()
    {
        HideGame();
        OpenStatusPanel("You Win!");
    }
    private void LoseGame()
    {
        HideGame();
        OpenStatusPanel("You Lose!");
    }

    private void HideGame()
    {
        Camera mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>(); // Hide the game
        mainCamera.cullingMask = LayerMask.GetMask("UI"); 
    }

    private void OpenStatusPanel(String text)
    {
        GameEndPanel.SetActive(true);
        GameObject.Find("StatusText").GetComponent<TextMeshProUGUI>().text = text;
    }
}
