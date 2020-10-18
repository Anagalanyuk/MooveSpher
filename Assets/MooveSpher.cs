using UnityEngine;
using UnityEngine.UI;

public class MooveSpher : MonoBehaviour
{
    public static event Messenger.OnCollision CollEnter;
    public static event Messenger.OnCollision CollExit;
    [SerializeField] private float _speed = 0.02f;
    [SerializeField] private Slider _sliderSpeed;
    [SerializeField] private Text _speedLable;
    [SerializeField] private Text _acceleraror;
    [SerializeField] private float _distance = 0.2f;
    [SerializeField] private Rigidbody _riger;
    [SerializeField] private Vector3 _gravity;
    [SerializeField] private float _maxSpeed = 6;

    private void Update()
    {
        Ray rayUP = new Ray(transform.position, Vector3.up);
        Ray rayDown = new Ray(transform.position, Vector3.up * -1);
        Ray rayLeft = new Ray(transform.position, Vector3.left);
        Ray rayRight = new Ray(transform.position, Vector3.left * -1);
        RaycastHit hitUp;
        RaycastHit hitDown;
        RaycastHit hitLeft;
        RaycastHit hitRigt;
        Physics.Raycast(rayUP, out hitUp);
        Physics.Raycast(rayDown, out hitDown);
        Physics.Raycast(rayLeft, out hitLeft);
        Physics.Raycast(rayRight, out hitRigt);
        //Debug.LogError("UP: " + hitUp.distance);
        //Debug.LogError("Down: " + hitDown.distance);
        //Debug.LogError("Left: " + hitLeft.distance);
        //Debug.LogError("Right: " + hitRigt.distance);

        float mooveHorizontal = Input.GetAxis("Horizontal");
        float mooveVertical = Input.GetAxis("Vertical");
        //Debug.LogError("Hor: " + mooveHorizontal);
        //Debug.LogError("vert: " + mooveVertical);
        // Debug.LogError("gravity: " + Physics.gravity);

        Physics.gravity = _gravity;

        if (mooveHorizontal < 0 && hitLeft.distance > _distance)
        {
            // MooveHor(mooveHorizontal);
           // MooveGravityHor(mooveHorizontal);
            MooveRidHor(mooveHorizontal);
        }
        else if (mooveHorizontal > 0 && hitRigt.distance > _distance)
        {
            //MooveHor(mooveHorizontal);
           // MooveGravityHor(mooveHorizontal);
            MooveRidHor(mooveHorizontal);
        }

        if (mooveVertical > 0 && hitUp.distance > _distance)
        {
            //MooveVert(mooveVertical);
            //MooveGravityVert(mooveVertical);
            MooveRidVert(mooveVertical);
        }
        else if (mooveVertical < 0 && hitDown.distance > _distance)
        {
            //MooveVert(mooveVertical);
            //MooveGravityVert(mooveVertical);
              MooveRidVert(mooveVertical);
        }

        //Physics.gravity = _gravity;

        float acceliratorHor = Input.acceleration.x;
        float acceliratorVert = Input.acceleration.y;
        _speedLable.text = _sliderSpeed.value.ToString();
        _maxSpeed = _sliderSpeed.value;
        //_acceleraror.text = Physics.gravity.ToString();
        if (acceliratorHor < 0)
        {
            //MooveHor(acceliratorHor);
            MooveGravityHor(acceliratorHor);
        }
        else if (acceliratorHor > 0)
        {
            //MooveHor(acceliratorHor);
            MooveGravityHor(acceliratorHor);
        }

        if (acceliratorVert > 0)
        {
            //MooveVert(acceliratorVert);
            MooveGravityVert(acceliratorVert);
        }
        else if (acceliratorVert < 0)
        {
            //MooveVert(acceliratorVert);
            MooveGravityVert(acceliratorVert);

        }
    }

    public void MooveRidHor(float hor)
    {
        _riger.AddForce(transform.position.x + hor * _speed,
                         transform.position.y,
                         transform.position.z);
    }

    public void MooveRidVert(float vert)
    {
        _riger.AddForce(transform.position.x,
                         transform.position.y + vert  * _speed,
                         transform.position.z);
    }

    public void MooveHor(float mooveHor)
    {
        transform.position = new Vector3(transform.position.x + mooveHor * _speed * Time.deltaTime,
                                  transform.position.y,
                                  transform.position.z);
    }

    public void MooveVert(float mooveVert)
    {
        transform.position = new Vector3(transform.position.x,
                                 transform.position.y + mooveVert * _speed * Time.deltaTime,
                                 transform.position.z);
    }

    public void MooveGravityHor(float horizontal)
    {
        if (horizontal > 0 && _gravity.x < _maxSpeed)
        {
            _gravity = new Vector3(_gravity.x + horizontal * _speed,
                                          _gravity.y,
                                          _gravity.z);
        }
        else if (horizontal < 0 && _gravity.x > -_maxSpeed)
        {
            _gravity = new Vector3(_gravity.x + horizontal * _speed,
                                          _gravity.y,
                                          _gravity.z);
        }
        else
        {
            _gravity = new Vector3(0, 0, 1f);
            // Debug.LogError("Zero Hor");
        }
    }

    public void MooveGravityVert(float vertical)
    {
        if (vertical > 0 && _gravity.y < _maxSpeed)
        {
            _gravity = new Vector3(_gravity.x,
                                   _gravity.y + vertical * _speed,
                                   _gravity.z);
        }
        else if (vertical < 0 && _gravity.y > -_maxSpeed)
        {
            _gravity = new Vector3(_gravity.x,
                                   _gravity.y + vertical * _speed,
                                   _gravity.z);
        }
        else
        {
            _gravity = new Vector3(0, 0, 1);
            //Debug.LogError("zero vert");
        }
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        //Gizmos.DrawRay(transform.position, Vector3.up * 5);
        //Gizmos.DrawRay(transform.position, Vector3.up * -1 * 5);
        //Gizmos.DrawRay(transform.position, Vector3.left * 5);
        //Gizmos.DrawRay(transform.position, Vector3.left * -1 * 5);
        Gizmos.DrawLine(transform.position, Physics.gravity);

    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log("<color=red>collision</color>");
        CollEnter?.Invoke(collision.gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        CollExit?.Invoke(collision.gameObject.name);
    }
}