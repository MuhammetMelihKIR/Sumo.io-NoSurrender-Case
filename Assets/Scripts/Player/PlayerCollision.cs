using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.VisualScripting;
using UnityEngine.AI;

public class PlayerCollision : MonoBehaviour
{
 
    [SerializeField] private float _maxPlayerLevel;
    [SerializeField] private float _pushForce;
    private int _playerLevel;
    private int _levelForce;

    private Rigidbody _playerRb;
    private Sumo _playerScript;
    private PlayerMovement _playerMovementScript;
    
    
    

    private void Start()
    {
        _playerMovementScript= GetComponent<PlayerMovement>();
        _playerRb= GetComponent<Rigidbody>();
        _playerScript = GetComponent<Sumo>();
        _playerLevel = 0;
        _levelForce = 7;

    }

    private void OnTriggerEnter(Collider other)
    {
        
        FoodTrigger(other);
        EnemyTrigger(other);
        WaterTrigger(other);
    }
    private void WaterTrigger(Collider other)
    {
        if (other.CompareTag("water"))
            EventManager.OnGameOver();
    }
    private void FoodTrigger(Collider other)
    {
        if (other.CompareTag("Food"))
        {

            UIManager.Instance.ScoreIncrease(); //score increase
            UIManager.Instance.PlayerScoreAnim();
            ObjectPool.instance.gameObject.Equals(other.gameObject);
            ObjectPool.instance.NewTransform(other.gameObject);

            if (_playerLevel < _maxPlayerLevel) //After the set value, the player's level does not increase.
            {
                _playerLevel++;
                _levelForce +=2;
                _playerScript.PlayerScaleIncrease();
                _playerMovementScript.moveSpeed -= 0.4f;             
            }
   
        }
    }

    private void EnemyTrigger(Collider other)
    {
        Rigidbody otherRb = other.GetComponentInParent<Rigidbody>();


        if (other.CompareTag("Enemy"))
        {
            
            _playerMovementScript.canMove=false;
            _playerRb.AddForce(-transform.forward * _pushForce * 3, ForceMode.Impulse);
            StartCoroutine(CanMoveAfterForce());
      
            otherRb.AddForce(transform.forward * _pushForce *_levelForce/2, ForceMode.Impulse);
            otherRb.velocity =Vector3.zero;
            

        }
        else if (other.CompareTag("aim")) // weak point
        {
            
            _playerMovementScript.canMove = false;
            _playerRb.AddForce(-transform.forward * _pushForce *3, ForceMode.Impulse);
            StartCoroutine(CanMoveAfterForce());
           
            otherRb.AddForce(transform.forward * _pushForce *_levelForce, ForceMode.Impulse); // As the character level increases, our power to hit the weak point increases. "LEVEL FORCE"
            otherRb.velocity = Vector3.zero;

        }

    }

    IEnumerator CanMoveAfterForce()
    {
        yield return new WaitForSeconds(0.3f);
        _playerMovementScript.canMove = true;
        _playerRb.velocity= Vector3.zero;
    }
}
