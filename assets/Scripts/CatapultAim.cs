using UnityEngine;

public class CatapultAim : MonoBehaviour
{
    private Camera _camera;
    private Vector3 _direction;
    private void Awake()
    {
        _camera = Camera.main;
    }
    
    private void Update()
    {
        var mousePos = Input.mousePosition;
        var thisPos = _camera.WorldToScreenPoint(transform.position);

        _direction = mousePos - thisPos;
        
        var angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    public Vector3 GetDirection()
    {
        return _direction.normalized;
    }
}
