using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieAnimation : MonoBehaviour
{
    [SerializeField] GameObject zombieWalkTarget;
    [SerializeField] float walkSpeed;
    [SerializeField] bool walkTowardsTargetPoint = true;
    [SerializeField] Animator zombieAnimator;

    [SerializeField] Player player;

    bool startedWalking = false;
    [SerializeField] float waitTimeToReleasePlayer;
    bool releasePlayer = false;

    [SerializeField] float followPlayerTime;
    public bool stopFollowingPlayer = false;

    [SerializeField] TMP_Text storyText;
    int countDown = 5;

    [SerializeField] GameObject scareCrowWall;
    [SerializeField] GameObject ghostGO;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (walkTowardsTargetPoint)
        {
            transform.LookAt(zombieWalkTarget.transform);

            //player.transform.LookAt(transform.position);
            //player.transform.eulerAngles = new Vector3(0, player.transform.rotation.y, 0);
            

            if (!startedWalking)
            {
                startedWalking = true;
                StartCoroutine(WaitToReleasePlayer());
            }

            zombieAnimator.SetFloat("MoveSpeed", walkSpeed);
            transform.position = Vector3.MoveTowards(transform.position, zombieWalkTarget.transform.position, walkSpeed * Time.deltaTime);

            // transform.position == zombieWalkTarget.transform.position || 
            if (stopFollowingPlayer == true)
            {
                walkTowardsTargetPoint = false;
                player.playerInDanger = false;
                
            } else
            {
                
            }
        } else
        {
            zombieAnimator.SetFloat("MoveSpeed", 0);
            zombieAnimator.SetBool("Attack", true);
        }
        
        if (!releasePlayer)
        {
            player.blockInput = true;
            player.blockLook = true;

        } else
        {
            if (!stopFollowingPlayer)
                player.blockInput = false;

            if (!stopFollowingPlayer)
            {
                //Camera.main.transform.LookAt(transform.position);
                //Camera.main.transform.localRotation = Quaternion.identity;
                ////player.transform.LookAt(transform.position);
                //player.gameObject.transform.eulerAngles = new Vector3(0, -transform.rotation.y, 0);
                //Debug.Log(player.transform.eulerAngles);
            }

            
        }
    }

    IEnumerator WaitToReleasePlayer()
    {
        player.playerInDanger = true;
        ghostGO.SetActive(false);

        StartCoroutine(CountdownToReleasePlayer());
        Camera.main.transform.LookAt(transform.position);
        //player.DisableFlashLight();
        yield return new WaitForSeconds(waitTimeToReleasePlayer);
        releasePlayer = true;
        scareCrowWall.SetActive(true);


        //player.EnableFlashLight();
        player.transform.LookAt(transform.position);
        player.transform.eulerAngles = new Vector3(0, player.transform.eulerAngles.y, 0);

        player.blockLook = false;
        
        Camera.main.transform.localRotation = Quaternion.identity;

        //// Speed the zombie up for a more intense chanse scene
        //walkSpeed *= 1.5f;
        //zombieAnimator.SetFloat("MoveSpeed", walkSpeed);

        yield return new WaitForSeconds(followPlayerTime);
        stopFollowingPlayer = true;
        
        Camera.main.transform.localRotation = Quaternion.identity;
    }

    IEnumerator CountdownToReleasePlayer()
    {
        
        while (countDown > 0)
        {
            storyText.gameObject.SetActive(true);
            storyText.text = "You have to choose left or right... Time left: " + countDown.ToString();
            yield return new WaitForSeconds(1);
            countDown -= 1;
        }

        storyText.text= "";
        storyText.gameObject.SetActive(false);

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(("Player")))
        {
            // Player dies!!
            player.Die("Greg the Zombie", transform);

            stopFollowingPlayer = true;

        }
    }


}
