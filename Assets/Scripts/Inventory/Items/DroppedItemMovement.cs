using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedItemMovement : MonoBehaviour
{
    float bottomOfRoot;

    bool landed;

    // Start is called before the first frame update
    void Start()
    {
        int randomDirection = Random.Range(-500, 500);
        int randomImpulse = Random.Range(800, 1500);
        Vector2 impulse = new Vector2(randomDirection, randomImpulse);
        gameObject.GetComponent<Rigidbody2D>().AddForce(impulse, ForceMode2D.Impulse);

        bottomOfRoot = FightManager.instance.enemiesParent.transform.position.x;
        landed = false;
        animationMovement = null;
    }

    Vector3 startingPos;
    // Update is called once per frame
    void Update()
    {
        if (!landed)
        {
            if (gameObject.transform.position.y < bottomOfRoot)
            {
                Vector3 newPos = new Vector3(gameObject.transform.position.x, bottomOfRoot);
                gameObject.transform.position = newPos;
                landed = true;
                // L.og("landed");
                startingPos = gameObject.transform.position;

            }

            if (gameObject.transform.position.x < 50)
            {
                //L.og("<0");
                Vector3 newPos = new Vector3(50, gameObject.transform.position.y);
                gameObject.transform.position = newPos;
            }

            if (gameObject.transform.position.x > Screen.width - 50)
            {
                //L.og("<0");
                Vector3 newPos = new Vector3(Screen.width - 50, gameObject.transform.position.y);
                gameObject.transform.position = newPos;
            }


        }
        else
        {
            if (!startedAnimation)
            {
                // L.og("lan animation");
                gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
                gameObject.GetComponent<Rigidbody2D>().mass = 0;
                gameObject.GetComponent<Rigidbody2D>().simulated = false;

                startedAnimation = true;

                target = GameObject.Find("GoldIcon").transform.position;
                

                animationMovement = StartCoroutine(Animate());

            }
        }
    }

    Vector3 target;
    bool startedAnimation = false;

    private bool IsAnimating { get { return animationMovement != null; } }
    Coroutine animationMovement;

    IEnumerator Animate()
    {
        //L.og("lan " + Vector3.Distance(gameObject.transform.position, target));
        while (Vector3.Distance(gameObject.transform.position, target) > 50)
        {
            //L.og("asdasd ");
            gameObject.transform.position = Vector3.LerpUnclamped(gameObject.transform.position, target, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();

        }

        gameObject.GetComponent<Rigidbody2D>().simulated = true;
        StopAnimation();
        Destroy(gameObject);
    }

    private void StopAnimation()
    {
        if (IsAnimating)
            StopCoroutine(animationMovement);
        animationMovement = null;
    }

}
