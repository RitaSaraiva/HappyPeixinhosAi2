# Report on the 2nd Artificial Intelligence Project

## Happy Peixinhos

**Project carried out by:**
- [Rita Saraiva, a21807278](https://github.com/RitaSaraiva)

## Reasearch

### Turan, E., & Çetin, G. (2019). Using Artificial Intelligence for Modeling of the Realistic Animal Behaviors in a Virtual Island. Computer Standards & Interfaces

This paper showcases a virtual island environment, implemented in Unity 3D, in 
which animal agents were integrated, making decisions on the actions to take using
Fuzzy Logic, a form a logic that considers truth values not as booleans (either 
false or true - 0 and 1 integers, respectively) but as any real number between 0
and 1, inclusive to both the upper and lower limit, which attemps to simulate that
decisions are made based on information that is often imprecise, vague or lacking
in certainty. The system implemented in this specific case took into account the
animal's Confidence Level, Behaviour Type and Health as input given to the system
and determined an action based on those, which led to the individual animals 
taking into account the conditions of the environment around them and make 
decisions on how to behave in various different situations, providing satisfactory
results. The techniques used were not the same as used in this paper but it provides
a good look at an interesting alternative decision making system that seems to 
provide satisfactory results if properly implemented and the inputs for the system
are well thought out.

### Kiss, A. & Pusztai, G. Animal Farm (2021) - a complex artificial life 3D framework

This article speaks about the creation of simplified environment with various 
animals using Unity 3D to simulate their behaviours and analyse the acquired data
though multiple simulations. The animals in this article have similar behaviours
to the requirements of project. The article never specifies what system was used
for the decision making only referring that "All animals are controlled by an 
individual logic model, which determines the next action of the animal, based on 
their needs and surrounding environment.", but it explains the different behaviours
and just like in my project it's possible to change the parameters in a way that
alters the simulation. In the case of this article, it also referred how the made
use of HeightMaps and NavMeshes to assist in the creation of the terrain and how
the animals walk though it, although it's not important to this project because 
its environment is in a fish tank its it's relevant information for what was taught
during the semester

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
	when they get out of range. The `TargetInSight` decision node works by first
	checking which type of fish is calling it and checks the lists of what it can
	eat in order of preference. Finally if theres an appropriate target it becomes
	the current target.

#### DangerInSight
The `DangerInSight´ decision node works in a similar way to `TargetInSight`,
	but checking what fish can eat the fish that calls it.

#### TargetInEatRange
The `TargetInEatRange` decision node works by calculating the distance between
	the fish that calls it and its current target and if it within eating range 
	returns true, otherwise returning false.

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

### Articles

1. Turan, E., & Çetin, G. (2019). Using Artificial Intelligence for Modeling of the Realistic Animal Behaviors in a Virtual Island. Computer Standards & Interfaces, 103361. doi:10.1016/j.csi.2019.103361 - https://www.sciencedirect.com/science/article/abs/pii/S0920548919300935
2.  Kiss, A. & Pusztai, G. Animal Farm (2021) - a complex artificial life 3D framework - 
https://www.researchgate.net/profile/Attila-Kiss-3/publication/351128596_Animal_Farm_-a_complex_artificial_life_3D_framework/links/6089871a458515d315e2eeb3/Animal-Farm-a-complex-artificial-life-3D-framework.pdf
### Help from colleagues

1. Pedro Inácio(21802050) 
   - Help to better understand how Decision Trees work 
   - Help to implement the TargetInSight method
   - Help with mantaining the fishes inside Game Area
   - Gave the idea on how to do the graphs 

### Moodle Exercices

1- Decisions - https://secure.grupolusofona.pt/ulht/moodle/pluginfile.php/994156/mod_resource/content/1/04_decisoes.pdf 

