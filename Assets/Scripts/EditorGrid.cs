using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(SetGame))]
public class EditorGrid : Editor
{
    GameObject grid;

    public override void OnInspectorGUI()
    {
        SetGame setGameScript = (SetGame)target;
        GameObject GameGrid;
        
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        if(GUILayout.Button("Generate Manual Grid")) {
            GameGrid = CreateGrid(setGameScript);
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        DrawDefaultInspector();
    }

    GameObject CreateGrid(SetGame setGame)
    { 
        try{
            DestroyImmediate(GameObject.Find("ManualGrid").gameObject);
        } catch{}
        grid = new GameObject("ManualGrid");

        grid.transform.position = Vector3.zero;
        for(int i = 0; i<setGame.boardWidth; i++){
            new GameObject("Column" + i){
                transform = {
                    parent = grid.transform,
                    localPosition = new Vector3((i - 0.5f - (setGame.boardWidth/2f)+1 )* 0.5f, (setGame.boardWidth)* 0.5f, 0)
                }
            }.AddComponent<GridColumn>().SetCubes(setGame.incomingCubes, setGame.boardHeight);
        }
        return grid;
    }
}

[CustomEditor(typeof(GridColumn))]
public class EditorGridColumn : Editor {
    public override void OnInspectorGUI(){
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        
        GridColumn gridColumn = (GridColumn)target;
        foreach(GameObject cube in gridColumn.cubes){
            
            if(GUILayout.Button("Generate " + cube.name)){
                if(gridColumn.transform.childCount < gridColumn.maxHeight){
                    GameObject newCube = Instantiate(cube);
                    newCube.GetComponentInChildren<MeshRenderer>().material.mainTexture = newCube.GetComponent<CubeObj>().texture; // This Line
                    Debug.LogError("//LOOK AT HERE LATER!! //it gives a leak error. But that's the point of the code.");
                    newCube.transform.position = new Vector2(gridColumn.transform.position.x, gridColumn.transform.childCount * 0.55f); // 0.55f = 0.50(cube size) + 0.05(spacing)
                    newCube.transform.parent = gridColumn.transform;
                }
            }
        }
            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            if(GUILayout.Button("Remove Last Added Cube")){
                if(gridColumn.transform.childCount>0) DestroyImmediate(gridColumn.transform.GetChild(gridColumn.transform.childCount-1).gameObject);
            }
        
    }
}