using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Osulator : MonoBehaviour
{
    Vector3 startingPos;

    [SerializeField]
    Vector3 movementVector;

    [SerializeField]
    float period = 2f;

    // [SerializeField]
    //[Range(0,1)] float movementFactor;// [Range(0,1)] creates a range slider(attribute) between 0 to 1
    float movementFactor;

    void Start()
    {
        startingPos = transform.position;//current postion is assigned to startingPos
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) //math epsilon divides by the lowest number possible
        {
            return; 
        }
        
            const float tau = Mathf.PI * 2; //const means constant. It is a constant varaible which means it doesn't change. constant value of 6.283
            float cycles = Time.time / period;//create a mechanisms for mesuring time. Time.time means how much time has elapsed. so this code means if e.g. there are 10 cycles we divide it by e.g. 2 then we would of had 5 cycles come and gone thus far. in other words continually growing over time
            float rawSinWave = Mathf.Sin(cycles * tau);//tau represents a entire circle it is 6.28. going from -1 to 1

            movementFactor = (rawSinWave + 1f) / 2f;//we want to go from 0 to 1. we do that by adding on 1, this means instead of going from minus one to one, we're going from zero to two then we wrap it in paraemthesis and divide it by two to give us a nice clean value of zero to one. recalculated to go from 0 to 1 so its cleaner. if we dont have this then are starting point will be the mid point which is what we will be fnding if we did the minus one to one.

            Vector3 offset = movementVector * movementFactor; //muliples the movement vector which is a vector3 and the vector 3 is determined by the input in the console, by movement factor. then reassigns the total value to offset
            transform.position = startingPos + offset;// the starting postion is added to the offset then reassigned to the transform.position which is the current position.
        
        }
}
