using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tech : MonoBehaviour
{
    bool forestry = false;
    bool mining = false;
    bool farming = false;
    bool hunting = false;

    void Start()
    {

    }

    void Update()
    {

    }

    public bool HaveTech(string techNeeded)
    {
        if(techNeeded == "forestry")
        {
            return forestry;
        }       
        else if (techNeeded == "mining")
        {
            return mining;
        }
        else if (techNeeded == "farming")
        {
            return farming;
        }
        else if (techNeeded == "hunting")
        {
            return hunting;
        }
        else
        {
            return false;
        }
    }

    public bool GetForestry()
    {
        return forestry;
    }

    public void SetForestry(bool set)
    {
        forestry = set;
    }

    public bool GetMining()
    {
        return mining;
    }
    public void SetMining(bool set)
    {
        mining = set;
    }

    public bool GetFarming()
    {
        return farming;
    }
    public void SetFarming(bool set)
    {
        farming = set;
    }

    public bool GetHunting()
    {
        return hunting;
    }
    public void SetHunting(bool set)
    {
        hunting = set;
    }
}
