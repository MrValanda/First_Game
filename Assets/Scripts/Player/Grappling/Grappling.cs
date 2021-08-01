
using UnityEngine;

public class Grappling : MonoBehaviour
{

    [SerializeField] private float speed;
   [SerializeField] private LayerMask whatIsGrappleable;
   [SerializeField] private Transform gunTip, camera;
   [SerializeField] private int countGrappling;
   [SerializeField] private float alignGrap;
   [SerializeField] private AudioSource grappleSound;

   private Transform _transform;
    private LineRenderer _lr;
    private Vector3 _grapplePoint;
    private float maxDistance = 100f;
    private bool _isGrappling;
    private PlayerController _playerController;
    private int _currentNumberHooks;

    public int CurrentNumberHooks => _currentNumberHooks;
    
    void Awake() {
        _lr = GetComponent<LineRenderer>();
        _transform = GetComponent<Transform>();
        _playerController = GetComponent<PlayerController>();
    }

    void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            if (CurrentNumberHooks < countGrappling)
                StartGrapple();
        }
        else if (Input.GetMouseButton(0))
        {
            Grapple();
        }
        else if (Input.GetMouseButtonUp(0)) {
            StopGrapple();
        }

        if (!IsGrappling() && _playerController.OnGround)
        {
            _currentNumberHooks = 0;
            _resetGrap = true;
        }
    }
    
    void LateUpdate() {
        DrawRope();
    }

    void Grapple()
    {
        if (_isGrappling)
        {
            Vector3 hookDirection =(_grapplePoint - _transform.position);

            if (hookDirection.magnitude < 2)
            {
                StopGrapple();
            }
            _playerController.Velocity =Vector3.Lerp( _playerController.Velocity,hookDirection.normalized * speed,Time.deltaTime*alignGrap);
        }
    }

    private bool _resetGrap;
    
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grappleSound.Play();
            if(hit.transform.CompareTag("ResetGrap"))
            {
                if (_resetGrap)
                    _currentNumberHooks = 0;
                _resetGrap = false;
            }
            else
            {
                _currentGrapplePosition = gunTip.position;
                _isGrappling = true;
                _currentNumberHooks++;
            }
            _grapplePoint = hit.point;
            _lr.positionCount = 2;
            
        }
    }


    void StopGrapple() {
        _lr.positionCount = 0;
        _isGrappling = false;

    }

    private Vector3 _currentGrapplePosition;
    
    void DrawRope() {
        if(!_isGrappling)
            return;
        _currentGrapplePosition = Vector3.Lerp(_currentGrapplePosition, _grapplePoint, Time.deltaTime * 8f);
        
        _lr.SetPosition(0, gunTip.position);
        _lr.SetPosition(1, _currentGrapplePosition);
    }

    public bool IsGrappling() {
        return _isGrappling;
    }

    public Vector3 GetGrapplePoint() {
        return _grapplePoint;
    }
}
