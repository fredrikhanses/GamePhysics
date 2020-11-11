using UnityEngine;

public class CueMovement : MonoBehaviour
{
    [SerializeField]
    private const float mass = 1.0f;
    private float oneOverMass = 1 / mass;
    [SerializeField]
    Vector3 input;
    [SerializeField]
    public Vector3 velocity;
    [SerializeField]
    Vector3 acc;
    [SerializeField]
    float friction = 0.0f;
    const string mouseX = "Mouse X";
    const string mouseY = "Mouse Y";
    const string horizontal = "Horizontal";
    const string vertical = "Vertical";
    [SerializeField]
    float drawTime;
    [SerializeField]
    float lastShot;
    [SerializeField]
    float speed;
    bool shot = false;
    float shootTimer;
    public float minimumX = -90.0f;
    public float maximumX = 90.0f;
    public float minimumY = -60.0f;
    public float maximumY = 60.0f;
    float rotationY = 0.0f;
    float rotationX = 0.0f;
    public float sensitivityX = 1.0f;
    public float sensitivityY = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        rotationX = Input.GetAxis(mouseX) * sensitivityX;
        rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);
        rotationY = Input.GetAxis(mouseY) * sensitivityY;
        rotationY = -Mathf.Clamp(rotationY, minimumY, maximumY);
        if (rotationX != 0.0f)
        {
            transform.Rotate(Vector3.right, rotationX);
        }
        //if (rotationY != 0.0f)
        //{
        //    transform.Rotate(Vector3.forward, rotationY);
        //}
        input.z = Input.GetAxis(horizontal) * 100f;
        input.y = Input.GetAxis(vertical) * 100f;
        input.x = 0.0f;
        bool draw = Input.GetMouseButton(0);
        bool shoot = Input.GetMouseButtonUp(0);
        bool reset = Input.GetMouseButtonDown(1);
        if (reset)
        {
            transform.position = new Vector3(50.0f, 0.0f, 0.0f);
            transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
        }
        if (draw)
        {
            input.x = -50.0f;
            drawTime += Time.deltaTime;
        }
        if (shoot)
        {
            shot = true;
        }
        if (shot)
        {
            input.x = drawTime * 1000.0f;
            input.x = Mathf.Clamp(input.x, 0.0f, 3000.0f);
            lastShot = input.x;
            shootTimer += Time.deltaTime;
            if (shootTimer >= 0.2f)
            {
                shot = false;
                drawTime = 0.0f;
                shootTimer = 0.0f;
                //transform.position = new Vector3(35.0f, 10.0f, 0.0f);
                //transform.eulerAngles = new Vector3(0.0f, 0.0f, 90.0f);
            }
        }
        Vector3 directionForward = transform.up;
        Vector3 directionRight = transform.forward;
        Vector3 force = input.z * directionRight + input.x * directionForward;
        force.y = input.y;
        ApplyForce(force);
    }

    void ApplyForce(Vector3 force)
    {
        acc = force * oneOverMass;
        velocity += acc * Time.deltaTime;
        if (speed > 50.0f && !shot)
        {
            velocity = velocity.normalized * 50.0f;
        }
        transform.position += velocity * Time.deltaTime;
        speed = velocity.magnitude;
        if (transform.position.x > 50.0f)
        {
            Vector3 temp = new Vector3(transform.position.x - 50.0f, 0.0f, 0.0f);
            transform.position -= temp;
            velocity = Vector3.zero;
        }
        if (transform.position.x < -50.0f)
        {
            Vector3 temp = new Vector3(transform.position.x + 50.0f, 0.0f, 0.0f);
            transform.position -= temp;
            velocity = Vector3.zero;
        }
        if (transform.position.y < 0.0f)
        {
            Vector3 temp = new Vector3(0.0f, transform.position.y, 0.0f);
            transform.position -= temp;
            velocity = Vector3.zero;
        }
        if (transform.position.y > 25.0f)
        {
            Vector3 temp = new Vector3(0.0f, transform.position.y - 25.0f, 0.0f);
            transform.position -= temp;
            velocity = Vector3.zero;
        }
        if (transform.position.z > 25.0f)
        {
            Vector3 temp = new Vector3(0.0f, 0.0f, transform.position.z - 25.0f);
            transform.position -= temp;
            velocity = Vector3.zero;
        }
        if (transform.position.z < -25.0f)
        {
            Vector3 temp = new Vector3(0.0f, 0.0f, transform.position.z + 25.0f);
            transform.position -= temp;
            velocity = Vector3.zero;
        }
        if (force.magnitude == 0.0f)
        {
            friction += 0.5f * Time.deltaTime;
            velocity *= 1.0f - friction;
        }
        else
        {
            friction = 0.0f;
        }
        if (friction >= 1.0f)
        {
            velocity = Vector3.zero;
            friction = 0.0f;
        }
    }
}
