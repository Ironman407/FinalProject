using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class STEI_PlayerController : MonoBehaviour {

    public float speed;
    public GameObject Enemy;
    public Text endText;
    public Text displayText;
    public AudioClip Audio_Effect;

    private AudioSource source;
    private float timer = 0;
    private bool inBox = false;
    private Rigidbody2D rb2d;

 


	// Use this for initialization
	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D> ();
        endText.text = "";
        source = GetComponent<AudioSource>();
	}


    IEnumerator ByeAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        
        //GameLoader.gameOn = false;

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bar"))
        {
            Enemy.transform.Translate(Vector3.left * Time.deltaTime);
            Enemy.transform.RotateAround(Vector3.zero, Vector3.up, 180 * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.CompareTag("Bar"))
        {
            //GameLoader.AddScore(10);
            endText.text = "You Win!";
            StartCoroutine(ByeAfterDelay(2));
            inBox = true;
            source.PlayOneShot(Audio_Effect);
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertaical = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(moveHorizontal, moveVertaical);
        rb2d.AddForce(movement * speed);

        timer +=  Time.deltaTime;
        if (inBox = false && timer >= 10)
        {
            endText.text = "You lose!";
            StartCoroutine(ByeAfterDelay(2));
        }
    }
}
