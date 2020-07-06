using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainLink : MonoBehaviour
{
    [SerializeField] float swingForce;
    [SerializeField] float playerAttachedStartSwingForce = 500;
    [SerializeField] float climbingSpeed;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] ChainLink upperLink;
    [SerializeField] ChainLink lowerLink;
    [SerializeField] Collider2D[] collider2Ds;

    CapsuleCollider2D chainLinkCollider;
    PlayerController playerAttachedToChainLink;
    float horizontal;
    float vertical;
    bool isPlayerAttached;
    // Start is called before the first frame update
    void Start()
    {
        chainLinkCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttachedToChainLink == null) return;
        if (!isPlayerAttached) return;
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        ChangePlayerLocalScale();

        ClimbUpDown();

        if (Input.GetKeyDown(KeyCode.Space))
        {

            playerAttachedToChainLink.StopSwinging();
            playerAttachedToChainLink = null;
            isPlayerAttached = false;
            StartCoroutine(DisableChainLinkCollider());
            foreach (Collider2D c2d in collider2Ds)
            {
                c2d.GetComponent<ChainLink>().Detach();
            }
        }
        
        if(playerAttachedToChainLink.transform.localPosition == new Vector3(0, 0.45f) && (upperLink != null))
        {
            upperLink.AttachPlayer(playerAttachedToChainLink);
            playerAttachedToChainLink = null;
            isPlayerAttached = false;
            return;
        }

        if (playerAttachedToChainLink.transform.localPosition == new Vector3(0, -0.45f) && (lowerLink != null))
        {
            lowerLink.AttachPlayer(playerAttachedToChainLink);
            playerAttachedToChainLink = null;
            isPlayerAttached = false;
            return;
        }
    }

    private void ChangePlayerLocalScale()
    {
        if (playerAttachedToChainLink == null) return;
        if (horizontal < 0)
        {
            playerAttachedToChainLink.transform.localScale = new Vector3(-1, 1, 1);

        }
        else if (horizontal > 0)
        {
            playerAttachedToChainLink.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    private void ClimbUpDown()
    {
        if (vertical > 0)
        {
            playerAttachedToChainLink.transform.localPosition = Vector2.MoveTowards(playerAttachedToChainLink.transform.localPosition, new Vector2(0, 0.45f), climbingSpeed * Time.deltaTime);
        }
        else if (vertical < 0)
        {
            playerAttachedToChainLink.transform.localPosition = Vector2.MoveTowards(playerAttachedToChainLink.transform.localPosition, new Vector2(0, -0.45f), climbingSpeed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (horizontal == 0) return;

        rb2d.AddForce(new Vector2(swingForce * horizontal, swingForce));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if(collision.collider.tag == "Player")
        {
            AttachPlayer(collision.collider.GetComponent<PlayerController>());
            playerAttachedToChainLink.transform.localPosition = new Vector3(0f, 0f, 0f);
            
            rb2d.AddForce(new Vector2(playerAttachedStartSwingForce * playerAttachedToChainLink.transform.localScale.x, 1));
        }
    }

    public void AttachPlayer(PlayerController attachedPlayer)
    {
        playerAttachedToChainLink = attachedPlayer;
        playerAttachedToChainLink.transform.parent = transform;
        playerAttachedToChainLink.SetSwing();
        playerAttachedToChainLink.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        isPlayerAttached = true;
    }

    IEnumerator DisableChainLinkCollider()
    {
        foreach(Collider2D c2d in collider2Ds)
        {
            c2d.enabled = false;
        }

        yield return new WaitForSeconds(0.6f);

        foreach (Collider2D c2d in collider2Ds)
        {
            c2d.enabled = true;
        }
    }  

    void Detach()
    {
        playerAttachedToChainLink = null;
        isPlayerAttached = false;
    }

}
