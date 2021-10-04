using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroPlat : MonoBehaviour
{

    public CharacterControl cc;
    public CapsuleCollider2D player;
    public bool isElectrified;
    private SpriteRenderer sr;
    public Sprite neutralPlat;
    public Sprite electricPlat;
    public BoxCollider2D platform;
    private float warningTime = 2f;
    private float electricTime = 2.5f;
    private float cooldown = 4f;
    public FightView fv;
    private bool waiting;
    private bool warning;
    private bool electrify;
    private int seedNumber = 0;
    
    void Start()
    {
        isElectrified = false;
        waiting = false;
        warning = false;
        electrify = false;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isElectrified)
        {
            if(platform.IsTouching(player))
            {
                DisableCharacter();
            }
            electricTime -= Time.deltaTime;
            if (electricTime < 0)
            {
                NeutralizePlatform();
            }
        }
        if (waiting)
        {
            cooldown -= Time.deltaTime;
            if (cooldown <= 0)
            {
                waiting = false;
                cooldown = 4f;
            }
        }
        else if(!isElectrified)
        {
            CheckForElectricity();
        }
        if (warning)
        {
            ShowDanger();
        }
        if(electrify)
        {
            ElectrifyPlatform();
        }
    }

    public void DisableCharacter()
    {
        cc.SetDisabled(true);
    }

    public void ElectrifyPlatform()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = electricPlat;

        electrify = false;
        isElectrified = true;
    }
    public void NeutralizePlatform()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = neutralPlat;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        isElectrified = false;
        electricTime = 2.5f;
        waiting = true;
    }
    public void ShowDanger()
    {
        if (warningTime > 0)
        {
            warningTime -= Time.deltaTime;
            if (Mathf.FloorToInt(warningTime * 4) % 2 == 1)
            {
                sr.color = Color.red;
            }
            else
            {
                sr.color = Color.white;
            }
        }
        else 
        {
            warning = false;
            electrify = true;
            warningTime = 2f;
        }
    }
    private void CheckForElectricity()
    {
        seedNumber = fv.seed.Next(100);
        if (seedNumber >= 99)
        {
            warning = true;
        }
    }
}
