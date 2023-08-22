using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class FishingPhaseII : MonoBehaviour
{
    public float speed = 2f;

    private bool waited;
    private GameObject gridGO;
    private GameObject tilemapGO;
    private Tilemap tilemap;
    private Vector3Int max;
    // number of times the player hits an obstacle
    private int collisions;
    private int numTilesX;

    void Awake()
    {
        collisions = 0;

        gridGO = transform.parent.gameObject;
        tilemapGO = gameObject;
        // Get the reference to the Tilemap component
        tilemap = GetComponent<Tilemap>();

        //StartPhaseII();
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
        // TODO: Give user feedback that they are colliding with an obstacle
        Debug.Log($"Hit an obstacle x{collisions}");

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

    public void StartPhaseII(GameObject player, out int currentPhase)
    {
        currentPhase = 1;
        Cursor.lockState = CursorLockMode.None;
        gridGO.SetActive(true);
        tilemapGO.SetActive(true);
        player.transform.position = gridGO.transform.position;

        // Calculate and display the number of tiles on the Tilemap
        numTilesX = tilemap.cellBounds.size.x;
        max = tilemap.cellBounds.max;
        Vector3 rightMostBoundPosition = tilemap.CellToWorld(max);
        //Debug.Log(numTilesX);
        //Debug.Log(rightMostBoundPosition);

        waited = false;
        StartCoroutine(Wait());
    }

    public void StopFishing()
    {
        waited = false;
        // Resets the tilemap position
        transform.localPosition = Vector3.zero;
        tilemap.CompressBounds();

        StopAllCoroutines();
    }
}
