using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    GameObject[] cubesCanSpawn;
    int boardHeight;
    private void Start() {
        //Get Spawnable Cubes
    }
    public void SpawnCube(Transform parent, float yPosition){ //Spawn a Cube at position
        ArrangeCubeTransform(Instantiate(GetRandomCube()), parent, yPosition);
    }
    public void SpawnCube(Transform parent, float yPosition, GameObject manualCube){ //Send Specific Cube at position
        
        ArrangeCubeTransform(manualCube, parent, yPosition);
    }
    void ArrangeCubeTransform(GameObject newCube, Transform parent, float yPosition){ //Arrange the transform of the new cube
        newCube.transform.position = new Vector2(parent.position.x, 1+(boardHeight/2) + yPosition); // 0.55f = 0.50(cube size) + 0.05(spacing)
        newCube.transform.parent = parent;
    }
    public GameObject GetRandomCube(){ //Return a random Cube
        return cubesCanSpawn[Random.Range(0, cubesCanSpawn.Length)];
    }

    public void SetCubes(GameObject[] incomingCubes, int boardWidth, int boardHeight){ //Set the cubes that can be spawned
        Transform gameArea = GameObject.Find("GameArea").transform;
        GameObject manualGrid = GameObject.Find("ManualGrid");

        this.boardHeight = boardHeight;

        this.cubesCanSpawn = new GameObject[incomingCubes.Length];
        this.cubesCanSpawn = incomingCubes;

        for(int i = 0; i < boardWidth; i++){
            GameObject Column = CreateColumn(i, boardWidth, gameArea);
            
            if(manualGrid != null){ //If there is a manual grid, spawn cubes from it
                for(int a = 0; a < boardHeight; a++){ //Spawn the first cube in the manual grid 
                    if(manualGrid.transform.GetChild(i).childCount > 0) SpawnCube(Column.transform, Column.transform.childCount, manualGrid.transform.GetChild(i).GetChild(0).gameObject); 
                    // its changing the child objects parent. that's why second getchild is always 0.
                    else break;
                } 
            }
            for(int a = Column.transform.childCount; a < boardHeight; a++){ //After manual cubes, Spawn cubes until the board height is reached
                SpawnCube(Column.transform, Column.transform.childCount); // Spawn cube bottom to top;
            } 
        }
        Destroy(manualGrid); //Destroy the manual grid after it has been used
    }

    private GameObject CreateColumn(int i, int boardWidth, Transform gameArea){ //Create a Column
        GameObject Column = new GameObject("Column" + i){
            transform = {
                parent = gameArea,
                localPosition = new Vector3((i - 0.5f - (boardWidth/2f)+1 )* 0.5f, (boardWidth)* 0.5f, 0)
            }
        };
        return Column;
    }
}
