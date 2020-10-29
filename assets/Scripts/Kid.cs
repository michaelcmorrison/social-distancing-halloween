using System;
using UnityEngine;
using UnityEngine.Events;

public class Kid : Person
{
    public float runSpeed;
    public float knockbackSpeed;
    public KidState state;
    
    [Serializable]
    public class UnityIntEvent : UnityEvent<int> {}
    public UnityIntEvent kidHitWall;
    public HitChain hitChain;
    
    private Vector2 _runDirection;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int WasHit = Animator.StringToHash("wasHit");

    public enum KidState
    {
        Idle,
        Running,
        Launched,
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _runDirection = ChooseRandomDirection();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case KidState.Idle:
                _animator.SetBool(IsWalking, false);
                return;
            case KidState.Launched:
                _animator.SetBool(IsWalking, false);
                _animator.SetBool(WasHit, true);
                FlyBackwards();
                break;
            case KidState.Running:
                _animator.SetBool(IsWalking, true);
                Run();
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.LogFormat("{0} hit {1}", gameObject.name, other.gameObject.name);
        
        if (other.gameObject.CompareTag("Ammunition"))
        {
            hitChain = new HitChain(this);
            state = KidState.Launched;
        }
        else if (other.gameObject.CompareTag("Kid"))
        {
            var otherKid = other.gameObject.GetComponent<Kid>();

            if (otherKid.hitChain != null)
            {
                hitChain = new HitChain(otherKid.hitChain.kids, this);
            }
            
            otherKid.state = KidState.Launched;
        }
        else if (other.gameObject.CompareTag("Wall")) 
        {
            if (state == KidState.Launched)
            {
                kidHitWall.Invoke(hitChain.kids.Count);
                Destroy(gameObject);    
            }
            else if (state == KidState.Running)
            {
                _runDirection = -_runDirection;
            }
        }
    }
    
    private void Run()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _runDirection * (runSpeed * Time.fixedDeltaTime));
    }

    private void FlyBackwards()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + Vector2.up * (knockbackSpeed * Time.fixedDeltaTime));
    }
}



