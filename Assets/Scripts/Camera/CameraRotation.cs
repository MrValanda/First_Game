
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float sensitivity;

    public float Sensitivity
    {
        get => sensitivity;
        set => sensitivity = value;
    }

    private Transform _transform;
    private float _moveX, _moveY;
    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        sensitivity = PlayerPrefs.GetFloat("Sensitivity");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        _moveX = Input.GetAxis("Mouse X");
        _moveY += Input.GetAxis("Mouse Y")*sensitivity*Time.fixedDeltaTime;
        _moveY = Mathf.Clamp(_moveY, -90, 90);
        _transform.localRotation = Quaternion.Euler(-_moveY, 0, 0);
        playerTransform.Rotate(Vector3.up * (_moveX * sensitivity * Time.fixedDeltaTime));
    }
}
