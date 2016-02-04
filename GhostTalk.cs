using UnityEngine;
using System.Collections;

namespace TEFprototype
{
    public class GhostTalk : MonoBehaviour
    {
        public AudioSource GhostTalkingAudio;

        bool LookAtGhost = false;

        private GameManager_EventMaster eventMasterScript;
        private Transform player;

        void SetInitialReferences()
        {
            eventMasterScript = GameObject.Find("GameManager").GetComponent<GameManager_EventMaster>();
            player = GameObject.Find("FPSController").GetComponent<Transform>();
        }

        void Start()
        {
            SetInitialReferences();
            StartCoroutine(PlayerRotation(3));
        }

        void Update()
        {
            if ( LookAtGhost )
            {
                var rotation = Quaternion.LookRotation(transform.position - player.position);
                player.rotation = Quaternion.Slerp(player.rotation, rotation, Time.deltaTime * 3f);
            }

        }

        void SpawnGhosts()
        {
            eventMasterScript.CallMyGeneralEvent();
            player.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
            transform.GetComponent<NavMeshAgent>().speed = 4f;
            player.gameObject.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().GetComponentInChildren<FindEnemies>().enabled = true;
        }

        IEnumerator PlayerRotation(float waitTime)
        {
            GhostTalkingAudio.Play();
            LookAtGhost = true;
            yield return new WaitForSeconds(waitTime);
            SpawnGhosts();
        } 
    }
}

