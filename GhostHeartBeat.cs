using UnityEngine;
using System.Collections;

public class GhostHeartBeat : MonoBehaviour {

    private Light myLight;
    private NavMeshAgent myNMA;

    float max;
    float min;
    float tChange;
    float speed;
    float waitTime;

    float deathTime;

    bool reverse = false;
    bool cycleComplete = false;

    // Use this for initialization
    void Start ()
    {
        SetInitialReferences();

        max = Random.Range(4, 5);
        min = Random.Range(.5f, 1.5f);
        speed = Random.Range(.75f, 1.25f); //speed is invert seconds
        waitTime = Random.Range(1.5f, 2.5f); //pause between heartbeats
    }

    void SetInitialReferences()
    {
        myLight = GetComponent<Light>();
        myNMA = GetComponentInParent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update ()
    {
        if ( myNMA.enabled )
            if ( tChange < 1 )
            {
                tChange += Time.deltaTime * speed;

                if ( !reverse )
                    myLight.intensity = Mathf.Lerp(min, max, tChange);
                else
                    myLight.intensity = Mathf.Lerp(max, min, tChange);
            }
            else
            {
                reverse = !reverse;
                tChange = 0;

                if ( reverse )
                    cycleComplete = true;

                if ( cycleComplete )
                {
                    cycleComplete = false;
                    HeartBeatPause(waitTime);
                }
            }
        else //Ghost is dying
        {
            deathTime += Time.deltaTime;

            if ( tChange < 1 )
            {
                tChange += Time.deltaTime * 10f; // Speed up heartbeat

                if ( !reverse )
                    myLight.intensity = Mathf.Lerp(Mathf.Max(0, min - deathTime), Mathf.Max(0, max - deathTime), tChange);
                else
                    myLight.intensity = Mathf.Lerp(Mathf.Max(0, max - deathTime), Mathf.Max(0, min - deathTime), tChange);
            }
            else
            {
                reverse = !reverse;
                tChange = 0;

                if ( reverse )
                    cycleComplete = true;

                if ( cycleComplete )
                {
                    cycleComplete = false;
                    HeartBeatPause(waitTime);
                }
            }
        }
	}

    IEnumerator HeartBeatPause(float _waitTime)
    {
        yield return new WaitForSeconds(_waitTime);
    }
}
