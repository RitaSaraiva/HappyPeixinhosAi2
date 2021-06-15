# Report on the 2st Artificial Intelligence Project

## Happy Peixinhos

**Project carried out by:**
- [Rita Saraiva, a21807278](https://github.com/RitaSaraiva)

## Description of the solution

### Decision Tree
![Decision Tree](decisionTree.png "DecisionTree")

For the decision trees the main inspiration was the solution of one of the 
exercises related to the decisions theme, specifically the exercise about fishes
with similar behaviours to the requirements of this project. Only one decision 
tree exists,as it checks for the type of fish calling it, which eliminates the 
need for multiple trees.
The implementation of the behaviour was based on the decision tree code provided
by the teacher.
Five action nodes exist (`Wander, Seek, Flee, Eat and Reproduce`) and four decision
nodes including the root node (`Target in sight, Danger in sight, Can reproduce, 
Target in eating range`). Each behaviour is explained better in the **Fishes 
Behaviour**.
As shown in the figure above, the root node `TargetInSight`checks if a
target exists in range, the next decision node checks if it's close enough
to eat it, if it is we reach the action node `EatTarget` and the fish eats the 
target, if it isn't we reach the action node `MoveToTarget` which uses the seek
behaviour to follow the target. In case there's no target in sight, the fish 
reaches the decision node `EnemyInSight`, and if there's an enemy he will do the
action node `Evade`, if there isn't he will reach the decion node 
`EnoughEnergyToReproduce`. If there's enough energy the fish will reach the 
`Reproduce` action node, splitting in two, but if there isn't it will do the 
`Wander` action node.
 
### FishesBehaviour

#### Wander
	The Wander movement behaviour works by using the bounds of the `GameArea` collider
	and generating a random position inside it to which the fish will move to.
	The `GameArea` was inspired by the video "03 Unity Fish AI Tutorial - How to 
	Spawn NPC Fish and Make Them Swim!" by the Messy Coder. It was adapted to make
	use of unity's collider bounds.

#### Flee
	The Flee movement behaviour works by calculating the vector contrary to the
	direction to the nearest enemy, while simultaneously keeping the fish inside
	the `GameArea` by checking it's bounds and changing course if the fish moves
	within an area of "padding" as to not leave the game area.

#### Pursue
	The Persue movement behaviour works by calculating the vector of the direction
	to the nearest target moving the fish towards him. 

#### Reproduce
	The Reproduce behaviour works by dividing the fish's energy by two and 
	instantiating a copy of the fish.

#### Eat Target
	The Eat Target behaviour works by checking if the target is a fish or an algae
	and casting the target as `IFood`, then removing it from the appropriate target
	list and removing it from the scene.

#### TargetInSight
	The fish knows what other fishes and algae are nearby by making use of the
	`OnTriggerEnter` and `OnTriggerExit` unity methods, adding any fish or algae
	that gets within range to the appropriate list and removing them from said list
	when they get out of range. The `TargetInSight` decision node works by first checking 
	which type of fish is calling it and checks the lists of what it can eat in order
	of preference. Finally if theres an appropriate target it becomes the current target.

#### DangerInSight
	The `DangerInSight´ decision node works in a similar way to `TargetInSight`,
	but checking what fish can eat the fish that calls it.

#### TargetInEatRange
	The `TargetInEatRange` decision node works by calculating the distance between
	the fish that calls it and its current target and if it within eating range returns
	true, otherwise returning false.

#### CanReproduce
	The `CanReproduce` decision node works by checking if the fish that calls it has
	enough energy to reproduce.

## References

### Code

1.  **AIUnityExamples** - MovementOptimize, SimpleDecisionTrees  (provided by the 
                          professor), 
    https://github.com/fakenmc/AIUnityExamples/tree/master/MovementOptimize
    https://github.com/fakenmc/AIUnityExamples/tree/master/SimpleDecisionTrees

### Website

1. https://www.youtube.com/watch?v=Duy4ZrT8STw - Final Wander Movement 
2. https://youtu.be/-tfWYnOs2Ss - First Wander Movement 
3. https://answers.unity.com/questions/986723/how-would-i-make-a-object-flee-from-player.html - Flee Movement
4. https://docs.unity3d.com/2021.1/Documentation/Manual/UnityManual.html - Overall Assistance
5. https://pt.stackoverflow.com/ - Overall Assistance


### Help from colleagues

1. Pedro Inácio(21802050) - Help to better understand how Decision Trees work 
			  - Help to implement the TargetInSight method
			  - Help with mantaining the fishes inside Game Area
                          - Gave the idea on how to do the graphs 

### Moodle Exercices

1-

