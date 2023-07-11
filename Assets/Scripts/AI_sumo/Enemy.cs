using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;


[RequireComponent(typeof(Rigidbody))]
public class Enemy :Sumo
{
    [SerializeField] private int _pushForce;
    public GameObject target;
    private Rigidbody rb;   
    private int _levelForce;
    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }


    private void OnTriggerEnter(Collider other)
    {
        EnemyDie(other);
        EnemyTriggerToEnemy(other);
        EnemyFoodTrigger(other);
    }

   
    public void EnemyTriggerToEnemy(Collider other)
    {

        if (other.CompareTag("Enemy"))
        {  
            rb.AddForce(-transform.forward * _pushForce * 3, ForceMode.Impulse);
  
        }

    }

    public void EnemyFoodTrigger(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            ObjectPool.instance.gameObject.Equals(other.gameObject);
            ObjectPool.instance.NewTransform(other.gameObject);
            _levelForce++;
            PlayerScaleIncrease();
 

        }
    }
    public void EnemyDie(Collider other)
    {
        if (other.gameObject.CompareTag("water"))
        {
            gameObject.SetActive(false);
            UIManager.Instance.EnemyValue();
        }
        
    }

   
}

