using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class AbilityController : MonoBehaviour
{
    const int NONE = 0;
    const int REWIND = 1;
    const int CLONE = 2;
    const int BOMBER = 3;

    RewindAbility rewind;
    CloneAbility clone;
    BomberAbility bomber;

    AbilityCooldown cooldown;

    public TMP_Dropdown abilityDropdown;

    public UnityEvent OnAbilityUsed;

    // Start is called before the first frame update
    void Start()
    {
        cooldown = GetComponent<AbilityCooldown>();

        rewind = GetComponent<RewindAbility>();
        clone = GetComponent<CloneAbility>();
        bomber = GetComponent<BomberAbility>();

        rewind.Initialize(cooldown);
        clone.Initialize(cooldown);
        bomber.Initialize(cooldown);

        abilityDropdown.onValueChanged.AddListener(UpdateAbility);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (cooldown.IsActive()) return;
            OnAbilityUsed.Invoke();
        }
    }

    public void UpdateAbility(int value)
    {
        cooldown.EndTimer();

        OnAbilityUsed.RemoveAllListeners();
        cooldown.OnCooldownFinished.RemoveAllListeners();

        rewind.Deactivate();
        clone.Deactivate();
        bomber.Deactivate();

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
