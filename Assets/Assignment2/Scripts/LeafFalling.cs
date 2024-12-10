using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public class LeafFalling : MonoBehaviour
{
    public float fallingspeed = 5f;
    public float flipInterval = 0.5f;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        StartCoroutine(FlipLeaf());
    }



    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(transform.position, new Vector3(0, -4, 0), fallingspeed* Time.deltaTime);

    }

    IEnumerator FlipLeaf()
    {
        while(true)
        {
            this.spriteRenderer.flipX = !this.spriteRenderer.flipX;
            yield return new WaitForSeconds(flipInterval);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
