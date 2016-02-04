using UnityEngine;
using System.Collections;

namespace TEFprototype
{
    public class ThrowGrenade : MonoBehaviour
    {

        public GameObject grenade;

        private Transform myTransform;

        public float propulsion;

        public AudioSource grenadeSound;

        private bool isAiming = false;

        private bool rightBarrelFiring = true;

        // Use this for initialization
        void Start()
        {
            SetInitialReferences();
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetButtonDown("Fire1"))
            {
                SpawnGrenade();
            }

            if ( Input.GetButtonDown("Fire2") )
            {
                isAiming = true;
            }
            if ( Input.GetButtonUp("Fire2") )
            {
                isAiming = false;
            }

            if ( isAiming )
            {
                myTransform.localPosition = Vector3.Lerp(myTransform.localPosition, new Vector3(0, -.13f, .25f), .5f);
            }

            if ( !isAiming )
            {
                myTransform.localPosition = Vector3.Lerp(myTransform.localPosition, new Vector3(.5f, -.5f, .1f), .5f);
            }
        }

        void SetInitialReferences()
        {
            myTransform = transform;
        }
		
		//Grenade is now a double-barrel shotgun
        void SpawnGrenade()
        {
            //the offset is from the shotgun median
			float barrelOffset;
			
			//this if statement determines where to instantiate the shotgun slug - from the right or left barrel
            if (rightBarrelFiring)
            {
                barrelOffset = .05f;
                rightBarrelFiring = false;
            }
            else
            {
                barrelOffset = -.05f;
                rightBarrelFiring = true;
            }
				
			//Instantiate the shotgun slug/grenade and push it forward 
            GameObject go = ( GameObject )Instantiate(grenade, myTransform.TransformPoint(barrelOffset, 0, 3.6f), myTransform.rotation * Quaternion.Euler(90f,0,0));
            go.GetComponent<Rigidbody>().AddForce(myTransform.forward * propulsion, ForceMode.Impulse);
            grenadeSound.Play();
            Destroy(go, 10);
        }
    }
}

