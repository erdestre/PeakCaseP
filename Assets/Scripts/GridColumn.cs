using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridColumn : MonoBehaviour
{
    public GameObject[] cubes;
    public int maxHeight; 

    public void SetCubes(GameObject[] cubes, int maxHeight){
        this.cubes = new GameObject[cubes.Length];
        this.cubes = cubes;
        this.maxHeight = maxHeight;
    }
}
