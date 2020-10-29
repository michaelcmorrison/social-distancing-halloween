using UnityEngine;

public class Kid : Person
{
    public float knockbackSpeed;
    public KidState state;
    
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

    private void OnEnable()
    {
        Spawner.Kids.Add(this);
    }

    private void OnDisable()
    {
        Spawner.Kids.Remove(this);
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
        if (other.gameObject.CompareTag("Ammunition"))
        {
            state = KidState.Launched;
        }
        else if (other.gameObject.CompareTag("Kid"))
        {
            var otherKid = other.gameObject.GetComponent<Kid>();

            if (otherKid.state != KidState.Launched)
            {
                _runDirection = ChooseRandomDirection();
            }
            else
            {
                state = KidState.Launched;
            }
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            switch (state)
            {
                case KidState.Launched:
                    GameManager.Instance.AddScore();
                    Destroy(gameObject);
                    break;
                case KidState.Running:
                    _runDirection = -_runDirection;
                    break;
            }
        }
    }
    
    private void Run()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + _runDirection * (moveSpeed * Time.fixedDeltaTime));
    }

    private void FlyBackwards()
    {
        _rigidbody2D.MovePosition(_rigidbody2D.position + Vector2.up * (knockbackSpeed * Time.fixedDeltaTime));
    }
}



