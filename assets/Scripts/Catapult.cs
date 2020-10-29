using System;
using Unity.Mathematics;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public Rigidbody2D ammo;
    public float fireRate;
    public float power;
    public Sprite loaded;
    public Sprite launched;
    private Vector2 Direction => _catapultAim.GetDirection();
    private bool CanFire => Time.time > _shotRefresh;
    private float _shotRefresh;
    
    private CatapultAim _catapultAim;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _catapultAim = GetComponent<CatapultAim>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (CanFire)
        {
            _spriteRenderer.sprite = loaded;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) && CanFire)
        {
            Launch();
        }
    }

    private void Launch()
    {
        _shotRefresh = Time.time + fireRate;
        _spriteRenderer.sprite = launched;
        var ammoCopy = Instantiate(ammo, transform.position, quaternion.identity);
        ammoCopy.AddForce(Direction * power);
    }
}