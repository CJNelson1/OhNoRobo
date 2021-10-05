using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    private System.Random seed;
    public static MapView mapInstance;
    public static GameObject toggleBoxContainer;

    // Head stuff
    [SerializeField] public bool HeadBroken;

    // Left arm stuff
    [SerializeField] public bool LeftArmBroken;

    // Right arm stuff
    [SerializeField] public bool RightArmBroken;
    // [SerializeField] public int gearSpeed1;
    // [SerializeField] public int gearSpeed2;
    // [SerializeField] public int gearSpeed3;

    // This is a persistant manager so we're using awake instead of start
    void Awake()
    {
        // I can't remember why we had to do all this nonsense, but this is all from VikingManager 
        if (!mapInstance)
        {
            mapInstance = this;
            seed = new System.Random();

            toggleBoxContainer = GameObject.Find("ToggleBoxContainer");

            HeadBroken = false;
            LeftArmBroken = false;
            RightArmBroken = false;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Send updates to relevant objects

        if (HeadBroken)
        {
            // Check if head is fixed yet
            bool anyBad = false;

            // See if any toggleboxes are bad
            // ToggleBox[] toggleBoxes = toggleBoxContainer.GetComponentsInChildren<ToggleBox>();
            // foreach (ToggleBox toggleBox in toggleBoxes)
            // {
            //     if (toggleBox.bad) 
            //     {
            //         anyBad = true;
            //         break;
            //     }
            // }

            HeadBroken = anyBad;

            if (!HeadBroken)
            {
                // Update map cuz head is fixed now
                Debug.Log("Head was just fixed");
            }
        }

        if (LeftArmBroken)
        {
            // Check if left arm is fixed yet
        }

        if (RightArmBroken)
        {
            // Check if right arm is fixed yet
        }
    }

    public void makeHeadUnstable()
    {
        Debug.Log("Test instability function. Initial headbroken value is " + HeadBroken);

        if (!HeadBroken)
        {
            HeadBroken = true;

            // Update map
            // TODO

            // Instead of breaking trampolines, we're gonna make the TVs bad
            // ToggleBox[] toggleBoxes = toggleBoxContainer.GetComponentsInChildren<ToggleBox>();
            // foreach (ToggleBox toggleBox in toggleBoxes)
            // {
            //     toggleBox.makeBad();
            // }
        }
    }

    public void makeLeftArmUnstable()
    {
        makeHeadUnstable(); // Just for testing right now

        if (!LeftArmBroken)
        {
            LeftArmBroken = true;

            // Update map
            // TODO

            // RoketArm - add 2 enemy dudes on few zappies, all at once - who let the dogs out?
        }
    }

    public void makeRightArmUnstable()
    {
        makeHeadUnstable(); // Just for testing right now

        if (!RightArmBroken)
        {
            RightArmBroken = true;

            // Update map
            // TODO

            // PunchArm - make gears rotate faster (make them go coo-coo randomly)
            // int gearSpeed1 = 0, gearSpeed2 = 0, gearSpeed3 = 0;
            
            // for (int i = 0; i <= 10; i++)  //this loop should go on as long as the player has the time left, temp=10, speeds to be changed as well.
            // {  
            //     gearSpeed1 = seed.Next(1,4);
            //     gearSpeed2 = seed.Next(1,4);
            //     gearSpeed3 = seed.Next(1,4);
            //     //make few "crazy" gears run on these speeds ?? Not sure how
            // }
        }
    }
}
