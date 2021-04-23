using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public enum AttacType
{
    heavy = 0,
    light = 1,
    kick = 2
};
public class FightingCombo : MonoBehaviour
{
    // [Header("Inputs")]
    // public KeyCode heavyKey;
    // public KeyCode lightKey;
    // public KeyCode kickKey;
    private JumpBehaviour _jumpBehaviour;
    private Actor _player;
    [Header("Attacks")] 
    public Attack heavyAttack;
    public Attack lightAttack;
    //public Attack kickAttack;
    public List<Combo> combos;
    public float comboLeeway = 0.2f;

    [Header("Components")] 
    Animator ani;

    private ComboInput lastInput=null;  
    List<int>currentCombos=new List<int>();
    private Attack curAttack=null;
    [HideInInspector]
    public ComboInput input = null;
    
    private float timer = 0;
    private float leeway = 0;
    private bool skip = false;

    private bool _heavyAttack;
    private bool _lightAttack;
    void Start()
    {
        _jumpBehaviour = GetComponent<JumpBehaviour>();
        _player = GetComponentInParent<Actor>();
        ani = GetComponent<Animator>();
        PrimeCombos();
    }
    void Update()
    {
        if (curAttack != null)
        {
            if (timer > 0)
                timer -= Time.deltaTime;
            else 
                curAttack = null;
            return;
        }
        if (currentCombos.Count > 0)
        {
            leeway += Time.deltaTime;
            if (leeway >= comboLeeway)
            {
                if (lastInput != null)
                {
                    
                    //Attack(getAttackFromType(lastInput.type));
                    lastInput = null;
                }

                ClearAll();
            }
        }
        else leeway = 0;

         input = null;

         if (_heavyAttack&&!skip)
         {
             input = new ComboInput(AttacType.heavy); 
             Attack(getAttackFromType(input.type));
             //_player.Attack(1);
             _heavyAttack = false;
         }
         if (_lightAttack&&!skip)
         {
             input = new ComboInput(AttacType.light); 
             Attack(getAttackFromType(input.type));
             //_player.Attack(1);
             _lightAttack = false;
         }
         // if (Input.GetKey(heavyKey)&&!skip)
        // {
        //     input = new ComboInput(AttacType.heavy);
        //     Attack(getAttackFromType(input.type));
        // }
        //
        // if (Input.GetKeyDown(lightKey)&&!skip)
        // {
        //     input = new ComboInput(AttacType.light);
        //     Attack(getAttackFromType(input.type));
        // }
        //
        // if (Input.GetKeyDown(kickKey)&&!skip)
        // {
        //     input = new ComboInput(AttacType.kick);
        //     Attack(getAttackFromType(input.type));
        // }

        if (input == null) return;
        lastInput = input;
        for (int i = 0; i < currentCombos.Count; i++)
        {
            //print("attack");
            Combo c = combos[currentCombos[i]];
            if (c.continueCombo(input))
            {
                leeway = 0;
            }
        }
        
        if (skip)
        {
            skip = false;
            return;
        }

        for (int i = 0 ; i < combos.Count; i++)
        {
            if (currentCombos.Contains(i)) continue;
            if (combos[i].continueCombo(input))
            {
                currentCombos.Add(i);
                //print(currentCombos.Count);
                //Attack(getAttackFromType(input.type));
                leeway = 0;
            }
        }
        
        // foreach (int i in remove)
        // {
        //     currentCombos.RemoveAt(i);
        // }
        // if(currentCombos.Count<=0)
        //     Attack(getAttackFromType(input.type));
    }

    // void ResetCombos()
    // {
    //     bool check=false;//
    //     leeway = 0;
    //     for (int i = 0; i < combos.Count; i++)//добавь кюрент комбос
    //     {
    //         Combo c = combos[i];
    //         if (c.check == true)
    //         {
    //             check = true;
    //             break;
    //         }
    //         c.resetCombo();
    //     }
    //     if (check == false)//
    //     {
    //         currentCombos.Clear();
    //     }
    // }


    public void OneAttack(int i=1)
    {
        if (_jumpBehaviour.JumpState) return;
        if(timer<=0&&!_player._takeDamage) _heavyAttack = true;
        else ClearAll();
    }

    public void  TwoAttack()
    {
        if(timer<=0&&!_player._takeDamage)_lightAttack = true;
        else ClearAll();
    }

    void ClearAll()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.resetCombo();
        }
        currentCombos.Clear();
    }

    // private void OnEnable()
    // {
    //     var button = GameObject.FindGameObjectWithTag("Button1");
    //     button.GetComponent<AttackButton>()._buttonDown += OneAttack;
    // }

    private void Attack(Attack att)
    {
        curAttack = att;
        timer = att.length;
        ani.Play(att.name,-1,0);
    }

    Attack getAttackFromType(AttacType t)
    {
        if (t == AttacType.heavy) return heavyAttack;
        if (t == AttacType.light) return lightAttack; 
        //if (t == AttacType.kick) return kickAttack;
        return null;
    }

    void PrimeCombos()
    {
        for (int i = 0; i < combos.Count; i++)
        {
            Combo c = combos[i];
            c.onInputed.AddListener(() =>
            {
                skip = true;
                Attack(c.comboAttack);
                if (c.inputs.Count == 4) ClearAll();
                // _player.Attack(c.comboAttack.damage);
                //ResetCombos();
            });
        }
    }
}
[System.Serializable]
public class Attack
{
    public string name;
    public float length;
    //public int damage;
}

[System.Serializable]
public class ComboInput
{
    public AttacType type;

    public ComboInput(AttacType t)
    {
        type = t;
    }

    public bool isSameAs(ComboInput test)
    {
        return (type == test.type);
    }
}

[System.Serializable]
public class Combo
{
    public List<ComboInput> inputs;
    public Attack comboAttack;
    public UnityEvent onInputed;
    private int curInput=0;
    //public bool check = false;
    
    public bool continueCombo(ComboInput i)
    {
        if (inputs[curInput].isSameAs(i))
        {
            curInput++;
            //check = true;
            if (curInput == inputs.Count)
            {
                //check = false;
                onInputed.Invoke();
                curInput = 0;
            }

            return true;
        }
        else
        {
            //check = false;
            curInput = 0;
            return false;
        }
    }

    public ComboInput currentComboInput()
    {
        if (curInput >= inputs.Count) return null;
        return inputs[curInput];
    }

    public void resetCombo()
    {
        //check = false;
        curInput = 0;
    }
}
