using UnityEngine;

namespace FutureGames.GamePhysics
{
    public class MonoPhysicalSphere : MonoPhysicalObject
    {
        public bool onPlane = false;

        public float Radius => transform.localScale.x * 0.5f;

        protected override void FixedUpdateMethod()
        {
            base.FixedUpdateMethod();

            //Vector3 hitPoint = plane.Projection(this);
            //bool isColliding = plane.IsColliding(this);

            //Debug.DrawLine(transform.position, hitPoint, isColliding ? Color.red : Color.blue);

            //CorrectPosition(isColliding, hitPoint);
        }

        //void CorrectPosition(bool isColliding, Vector3 hitPoint, MonoPlane plane)
        //{
        //    if (isColliding == false)
        //        return;

        //    Vector3 correctedPosition = plane.CorrectedPosition(this);
        //    Debug.DrawLine(hitPoint, correctedPosition, Color.green);

        //    transform.position = correctedPosition;
        //}


        /// <summary>
        /// Assuming the initial velocity is 0
        /// </summary>
        /// <returns></returns>
        public float VelocityOnGround(MonoPlane plane)
        {
            return Mathf.Sqrt(2f * Physics.gravity.magnitude * (transform.position.y - plane.transform.position.y));
        }

        //public float ErrorVelocityOnTheGround()
        //{
        //    return Mathf.Abs(velocity.magnitude - VelocityOnGround());
        //}

        protected override void OnTriggerEnterMethod(Collider other)
        {
            UpdateOnPlaneWhenEnter(other);
        }

        private void UpdateOnPlaneWhenEnter(Collider other)
        {
            MonoPlane plane = other.GetComponent<MonoPlane>();
            if (plane == null)
                onPlane = false;
            else
                onPlane = true;
        }

        protected override void OnTriggerExitMethod(Collider other)
        {
            UpdateOnPlaneWhenExit(other);
        }

        private void UpdateOnPlaneWhenExit(Collider other)
        {
            MonoPlane plane = other.GetComponent<MonoPlane>();
            if (plane != null)
                onPlane = false;
        }
    }
}