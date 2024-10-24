using UnityEngine;

public class OpenGate : MonoBehaviour
{
    [SerializeField] Animator gateAnimator;
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
                Destroy(gameObject);
            }
        }
    }
}
