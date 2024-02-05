using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour,ISaveable
{
    [Header("事件监听")]
    public VoidEventSO newGameEvent;

    [Header("基本属性")]
    public float maxHealth;
    public float currentHealth;
    public float maxPower;
    public float currentPower;
    public float powerRecoverSpeed;
    public float cost;

    [Header("受伤无敌")]
    public float invulnerableDuration;
    private float invulnerableCounter;
    public bool invulnerable;


    public UnityEvent<Character> OnHealthChange;

    public UnityEvent<Transform> OnTakeDamege;
    public UnityEvent OnDie;

    private void OnEnable()
    {
        ISaveable saveable=this;
        // saveable.RegisterSaveData();

        newGameEvent.OnEventRaised += NewGame;
    }
    private void OnDisable()
    {
        ISaveable saveable=this;
        // saveable.UnRegisterSaveData();

        newGameEvent.OnEventRaised -= NewGame;
    }


    private void NewGame()
    {
        currentHealth = maxHealth;
        currentPower = maxPower;
        OnHealthChange?.Invoke(this);
        
    }
    private void Update()
    {
        if (invulnerable)
        {
            invulnerableCounter -= Time.deltaTime;
            if(invulnerableCounter <= 0)
            {
                invulnerable = false;
            }
        }
        if (currentPower < maxPower)
        {
            currentPower += Time.deltaTime*powerRecoverSpeed;
        }
    }
    public void TakeDamage(Attack attacker)
    {
        if (invulnerable) { return; }
        if(currentHealth-attacker.damage > 0) {
            currentHealth -= attacker.damage;
            OnTakeDamege?.Invoke(attacker.transform);
            TriggerInvulnerable();
        }
        else
        {
            currentHealth = 0;
            //die
            OnDie?.Invoke();
        }
        OnHealthChange?.Invoke(this);
    }

    private void TriggerInvulnerable()
    {
        if (!invulnerable) 
        {
            invulnerable = true;
            invulnerableCounter = invulnerableDuration;
        }
    }
    public void OnDash(int cost)
    {
        currentPower-=cost;
        OnHealthChange?.Invoke(this);
    }

    public DataDefination GetDataID()
    {
        return GetComponent<DataDefination>();
    }

    public void GetSaveDate(Data data)
    {
        if(data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            data.characterPosDict[GetDataID().ID]=transform.position;
        }
        else
        {
            data.characterPosDict.Add(GetDataID().ID, transform.position);
        }
    }

    public void LoadData(Data data)
    {

        if (data.characterPosDict.ContainsKey(GetDataID().ID))
        {
            transform.position = data.characterPosDict[GetDataID().ID];
        }
    }
}
