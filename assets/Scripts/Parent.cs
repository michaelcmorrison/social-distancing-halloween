using System;
using UnityEngine;

public class Parent : Person
{
    public ParentState state;

    private Vector2 _walkDirection;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private static readonly int IsAngry = Animator.StringToHash("isAngry");
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsDrinking = Animator.StringToHash("isDrinking");

    public enum ParentState
    {
        Idle,
        Angry,
        Walking,
        Drinking
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Spawner.Parents.Add(this);
    }

    private void OnDisable()
    {
        Spawner.Parents.Remove(this);
    }
    
    private void Start()
    {
        _walkDirection = ChooseRandomDirection();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case ParentState.Idle:
                _animator.SetBool(IsWalking, false);
                return;
            case ParentState.Angry:
                _animator.SetBool(IsWalking, false);
                _animator.SetBool(IsAngry, true);
                break;
            case ParentState.Walking:
                _animator.SetBool(IsAngry, false);
                _animator.SetBool(IsWalking, true);
                Walk();
                break;
            case ParentState.Drinking:
                _animator.SetBool(IsDrinking, true);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Drink()
    {
        Destroy(gameObject);
    }

    private void Walk()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _walkDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ammunition"))
        {
            state = other.gameObject.GetComponent<Ammunition>().isAlcohol ? ParentState.Drinking : ParentState.Angry;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            if (state == ParentState.Walking)
            {
                _walkDirection = -_walkDirection;
            }
        }
    }
}


