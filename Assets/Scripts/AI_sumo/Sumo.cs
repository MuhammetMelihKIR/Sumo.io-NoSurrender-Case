using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sumo : MonoBehaviour
{
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int   _vibrato;
    [SerializeField] private float _randomness;

    Vector3 _sumoScale;
    public void PlayerScaleIncrease()
    {
       
        transform.DOShakeScale(_duration,_strength,_vibrato,_randomness); //Player Scale Animation

        // Player  increase in size when his eat food
        _sumoScale = transform.localScale;
        _sumoScale.x += 0.5f;                
        _sumoScale.y += 0.5f;
        _sumoScale.z += 0.5f;
        transform.localScale = _sumoScale;    

    }

   
}
