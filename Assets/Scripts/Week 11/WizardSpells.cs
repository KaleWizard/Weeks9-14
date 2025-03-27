using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardSpells : MonoBehaviour
{
    public SpellMovement fireballPrefab;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void CastSpell()
    {
        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
    }
}
