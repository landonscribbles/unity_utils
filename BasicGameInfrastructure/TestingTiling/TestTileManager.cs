/*
 * Info:
 * Creates a grid of prefabs to act as a test background. 
 * Requires a prefab testTile that is 1x1x<whatever>, the player object, the Z level
 * that you want for spawned tiles (assuming 2D), and how far out from the screen
 * sides prefabs should be created at.
 * 
 * The script will take care of removing tiles that have gone too far outside of the
 * camera and OnBecameInvisible is *not* required on the linked prefab.
 * 
 * Tested on:
 * Winders
 * 
 * Known Issues:
 * It's currently kind of brittle but could be extended to not be with a little effort.
 * 
 * 
 */

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class TestTileManager : MonoBehaviour {
    public GameObject testTile;
    public GameObject player;
    public int tileZLevel;
    public int extraTilePadding;

    private int playerXLoc;
    private int playerYLoc;

    private Vector3 topLeftScreenPos;
    private Vector3 bottomLeftScreenPos;
    private Vector3 bottomRightScreenPos;

    private List<GameObject> testTiles = new List<GameObject>();
    private List<Vector3> existingTileVectors = new List<Vector3>();
    private List<Vector3> toCreateTileVectors = new List<Vector3>();


    void CreateTiling() {
        foreach (Vector3 tileVector in toCreateTileVectors) {
            GameObject newTile = Instantiate(testTile, tileVector, Quaternion.identity) as GameObject;
            testTiles.Add(newTile);
        }
    }

	void Start () {
        playerXLoc = (int)player.transform.position.x;
        playerYLoc = (int)player.transform.position.y;
        UpdateScreenBoundaries();
        UpdateTileLocations();
	}

    private void UpdateScreenBoundaries() {
        Vector3 playerPos = player.transform.position;

        float zDist = playerPos.z - Camera.main.transform.position.z;

        Vector3 bottomLeftCheck = new Vector3(0, 0, zDist);
        bottomLeftScreenPos = Camera.main.ViewportToWorldPoint(bottomLeftCheck);

        Vector3 bottomRightCheck = new Vector3(1, 0, zDist);
        bottomRightScreenPos = Camera.main.ViewportToWorldPoint(bottomRightCheck);

        Vector3 topLeftCheck = new Vector3(0, 1, zDist);
        topLeftScreenPos = Camera.main.ViewportToWorldPoint(topLeftCheck);
    }

    List<int> BuildRange(int minVal, int maxVal) {
        // Seriously I have to do this? Where's my:
        // range(min, max) ?
        List<int> rangeVals = new List<int>();
        for (int i = minVal; i <= maxVal; i++) {
            rangeVals.Add(i);
        }
        return rangeVals;
    }

    private void UpdateTileLocations() {
        // We want to extend just off of screen
        int bottomLeftXPos = (int)bottomLeftScreenPos.x - extraTilePadding;
        int bottomRightXPos = (int)bottomRightScreenPos.x + extraTilePadding;
        int topLeftYPos = (int)topLeftScreenPos.y + extraTilePadding;
        int bottomLeftYPos = (int)bottomLeftScreenPos.y - extraTilePadding;

        List<int> xRange = BuildRange(bottomLeftXPos, bottomRightXPos);
        List<int> yRange = BuildRange(bottomLeftYPos, topLeftYPos);

        // Add new tile locations if needed
        toCreateTileVectors.Clear();
        foreach (int xVal in xRange) {
            foreach (int yVal in yRange) {
                Vector3 tileLoc = new Vector3(xVal, yVal, tileZLevel);
                toCreateTileVectors.Add(tileLoc);

            }
        }

        // Remove tiles too far out of range and get the locations of existing tiles
        existingTileVectors.Clear();
        for (int i = testTiles.Count - 1; i >= 0; i--) {
            // While we're looping clean up GameObjects that destroyed themselves on OnBecameInvisible
            // Attempted to do this OnBecameInvisble and null checking but it was acting weird
            Vector3 tilePos = testTiles[i].transform.position;
            if (tilePos.x < bottomLeftXPos || tilePos.x > bottomRightXPos || tilePos.y < bottomLeftYPos || tilePos.y > topLeftYPos) {
                GameObject killTile = testTiles[i];
                Destroy(killTile);
                testTiles.RemoveAt(i);
                continue;
            }
            existingTileVectors.Add(testTiles[i].transform.position);
        }

        // Create new tiles as needed
        foreach (Vector3 newTileLoc in toCreateTileVectors) {
            if (!existingTileVectors.Contains(newTileLoc)) {
                GameObject newTile = Instantiate(testTile, newTileLoc, Quaternion.identity) as GameObject;
                testTiles.Add(newTile);
            }
        }
    }
	

	void Update () {
        int newPlayerX = (int)player.transform.position.x;
        int newPlayerY = (int)player.transform.position.y;

        if (newPlayerX != playerXLoc || newPlayerY != playerYLoc) {
            // We only need to do this when a player has moved a significant amount
            UpdateScreenBoundaries();
            UpdateTileLocations();
            playerXLoc = newPlayerX;
            playerYLoc = newPlayerY;
        }
	}
}
