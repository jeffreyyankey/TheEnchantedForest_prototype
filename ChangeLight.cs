using UnityEngine;
using System.Collections;

namespace TEFprototype
{
    public class ChangeLight : MonoBehaviour
    {
        public Color color0;
        public Color color1;

        public bool reverse = false;

        private Light myLight;

        float tChange;
        float speed = 0.3f;

        void OnEnable()
        {
            SetInitialReferences();
            tChange = 0; //Resets everytime the script is enabled so reverse bool will work
        }

        void SetInitialReferences()
        {
            myLight = GetComponent<Light>();
        }

        void Update()
        {
            if ( tChange < 1 )
            {
                tChange += Time.deltaTime * speed;

                if ( !reverse )
                    myLight.color = Color.Lerp(color0, color1, tChange);
                else
                    myLight.color = Color.Lerp(color1, color0, tChange);
            }
            else
            {
                enabled = false; //Turn off component
            }
        }
    }
}

