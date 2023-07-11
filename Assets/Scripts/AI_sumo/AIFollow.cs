using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class AIFollow: MonoBehaviour
{
    [SerializeField] private float _followSpeed;
    private Transform _target;
    public GameObject mainTarget;
    public static bool on;

    
    private void Start()
    {   
        on= true;
    }
    
    private void  Searching()
    {

        GameObject enemy = mainTarget;
        {
            _target = enemy.transform;
        }
    }
    private void Update()
    {
         Searching();

        if (_target != null && on && UIManager.Instance.startGame==true)
        {
            
            Vector3 dir = _target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _followSpeed * Time.deltaTime);

            
            transform.position += transform.forward * _followSpeed * Time.deltaTime;
        }
    }
}