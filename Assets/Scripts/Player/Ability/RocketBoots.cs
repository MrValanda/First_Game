using UnityEngine;


public class RocketBoots : MonoBehaviour
{
   [SerializeField] private float force;
   [SerializeField] private float duration;
   [SerializeField] private new Transform camera;

   private PlayerController _playerController;
   private Vector3 _boostDirection;
   private bool _startBoost;

   private bool _canBoost;

   public bool OnBoost => _startBoost;

   private void Start()
   {
      _playerController = GetComponent<PlayerController>();
      _canBoost = true;
   }


   private void Update()
   {
      if (_canBoost && !_playerController.OnGround)
      {
         if (!_startBoost &&Input.GetKeyDown(KeyCode.Q))
         {
            _playerController.Velocity=Vector3.zero;
            Invoke(nameof(StartBoost), 0.5f);
         }
         if(_startBoost)
            _playerController.Velocity = _boostDirection.normalized * force;

      }

      if (_playerController.IsGrappling && _startBoost)
      {
         StopBoost();
      }
      
      if (_playerController.OnGround)
      {
         StopBoost();
         _canBoost = true;
      }
   }

   private void StartBoost()
   {
      _boostDirection = camera.forward;
      _startBoost = true;
      Invoke(nameof(StopBoost), duration);
   }

   private void StopBoost()
   {
      _canBoost = false;
      _startBoost = false;
   }

}
