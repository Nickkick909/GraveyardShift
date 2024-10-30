using System.Collections;
using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [SerializeField] Animator gateAnimator;
    [SerializeField] GameObject enjoyMusic;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("test");
        if (other.gameObject.tag  == "Player")
        {
            if (gameObject.tag == "OpenGate")
            {
                gateAnimator.SetTrigger("OpenGate");
                Destroy(gameObject);
            }
            else if (gameObject.tag == "CloseGate")
            {
                gateAnimator.SetTrigger("CloseGate");
                enjoyMusic.SetActive(true);
                StartCoroutine(DisableAnimatorAfterDelay());
                
            }
        }
    }

    IEnumerator DisableAnimatorAfterDelay()
    {
        yield return new WaitForSeconds(2);
        gateAnimator.enabled = false;
        Destroy(gameObject);
    }
}
