using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Goals //In the SetGame script. This is holding the goals
{
    public Sprite sprite; //
    public int howManyCubes;
    [HideInInspector] public TMPro.TextMeshProUGUI text; // Don't need to see this in the inspector

    public void SetGoalText(){ 

        text.text = howManyCubes.ToString();
    }
    public void DecreaseGoal(){
        howManyCubes--;
    }
}
