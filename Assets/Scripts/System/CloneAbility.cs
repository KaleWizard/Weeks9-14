using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAbility : MonoBehaviour
{
    AbilityCooldown cooldown;

    // Called on scene load
    public void Initialize(AbilityCooldown c)
    {
        cooldown = c;
    }

    // Called upon ability selected
    public void Activate()
    {

    }

    // Called upon ability action used
    public void Use()
    {

    }

    // Called on cooldown finished
    public void Refresh()
    {

    }

    // Called when ability deselected
    public void Deactivate()
    {

    }
}
