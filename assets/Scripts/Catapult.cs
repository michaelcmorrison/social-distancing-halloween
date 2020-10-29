using Unity.Mathematics;
using UnityEngine;

public class Catapult : MonoBehaviour
{
    public Rigidbody2D ammo;
    public float fireRate;
    public float power;
    public Sprite loaded;
    public Sprite launched;
    public SpriteRenderer ammoSprite;
    [HideInInspector] public bool isLoaded;
    private Vector2 Direction => _catapultAim.GetDirection();
    private bool CanFire => Time.time > _shotRefresh;
    private float _shotRefresh;

    private AmmoLoader _loader;
    private CatapultAim _catapultAim;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _catapultAim = GetComponent<CatapultAim>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _loader = GetComponent<AmmoLoader>();
    }

    private void Update()
    {
        if (CanFire && !isLoaded)
        {
            Load(_loader.GetRandomAmmunition());
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanFire)
        {
            Launch();
        }
    }

    private void Launch()
    {
        isLoaded = false;
        _spriteRenderer.sprite = launched;
        ammoSprite.enabled = false;
        _shotRefresh = Time.time + fireRate;
        var ammoCopy = Instantiate(ammo, transform.position, quaternion.identity);
        ammoCopy.AddForce(Direction * power);
    }

    private void Load(Rigidbody2D ammunition)
    {
        isLoaded = true;
        _spriteRenderer.sprite = loaded;
        ammo = ammunition;
        var sprite = ammunition.GetComponent<SpriteRenderer>().sprite;
        ammoSprite.enabled = true;
        ammoSprite.sprite = sprite;
    }
}