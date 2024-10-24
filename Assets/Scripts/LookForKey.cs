using UnityEngine;
using System.Collections;
using TMPro;

public class LookForKey : MonoBehaviour
{

    [SerializeField] TMP_Text lookForKeyText;

    [TextArea(5, 10)]
    [SerializeField] string[] textPrompts;

    [SerializeField] GameObject ghostGO;

    [SerializeField] GameObject ghostScareSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lookForKeyText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            lookForKeyText.gameObject.SetActive(true);


            Player player = other.gameObject.GetComponent<Player>();
            player.blockInput = true;


            StartCoroutine(ShowTextPrompts(player));

        }
    }

    IEnumerator ShowTextPrompts(Player player)
    {

        for (int i = 0; i < textPrompts.Length; i++)
        {

            if (i == 2)
            {
                ghostScareSfx.SetActive(true);
                lookForKeyText.fontSize += 100;
                ghostGO.SetActive(true);
                
                Camera.main.transform.LookAt(ghostGO.transform);


            }
            else if (i == 3)
            {
                lookForKeyText.fontSize -= 100;
            }

            while (true)
            {
                lookForKeyText.text = textPrompts[i];
                if (Input.GetKey(KeyCode.Space))
                {
                    break;
                }

                yield return new WaitForEndOfFrame();
            }


            yield return new WaitWhile(() => Input.GetKey(KeyCode.Space));
        }

        lookForKeyText.text = "";
        lookForKeyText.gameObject.SetActive(false);
        player.blockInput = false;

        Destroy(gameObject);

        ghostGO.GetComponent<GhostMovement>().TriggerStartGhostMovement();
        

       
    }

}
