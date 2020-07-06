using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    [SerializeField] Transform[] pillarPoints;
    [SerializeField] float pillarDelay = 0f;
    [SerializeField] float pillarSpeed;
    [SerializeField] AudioSource crushSound;
    [SerializeField] ParticleSystem particleEffect;
    [SerializeField] float pillarStayTime = 1f;

    Vector2 nextPos;
    bool hold = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PillarStartDelay());

        transform.position = pillarPoints[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        if (hold) return;

        if(transform.position == pillarPoints[0].position && !hold)
        {
            nextPos = pillarPoints[1].position;
            StartCoroutine(PillarMoveStopper());
        }
        else if (transform.position == pillarPoints[1].position && !hold)
        {
            nextPos = pillarPoints[0].position;
            crushSound.Play();
            particleEffect.Play();
            StartCoroutine(PillarMoveStopper());
        }

        transform.position = Vector2.MoveTowards(transform.position, nextPos, pillarSpeed * Time.deltaTime);     
          
    }

    IEnumerator PillarMoveStopper()
    {
        hold = true;
        yield return new WaitForSeconds(pillarStayTime);
        hold = false;
    }

    IEnumerator PillarStartDelay()
    {
        yield return new WaitForSeconds(pillarDelay);
    }
}
