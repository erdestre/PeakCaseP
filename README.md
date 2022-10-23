# PeakCaseP
 
Dev Notes
- Erdem Süren Peak Project

*All changes can be done via the “SetGame” object in the hierarchy.

(0) SetGame Script:

(0.0) “Generate Manual Grid” 
*At the top of the inspector there is a “Generate Manual Grid” button. If you click that, there will be an object named Grid instantiated at the bottom of the hierarchy. You can add any cube to this grid object. (Look at 1.0).

(0.1) Grid Width and Length
*You can change any of these values. It will affect the grid and it will also be effecting the “Generate Manual Grid” option (Look at 0,0). If you changed it, Reset the Manual Grid (You can delete it from the hierarchy.)

(0.2) Goals
*If you want to change, add or remove any goal(s), add a new object, select the texture of this object and choose how many cubes you want the player to pop.

(0.3) IncomingCubes
*You can select which cubes will be in this level of the game.

(0.4) HowManyMoves
*You can select the move number


(1) Adding Cubes Manually

(1.0) EditorGrid EditorScript -> GridColumn Script:
*If you generated a ManualGrid object (Look at 0,0). You will see that it has (x) number of children. (X = width value that you selected). If you select one of them, in the inspector you can see the buttons you specified in incomingCubes (Look at 0.3). You can add or remove any cubes. After your level design is completed. Just leave it as it is. 

(2)Coding New Cubes

*If you want to code a new cube, create a new script and connect with the CubeObj abstract class. In the Feature() function, write a code that you want the cube to do. In the CheckExecRequirements(), write what will trigger the cube? (Note: DestroyPhase() function has to be called after the feature has done.) 

*If you coded a new cube. Check the cube’s inspector. All game settings can be done in the SetGame object except a cube’s personal variables.

(3) Sound Settings

Created a AudioMixer. AudioMixer has 3 channels.
1-Master
2-Music (There is no music in the game but in any case I added.)
3-SFX
All sound effects playing in SFX audio channel. There is no gui in this version of project but you can change it via AudioMixer



Thanks for your time.
Erdem Süren.


