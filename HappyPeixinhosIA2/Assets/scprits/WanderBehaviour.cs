using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AIUnityExamples.Movement.Core;

public class WanderBehaviour : MonoBehaviour
{
    [SerializeField] private GameArea gameArea;
    [SerializeField] private float maxWallDist;
    
    public float wandertime;
    public float movementSpeed;

    // Update is called once per frame
    void Update()
    { 
        float relativeX = transform.position.x - gameArea.center.x;
        float relativeY = transform.position.y - gameArea.center.y;
        float relativeZ = transform.position.z - gameArea.center.z;
        bool onLimits = (relativeX> 0 && gameArea.Xmax - relativeX < maxWallDist) || (relativeX < 0 && gameArea.Xmin - relativeX < -maxWallDist)
        || (relativeX < 0 && gameArea.Xmin - relativeX < -maxWallDist) || (relativeY < 0 && gameArea.Ymin - relativeY < -maxWallDist)
        || (relativeZ> 0 && gameArea.Zmax - relativeZ < maxWallDist) || (relativeZ < 0 && gameArea.Zmin - relativeZ < -maxWallDist);

        //float printx = gameArea.Xmax.relativeX;
        
        //if (wandertime > 0){
            if (onLimits){
                transform.Translate(-Vector3.forward * movementSpeed);
                print("onlimits");
            }
            
            else transform.Translate(Vector3.forward * movementSpeed);
            
            //wandertime -= Time.deltaTime; 
        //}
        //else {
        //    wandertime = Random.Range(5.0f, 7.0f);
        //    Wander ();
        //}
    }

    void Wander (){
        transform.eulerAngles = new Vector3 (0, Random.Range (0,360), 0);
    }
}
