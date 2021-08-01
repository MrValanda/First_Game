using System;
using System.Globalization;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed,gravityScale;
    [FormerlySerializedAs("jumpForce")] [SerializeField] private float jumpHeight;
    [SerializeField] [Range(0, 100)] private float maxPowerJumpForce;
    [SerializeField] private Text powerJumpText;
    [SerializeField] private AudioSource walkSound;
    [SerializeField] private AudioSource megaJumpSound;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource fallSound;
     private Grappling grappling;


    public Vector3 _lastPointStay;
    private Vector3 _velocity;
    
    private CharacterController _characterController;
    private RocketBoots _rocketBoots;

    private Transform _transform;

    private float _horizontalInputs, _verticalInputs,_acceleratedMovement ,_directionY;
    
    public float _powerJumpForce;

    private bool _needInertia;
    
    private bool _onGround;

    #region GeneralData
    public bool OnGround
    {
        get => _characterController.isGrounded;
    }
    public bool IsGrappling
    {
        get => grappling.IsGrappling();
    }
    public Vector3 Velocity
    {
        get => _velocity;
        set => _velocity = value;
    }   
    #endregion

    void Start()
    {
        grappling = GetComponent<Grappling>();
        _rocketBoots = GetComponent<RocketBoots>();
        _characterController = GetComponent<CharacterController>();
        _transform = GetComponent<Transform>();
    }

    private void Update()
    {
        AbilityJump();

    }

    void FixedUpdate()
    {
        Inputs();
        Movement();
    }

    private void Inputs()
    {
        _horizontalInputs = Input.GetAxisRaw("Horizontal");
        _verticalInputs = Input.GetAxisRaw("Vertical");
        _acceleratedMovement = Input.GetKey(KeyCode.LeftShift)?1.2f:1;
    }

    private void Movement()
    {
        if (_rocketBoots.OnBoost || grappling.IsGrappling())
        {
            _needInertia = true;
            _directionY = -1f;
        }

        if (!grappling.IsGrappling() && !_rocketBoots.OnBoost)
        {
            if (_needInertia)
            {
                _velocity = Vector3.Slerp(_velocity,
                    (_transform.forward * _verticalInputs + _transform.right * _horizontalInputs).normalized *
                    (moveSpeed * _acceleratedMovement), Time.deltaTime*0.5f);
                _directionY = _velocity.y;
            }
            else
                _velocity = (_transform.forward * _verticalInputs + _transform.right * _horizontalInputs).normalized *
                            (moveSpeed * _acceleratedMovement);

            if (OnGround)
            {
                _directionY = -1f;
                _needInertia = false;
                if (Input.GetKey(KeyCode.Space))
                {
                    _directionY = jumpHeight;
                        jumpSound.Play();
                }
                if (_velocity.magnitude > 0)
                {
                    if (!walkSound.isPlaying)
                        walkSound.Play();
                }
                else
                {
                    walkSound.Stop();
                }
                
            }
            else
            {
                walkSound.Stop();
            }
            CalculateGravity();
        }
       
        _characterController.Move(_velocity * Time.deltaTime);
    }


    void AbilityJump()
    {
        if (Input.GetMouseButton(1))
        {
            _powerJumpForce = Mathf.Lerp(_powerJumpForce,maxPowerJumpForce,3*Time.deltaTime);
            powerJumpText.text =Math.Round(_powerJumpForce / maxPowerJumpForce*100).ToString(CultureInfo.CurrentCulture);
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (OnGround&&Math.Abs(Math.Round(_powerJumpForce / maxPowerJumpForce*100) - 100) < 0.1)
            {
                _directionY = jumpHeight*_powerJumpForce;
                megaJumpSound.Play();
                CalculateGravity();
                _characterController.Move(_velocity*Time.deltaTime);
            }
            _powerJumpForce = 0;
            powerJumpText.text =Math.Round(_powerJumpForce / maxPowerJumpForce*100).ToString(CultureInfo.CurrentCulture);
        }
    }


    private void CalculateGravity()
    {
        _directionY -= gravityScale*Time.deltaTime;
        _velocity.y = _directionY;
        if (Math.Abs(_velocity.y) > 8f)
        {
            if (!fallSound.isPlaying)
                fallSound.Play();
        }
        else
        {
            fallSound.Stop();

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Void"))
        {
            _characterController.enabled = false;
            _transform.position = _lastPointStay;
            _characterController.enabled = true;
        }
        else if (other.gameObject.CompareTag("Save"))
        {
            _lastPointStay = _transform.position;
        }
    }

  
}

