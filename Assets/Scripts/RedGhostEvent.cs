using System.Collections;
using UnityEngine;

public class RedGhostEvent : MonoBehaviour
{
    [SerializeField] GameObject[] redGhosts;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource heartBeat;

    bool eventStarted = false;
    Transform player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( (eventStarted))
        {
             for (int i = 0; i < redGhosts.Length; i++)
            {
                redGhosts[i].transform.LookAt(player);
            }
        }
       
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            audioSource.enabled = true;
            heartBeat.enabled = true;
            StartCoroutine(StartRedGhostEvent(other.transform));
        }
    }

    IEnumerator StartRedGhostEvent(Transform playerTransform)
    {
        for (int i = 0; i < redGhosts.Length; i++)
        {
            player = playerTransform;
            eventStarted = true;
            yield return new WaitForSeconds(0.25f);
            redGhosts[i].SetActive(true);
            //redGhosts[i].transform.LookAt(player);
        }
    }
}
