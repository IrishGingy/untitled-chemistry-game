using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingManager : MonoBehaviour
{
    // TODO: Can put different tilemaps that are affected by the player's gear and upgrades

    public GameObject startPosition;
    public BoxCollider2D endPositionCollider;
    public GameObject player;
    public int currentPhase;
    public float speed = 1f;
    public GameObject tilemapGO;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        tilemapGO.SetActive(false);
        rb = player.GetComponent<Rigidbody2D>();
        rb.gravityScale = 1f;
        currentPhase = 0;
        player.transform.position = startPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentPhase == 0)
        {
            if (Input.GetMouseButton(0))
            {
                rb.AddForce(Vector3.up * 5);
            }
            if (Input.GetMouseButton(1))
            {
                rb.AddForce(Vector3.up);
                rb.AddForce(Vector3.right * speed);
            }
        }
        //// Calculate the new position based on the current position and the left direction
        //Vector3 newPosition = transform.position + Vector3.right * speed * Time.deltaTime;

        //// Update the position of the GameObject
        //transform.position = newPosition;
    }

    public void ResetFishing()
    {
        player.transform.position = startPosition.transform.position;
    }

    public void FishOn()
    {
        rb.velocity = Vector3.zero;
        player.transform.position = Vector3.zero;
        tilemapGO.SetActive(true);
        currentPhase = 1;
        rb.gravityScale = 0f;
    }
}
