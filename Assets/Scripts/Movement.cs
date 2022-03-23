using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{//the order of which we should arrange are code
    [SerializeField]//1st parameters
    float fowardSpeed = 0;

    [SerializeField]
    float rotationSpeed = 0;

    [SerializeField]
    AudioClip rocketEngine;

    [SerializeField]
    ParticleSystem rocketPE;

    [SerializeField]
    ParticleSystem sideRocketLeftPE;

    [SerializeField]
    ParticleSystem sideRocketRightPE;


    Rigidbody rb;//2nd cache

    AudioSource aS;

    CollisionHandling cH;



    //bool is alive, STATE example

    //we structured the top variables as:

    //PARAMETERS- used for tunning to the editor typically.
    //CACHE- for speed or readability example.
    //STATE private instance (member) variables.
   
    void Start()//2nd start/update methods
    {
        rb = GetComponent<Rigidbody>();
        aS = GetComponent<AudioSource>();
        cH = GetComponent<CollisionHandling>();
      
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        processRoation();
    }
  

    void ProcessThrust()//3rd any custom methods
    {
        if (Input.GetKey(KeyCode.Space))
        {
            processingThrustInput();
        }

        else if (!Input.GetKey(KeyCode.Space))
        {
            processThrustSounds();
        }
        //we could also just use else else{ aS.Stop}
    }

    void processRoation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            startingRotationInputA();
        }

        else if (Input.GetKey(KeyCode.D))//we use else if to stop the player from moving in two direction.   //else in english can mean otherwise. e.g.otherwise if this is true do that
        {
            startingRoationD();
        }

        else//using a else statment to say if your not doing the two if statements above then do the line of code at the bottom 
        {
            sideRocketRightPE.Stop();//stops the right particle effect
            sideRocketLeftPE.Stop();
        }
    }

    void startingRotationInputA()
    {
        //transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        //same as writing transform.Rotate(0,0,1 * speed * Time.deltaTime);
        sideRocketLeftPE.Play();//plays the particle effect
        applyRotation(rotationSpeed);//we pass in  rotation speed to move left
        if (!sideRocketRightPE.isPlaying)
        {
            sideRocketRightPE.Play();//plays the particle effect
        }
    }

    void startingRoationD()
    {
        //transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
        //same as writing transform.Rotate(0,0,-1);
        sideRocketRightPE.Play();
        applyRotation(-rotationSpeed);//we pass in minus rotation speed to move right

        if (!sideRocketLeftPE.isPlaying)
        {
            sideRocketLeftPE.Play();//plays the particle effect
        }
    }

    void processThrustSounds()
    {
        aS.Stop();
        rocketPE.Stop();//stops the particle effect
    }

    void processingThrustInput()
    {
        // rb.AddRelativeForce(0,10,0 * speed * Time.deltaTime);
        //this code means were adding force to the relitive position of 1 on the x,y and z.

        //we can also write the following code like this:
        //rb.AddRelativeForce(Vector3.up);//same as writing rb.AddRelativeForce(0,1,0);

        rb.AddRelativeForce(Vector3.up * fowardSpeed * Time.deltaTime);

        if (!aS.isPlaying)//stops the audio source from playing muLtiple times when we press the space
        {
            //aS.Play();//good for only playing one clip
            aS.PlayOneShot(rocketEngine, 0.75f);//good for playing multiple clips.   

        }

        if (!rocketPE.isPlaying)
        {
            rocketPE.Play();//plays the particle effect
        }
    }

    void applyRotation(float rotationThisFrame)//we are creating parameters so we can add addition information.
    {
        rb.freezeRotation = true; //freezing rotation so we can manually rotate

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        //we pass in rotationThisFrame so we can pass in rotationSpeed as a parameter

        rb.freezeRotation = false;//unfreezing rotation so the physics system can take over
    }

    void CheatCLoadNextLevel()
    {
        cH.LoadNextLevel();//loads the next level
    }
}
