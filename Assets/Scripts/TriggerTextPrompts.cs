using UnityEngine;
using System.Collections;
using TMPro;

public class TriggerTextPrompts : MonoBehaviour
{

    [SerializeField] TMP_Text storyText;

    [TextArea(5, 10)]
    [SerializeField] string[] textPrompts;

    [SerializeField] GameObject ghostGO;

    [SerializeField] bool hasScare;
    [SerializeField] GameObject scareSfx;
    [SerializeField] int textIndexForScare;

    [SerializeField] bool lookAtGhost;

    [SerializeField] GameObject otherObject;
    [SerializeField] bool lookAtOtherObject;
    [SerializeField] int spawnObjectIndex;
    [SerializeField] int lookAtObjectIndex;

    [SerializeField] bool spawnNextTrigger;
    [SerializeField] GameObject nextTrigger;

    [SerializeField] Transform newGhostSpawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        storyText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            storyText.gameObject.SetActive(true);


            Player player = other.gameObject.GetComponent<Player>();
            player.blockInput = true;


            StartCoroutine(ShowTextPrompts(player));

        }
    }

    IEnumerator ShowTextPrompts(Player player)
    {
        if (newGhostSpawn != null)
        {
            ghostGO.SetActive(true);
            ghostGO.transform.position = newGhostSpawn.transform.position;
        }

        if (lookAtGhost)
        {
            ghostGO.transform.LookAt(player.transform);
            //Camera.main.transform.LookAt(ghostGO.transform);
            Debug.Log("Player current y: " +  player.transform.rotation.y);
            player.transform.LookAt(ghostGO.transform.position);
            player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
            Camera.main.transform.localRotation = Quaternion.identity;
            Debug.Log("Player new y: " + player.transform.rotation.y);
        }


        for (int i = 0; i < textPrompts.Length; i++)
        {
            if (otherObject != null && spawnObjectIndex == i)
            {
                otherObject.SetActive(true);
            }

            if (hasScare && i == textIndexForScare)
            {
                scareSfx.SetActive(true);
            }

            if (lookAtOtherObject && lookAtObjectIndex == i)
            {
                player.transform.LookAt(otherObject.transform.position);
                player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);
                Camera.main.transform.localRotation = Quaternion.identity;
            }


            while (true)
            {
                storyText.text = textPrompts[i];
                if (Input.GetKey(KeyCode.Space))
                {
                    break;
                }

                yield return new WaitForEndOfFrame();
            }


            yield return new WaitWhile(() => Input.GetKey(KeyCode.Space));
        }

        storyText.text = "";
        storyText.gameObject.SetActive(false);
        player.blockInput = false;

        if (spawnNextTrigger)
        {
            nextTrigger.SetActive(true);
        }

        Destroy(gameObject);
    }

}
