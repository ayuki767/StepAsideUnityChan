using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{
    Animator myAnimator;
    Rigidbody myRigidbody;

    float velocityZ = 16f;
    float velocityX = 10f;
    float VelocityY = 10f;

    float movableRange = 3.4f;

    float coefficient = 0.99f;

    bool isEnd = false;

    GameObject stateText;
    GameObject scoreText;

    int score = 0;

    bool isLButtonDown = false;
    bool isRButtonDown = false;
    bool isJButtonDown = false;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody>();

        myAnimator.SetFloat("Speed", 1);

        stateText = GameObject.Find("GameResultText");
        scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        if(isEnd)
        {
            velocityZ *= coefficient;
            velocityX *= coefficient;
            VelocityY += coefficient;
            myAnimator.speed *= coefficient;
        }
        float inputVelocityX = 0;
        float inputVelocityY = 0;

        if ((Input.GetKey(KeyCode.LeftArrow) || isLButtonDown) && -movableRange < transform.position.x)
        {
            inputVelocityX = -velocityX;
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || isRButtonDown) && transform.position.x < movableRange)
        {
            inputVelocityX = velocityX;
        }

        if((Input.GetKeyDown(KeyCode.Space) || isJButtonDown) && transform.position.y < 0.5f)
        {
            myAnimator.SetBool("Jump", true);
            inputVelocityY = VelocityY;
        }
        else
        {
            inputVelocityY = myRigidbody.velocity.y;
        }

        if(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            myAnimator.SetBool("Jump", false);
        }


        myRigidbody.velocity = new Vector3(inputVelocityX, inputVelocityY, velocityZ);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag")
        {
            isEnd = true;
            stateText.GetComponent<Text>().text = "GAME OVER";
        }
        if(other.gameObject.tag == "GoalTag")
        {
            isEnd = true;
            stateText.GetComponent<Text>().text = "CLEAR!!";
        }
        if(other.gameObject.tag == "CoinTag")
        {
            score += 10;

            scoreText.GetComponent<Text>().text
                = "Score" + score + "pt";

            GetComponent<ParticleSystem>().Play();
            Destroy(other.gameObject);
        }
    }

    public void GetMyJumpButtonDown()
    {
        isJButtonDown = true;
    }
    public void GetMyJumpButtonUp()
    {
        isJButtonDown = false;
    }
    public void GetMyLeftButtonDown()
    {
        isLButtonDown = true;
    }
    public void GetMyLeftButtonUp()
    {
        isLButtonDown = false;
    }
    public void GetMyRightButtonDown()
    {
        isRButtonDown = true;
    }
    public void GetMyRightButtonUp()
    {
        isRButtonDown = false;
    }

}
