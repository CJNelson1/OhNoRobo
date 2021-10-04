using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPlat : MonoBehaviour
{

    public CharacterControl cc;
    public bool isElectrified;
    public Sprite neutralPlat;
    public Sprite electricPlat;
    public BoxCollider2D shockArea;
    
    void Start()
    {
        isElectrified = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isElectrified)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = electricPlat;
            shockArea.enabled = true;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = neutralPlat;
            shockArea.enabled = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        // When target is hit
        if (other.gameObject.tag == "Player" && isElectrified)
        {
            DisableCharacter();
        }
    }

    public void DisableCharacter()
    {
        cc.SetDisabled(true);
    }

    public void ElectrifyPlatform()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
        isElectrified = true;
    }
}
