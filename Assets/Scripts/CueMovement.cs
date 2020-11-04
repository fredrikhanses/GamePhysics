using UnityEngine;

public class CueMovement : MonoBehaviour
{
    [SerializeField]
    private const float mass = 1.0f;
    private float oneOverMass = 1 / mass;
    [SerializeField]
    Vector3 input;
    [SerializeField]
    Vector3 velocity;
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
    bool shot = false;
    float shootTimer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        input.x = 0.0f;
        input.z = Input.GetAxis(mouseX) * 250f;
        input.y = Input.GetAxis(mouseY) * 250f;
        bool draw = Input.GetMouseButton(0);
        bool shoot = Input.GetMouseButtonUp(0);
        bool reset = Input.GetMouseButtonDown(1);
        if (reset)
        {
            transform.position = new Vector3(35.0f, 1.0f, 0.0f);
        }
        if (draw)
        {
            input.x = 100.0f;
            drawTime += Time.deltaTime;
        }
        if (shoot)
        {
            shot = true;
        }
        if (shot)
        {
            input.x = drawTime * -1000.0f;
            lastShot = input.x;
            shootTimer += Time.deltaTime;
            if (shootTimer >= 0.5f)
            {
                shot = false;
                drawTime = 0.0f;
                shootTimer = 0.0f;
            }
        }
        ApplyForce(input);
    }

    void ApplyForce(Vector3 force)
    {
        acc = force * oneOverMass;
        velocity += acc * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
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
