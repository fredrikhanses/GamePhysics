using UnityEngine;

namespace FutureGames.GamePhysics
{
    public class MonoCar : MonoPhysicalObject
    {
        [SerializeField]
        float thrust = 10f;

        [SerializeField]
        float steering = 120f;

        [SerializeField]
        float steeringForceCoef = 10f;

        float ForwardInput()
        {
            return Input.GetAxis("Vertical");
        }

        float SteeringInput()
        {
            return Input.GetAxisRaw("Horizontal");
        }

        protected override void UpdateMethod()
        {
            transform.Rotate(transform.up * SteeringInput() * steering * Time.deltaTime);
        }

        protected override void FixedUpdateMethod()
        {
            base.FixedUpdateMethod();

            //ApplyForce(transform.forward * ForwardInput() * thrust);
            Velocity = transform.forward * ForwardInput() * thrust;

            ApplyForce(transform.right * SteeringInput() * steering * steeringForceCoef * Velocity.magnitude);
        }
    }
}