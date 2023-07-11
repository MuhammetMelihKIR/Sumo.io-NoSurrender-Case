using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SocialPlatforms;


public class FoodRotation : MonoBehaviour
{
    [SerializeField] float _rotateSpeed;

    void Update()
    {
        RotateFood();
    }

    void RotateFood()
    {
        transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);
        
    }

    
}
