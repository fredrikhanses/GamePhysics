using UnityEngine;

public class SimplePhysics : MonoBehaviour
{
    [SerializeField]
    private const float mass = 1.0f;
    private float oneOverMass = 1 / mass;
    [SerializeField]
    private bool useGravity = true;
    [SerializeField]
    SphereCollider sphereCollider;
    [SerializeField]
    bool grounded = false;
    [SerializeField]
    bool collided = false;
    [SerializeField]
    Vector3 input;
    [SerializeField]
    Vector3 velocity;
    [SerializeField]
    Vector3 acc;
    Vector3 hitNormal = Vector3.up;
    RaycastHit hit;
    float friction = 0.0f;
    const string horizontal = "Horizontal";
    const string vertical = "Vertical";

    private void Awake()
    {
        if (sphereCollider == null)
        {
            sphereCollider = GetComponent<SphereCollider>();
        }
    }

    void Update()
    {
        Debug.DrawLine(transform.position, transform.position - transform.up, Color.yellow);
        Debug.DrawLine(transform.position, transform.position - transform.right, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.yellow);
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.yellow);
        CheckGrounded();
        input.x = Input.GetAxis(horizontal);
        input.z = Input.GetAxis(vertical);
        bool jump = Input.GetButtonDown("Jump");
        if (jump && grounded)
        {
            input.y = 1000.0f; 
        }
        else
        {
            input.y = 0.0f;
        }
        ApplyForce(input);
    }

    private Vector3 Reflect(Vector3 velocity, Vector3 hitNormal)
    {
        return (velocity - 2f * Vector3.Dot(velocity, hitNormal) * hitNormal) * 0.95f;
    }

    void ApplyForce(Vector3 force)
    {
        acc = useGravity ? (force + Physics.gravity) * oneOverMass : force * oneOverMass;
        velocity += acc * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        //if (grounded)
        //{
        //    friction += 0.0001f * Time.deltaTime;
        //    if (friction >= 1.0f)
        //    {
        //        velocity = Vector3.zero;
        //    }
        //    velocity = Vector3.Lerp(velocity, Vector3.zero, friction);
        //}
        //else
        //{
        //    friction = 0.0f;
        //}
    }

    private void CheckGrounded()
    {
        if (Physics.Raycast(transform.position, transform.right, out hit, 1.0f))
        {
            Debug.Log("hit");
            hitNormal = (hit.point - transform.position).normalized;
            if (!collided)
            {
                velocity = Reflect(velocity, hitNormal);
                Debug.DrawLine(hit.point, transform.position, Color.red, 5f);
                Debug.DrawLine(hit.point, hit.point - hitNormal, Color.green, 5f);
                grounded = true;
                useGravity = false;
                //acc = Vector3.zero;
                //velocity = Vector3.zero;
                collided = true;
            }
        }
        else if (Physics.Raycast(transform.position, -transform.right, out hit, 1.0f))
        {
            Debug.Log("hit");
            hitNormal = (hit.point - transform.position).normalized;
            if (!collided)
            {
                velocity = Reflect(velocity, hitNormal);
                Debug.DrawLine(hit.point, transform.position, Color.red, 5f);
                Debug.DrawLine(hit.point, hit.point - hitNormal, Color.green, 5f);
                grounded = true;
                useGravity = false;
                //acc = Vector3.zero;
                //velocity = Vector3.zero;
                collided = true;
            }
        }
        else if (Physics.Raycast(transform.position, transform.up, out hit, 1.0f))
        {
            Debug.Log("hit");
            hitNormal = (hit.point - transform.position).normalized;
            if (!collided)
            {
                velocity = Reflect(velocity, hitNormal);
                Debug.DrawLine(hit.point, transform.position, Color.red, 5f);
                Debug.DrawLine(hit.point, hit.point - hitNormal, Color.green, 5f);
                grounded = true;
                useGravity = false;
                //acc = Vector3.zero;
                //velocity = Vector3.zero;
                collided = true;
            }
        }
        else if (Physics.Raycast(transform.position, -transform.up, out hit, 1.0f))
        {
            Debug.Log("hit");
            hitNormal = (hit.point - transform.position).normalized;
            if (!collided)
            {
                velocity = Reflect(velocity, hitNormal);
                Debug.DrawLine(hit.point, transform.position, Color.red, 5f);
                Debug.DrawLine(hit.point, hit.point - hitNormal, Color.green, 5f);
                grounded = true;
                useGravity = false;
                //acc = Vector3.zero;
                //velocity = Vector3.zero;
                collided = true;
            }
        }
        else
        {
            Debug.Log("leave");
            grounded = false;
            useGravity = true;
            collided = false;
        }
    }
}
