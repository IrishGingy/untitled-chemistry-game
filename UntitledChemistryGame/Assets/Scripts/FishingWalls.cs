using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FishingWalls : MonoBehaviour
{
    public float speed = 2f;

    private bool waited;
    private Tilemap tilemap;
    private Vector3Int max;
    // number of times the player hits an obstacle
    private int collisions;

    // Start is called before the first frame update
    void Start()
    {
        collisions = 0;

        // Get the reference to the Tilemap component
        tilemap = GetComponent<Tilemap>();
        tilemap.CompressBounds();

        // Calculate and display the number of tiles on the Tilemap
        int numTilesX = tilemap.cellBounds.size.x;
        max = tilemap.cellBounds.max;
        Vector3 rightMostBoundPosition = tilemap.CellToWorld(max);
        //Debug.Log(numTilesX);
        //Debug.Log(rightMostBoundPosition);

        waited = false;
        StartCoroutine(Wait());
    }

    // Update is called once per frame
    void Update()
    {
        if (waited)
        {
            // Calculate the new position based on the current position and the left direction
            Vector3 newPosition = transform.position + Vector3.left * speed * Time.deltaTime;

            // Update the position of the GameObject
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collisions++;

        // restart scene
        if (collisions >= 3)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(2f);
        waited = true;
    }
}
