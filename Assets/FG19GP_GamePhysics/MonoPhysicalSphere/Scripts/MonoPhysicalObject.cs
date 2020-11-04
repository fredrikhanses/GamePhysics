using UnityEngine;

namespace FutureGames.GamePhysics
{
    public class MonoPhysicalObject : MonoBehaviour
    {
        [SerializeField]
        Vector3 velocity = Vector3.zero;
        public Vector3 Velocity
        {
            get => velocity;
            set => velocity = new Vector3(
                constrainMove_X ? 0f : value.x,
                constrainMove_Y ? 0f : value.y,
                constrainMove_Z ? 0f : value.z);
        }

        [SerializeField]
        float maxVelocity = 40f;

        [SerializeField]
        bool constrainMove_X = false;
        [SerializeField]
        bool constrainMove_Y = false;
        [SerializeField]
        bool constrainMove_Z = false;

        public float mass = 1f;

        [SerializeField]
        bool useGravity = false;

        public bool isVerlet = true;

        /// <summary>
        /// Euler integration
        /// </summary>
        /// <param name="force"></param>
        public void ApplyForce(Vector3 force)
        {
            Vector3 totalForce = useGravity ? force + mass * Physics.gravity : force;

            // f = m * a
            // a = f / m
            Vector3 acc = totalForce / mass;

            Integrate(acc, isVerlet);
        }

        void Integrate(Vector3 acc, bool isVerlet = false)
        {
            if (isVerlet == false) // use Euler
            {
                // v1 = v0 + a*detaTime
                Velocity = Velocity + acc * Time.fixedDeltaTime;

                // p1 = p0 + v*deltatime
                transform.position = transform.position + Velocity * Time.fixedDeltaTime;
            }
            else // use Verlet
            {
                transform.position +=
                    Velocity * Time.fixedDeltaTime +
                    acc * Time.fixedDeltaTime * Time.fixedDeltaTime * 0.5f;

                Velocity += acc * Time.fixedDeltaTime * 0.5f; // ??
            }
        }

        private void Update()
        {
            UpdateMethod();
        }

        protected virtual void UpdateMethod()
        {

        }

        private void FixedUpdate()
        {
            FixedUpdateMethod();
        }

        protected virtual void FixedUpdateMethod()
        {
            ApplyForce(new Vector3(0f, 0f, 0f));
        }

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterMethod(other);
        }

        protected virtual void OnTriggerEnterMethod(Collider other)
        {

        }

        private void OnTriggerExit(Collider other)
        {
            OnTriggerExitMethod(other);
        }

        protected virtual void OnTriggerExitMethod(Collider other)
        {
        }

        protected void LimitVelocity()
        {
            Velocity = Velocity.normalized * Mathf.Min(Velocity.magnitude, maxVelocity);
        }
    }
}