using System.Collections;//we dont need this we can delete it
using System.Collections.Generic;//we dont need this we can delete it
using UnityEngine;//unity engine is highlighted because where using monoBehaviour
using UnityEngine.SceneManagement;
public class CollisionHandling : MonoBehaviour
{
    [SerializeField]
    float levelDelay;//allows us to parameteise the invoke second delay.

    [SerializeField]
    AudioClip startCrashAudio;

    [SerializeField]
    AudioClip successSound;

    [SerializeField]
    ParticleSystem successPE;

    [SerializeField]
    ParticleSystem explosionPE;

    AudioSource aS;

    ParticleSystem pS;

  //  BoxCollider bC;

    bool isTransitioning = false;//for turning off things when we bumb into something so things dont run while were tranistioning.
    bool collisionDisable = false;//allows for toggling of the collision key

     void Start()
    {
        aS = GetComponent<AudioSource>();
        pS = GetComponent<ParticleSystem>();
    //    bC = GetComponent<BoxCollider>();
    }

     void Update()//re-adding the update for cheat keys bcause we need to check every frame if the keys are being pressed.
    {
        KeyCheatCodes();
    }

    void KeyCheatCodes()
    {
        if(Input.GetKeyDown(KeyCode.L))//cheching for when thekey is pressed down
        {
            Debug.Log("next level loaded");
            LoadingNextlevel();      
        }

       else if (Input.GetKeyDown(KeyCode.C))//remember else if is otherwise.
        {
            Debug.Log("collision disabled");
            collisionDisable = !collisionDisable;//this will toggle collision
           // bC.enabled = false;//disables collision one way

            //else if (GetComponent<BoxCollider>().enabled == false) to try re-enable collision 
            // {
            //     if (Input.GetKey(KeyCode.C))
            //     {
            //         Debug.Log("collision on");
            //         GetComponent<BoxCollider>().enabled = true;
            //     }
            // }
        }
    }

    void OnCollisionEnter(Collision col)
    {
        //we can also do the if statment below as:
        //if (isTransitioning)
        //{
        //    return;
        //}
        //if (isTransitioning) { return; } means: If isTransitioning is true, terminate the method immediately and do NOT execute the rest of the code block, here: the switch. That’s all.
        //the impication for doing this way is else or otherwise do all the code down here.

        if (isTransitioning || collisionDisable)// this means if tranistioning is false or use ! operator which will mean true in this case. then we have a or collisionDisable is in this case equal to false. 
        {
            return;//If isTransitioning is true, terminate the method immediately and do NOT execute the rest of the code block, here: the switch. That’s all.
                   //the impication for doing this way is else or otherwise do all the code down here.
        }

        {
            switch (col.gameObject.tag)
            {
                case "Friendly":
                    print("your hitting a friendly");
                    break;

                case "Finish":
                    LoadingNextlevel();
                    break;

                default://since the default is triggered when we touch a unmarked tag it is a catch all saftey net.                    
                    StartingCrashSequence();//reload the level after 1 second delay. If you have the invoke method instead of startingcrashsequence,
                    break;                      //what will happen is that the game will wait however many seconds then disable the script. so invoke after disabling the script

            }
        }
    }

    void StartingCrashSequence()
    {
            isTransitioning = true;
            aS.Stop();//stops all of our sound effects from playing. used to stop sound effects from playing when we crash.
            aS.PlayOneShot(startCrashAudio, 0.75f);
            explosionPE.Play();        
            GetComponent<Movement>().enabled = false;
            Invoke("restartScene", levelDelay);
    
    }

    void LoadingNextlevel()
    {
            isTransitioning = true;
            aS.Stop();//stops all of our sound effects from playing. used to stop sound effects from playing when we crash.
            aS.PlayOneShot(successSound, 0.75f);
            successPE.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", levelDelay); 
    }

    void restartScene()
    {
          int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//using a int because buildIndex is a integar.
           SceneManager.LoadScene(currentSceneIndex);//shortened down version of line 36, 
        //loads the current scene index but adds 1 to the scene so it loads the next level

        // SceneManager.LoadScene(0);// or  SceneManager.LoadScene("SandBox");using build index

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //what this code does is returns the index of the scene in the build settings that we are currently on get active scene, the one we're currently on.
    }

   public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;//using a int because buildIndex is a integar.
        int nextSceneIndex = currentSceneIndex + 1;//

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)//sceneCountInBuildSettings gets the number of scenes in the build settings.currently the maxium scene count is 2.
            //the if statement means if the nextSceneIndex is the same as the number that we have, like the total of scenes, then we run the code below.
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
        //THINK OF THE LOGIC OF THIS METHOD NUMERICALLY 
    }
    //what this code does is say if were on our second scene that means are current scene index is going to equal one, our nextSceneIndex is going to equal one + 1 which is two.
    //then in the if statment if two is equal to the same as two because SceneManager.sceneCountInBuildSettings is going to be the total number of scenes that we've got in our game,
    //which is two, then our next scene index is actually going to be zero.
    //which is going to change at this point because we assigned a value nextSceneIndex

  
}
