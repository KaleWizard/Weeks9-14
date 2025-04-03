using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class AbilityController : MonoBehaviour
{
    // Ability value constants, corresponding to dropdown choices
    const int NONE = 0;
    const int REWIND = 1;
    const int CLONE = 2;
    const int BOMBER = 3;

    // Ability Classes
    RewindAbility rewind;
    CloneAbility clone;
    BomberAbility bomber;

    // Ability cooldown timer
    AbilityCooldown cooldown;

    // Ability select dropdown
    public TMP_Dropdown abilityDropdown;

    public UnityEvent OnAbilityUsed;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = GetComponent<AbilityCooldown>();

        // Retrieve each ability class
        rewind = GetComponent<RewindAbility>();
        clone = GetComponent<CloneAbility>();
        bomber = GetComponent<BomberAbility>();

        // Initialize each ability class
        rewind.Initialize(cooldown);
        clone.Initialize(cooldown);
        bomber.Initialize(cooldown);

        // On dropdown's value changed, update the ability selection
        abilityDropdown.onValueChanged.AddListener(UpdateAbility);
    }

    void Update()
    {
        // If Ability action is pressed and no cooldown is active
        if (Input.GetKeyDown(KeyCode.R) && !cooldown.IsActive())
        {
            OnAbilityUsed.Invoke();
        }
    }

    public void UpdateAbility(int value)
    {
        // End the cooldown timer
        cooldown.EndTimer();

        // Remove listeners from Ability Used and Cooldown Finished events
        OnAbilityUsed.RemoveAllListeners();
        cooldown.OnCooldownFinished.RemoveAllListeners();

        // Deactivate all abilities
        rewind.Deactivate();
        clone.Deactivate();
        bomber.Deactivate();

        // For the new value of the dropdown:
        //  - Activate it
        //  - Add its "Use" method to the Ability Used event
        //  - Add its "Refresh" method to the cooldown timer's Cooldown Finished event
        switch(value)
        {
            case REWIND:
                rewind.Activate();
                OnAbilityUsed.AddListener(rewind.Use);
                cooldown.OnCooldownFinished.AddListener(rewind.Refresh);
                break;

            case CLONE:
                clone.Activate();
                OnAbilityUsed.AddListener(clone.Use);
                cooldown.OnCooldownFinished.AddListener(clone.Refresh);
                break;

            case BOMBER:
                bomber.Activate();
                OnAbilityUsed.AddListener(bomber.Use);
                cooldown.OnCooldownFinished.AddListener(bomber.Refresh);
                break;
        }
    }
}
