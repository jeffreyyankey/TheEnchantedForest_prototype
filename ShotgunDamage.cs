using UnityEngine;
using System.Collections;

namespace Chapter1
{
    public class ShotgunDamage : MonoBehaviour
    {
        private Collider[] hitColliders;
        private float destroyTime = 7;
        public float blastRadius;

        public float explosionPower;
        public LayerMask explosionLayers;

        void OnCollisionEnter(Collision col)
        {
            //Debug.Log(col.contacts[0].point.ToString());
            if ( col.gameObject.GetComponent<Rigidbody>() != null)
            {
                //Shotgun slug hit object with rigidbody so apply a force to it
				ApplyForceToTarget(col.contacts[0].point, col.gameObject.GetComponent<Rigidbody>());
            }
                
            Destroy(gameObject);
        }

        void ApplyForceToTarget(Vector3 contactPoint, Rigidbody rb)
        {

            if ( rb.GetComponent<NavMeshAgent>() != null)
                rb.GetComponent<NavMeshAgent>().enabled = false;

            rb.isKinematic = false;

            if ( rb.CompareTag("Enemy") )
            {
                //rb.GetComponent<Rigidbody>().isKinematic = false;
                explosionPower = explosionPower * 10;
            }
              
            rb.AddExplosionForce(explosionPower, contactPoint, blastRadius, .5f, ForceMode.Impulse);
            //Vector3 direction = rb.transform.position - transform.position;
            //rb.AddForceAtPosition(direction.normalized, contactPoint, ForceMode.Impulse);

            if ( rb.CompareTag("Enemy") )
            {
                Destroy(rb.gameObject, destroyTime);
            }
        }
    }
}