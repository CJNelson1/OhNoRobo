using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapView : MonoBehaviour
{
    private System.Random seed;
    public static MapView mapInstance;
    public bool headOk;
    public bool leftArmOk;
    public bool rightArmOk;

    // This is a persistant manager so we're using awake instead of start
    void Awake()
    {
        // I can't remember why we had to do all this nonsense, but this is all from VikingManager 
        if (!mapInstance)
        {
            mapInstance = this;
            seed = new System.Random();

            headOk = true;
            leftArmOk = true;
            rightArmOk = true;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
