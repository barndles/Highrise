using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEditor;

public class playermovement : MonoBehaviour
{
    public IEnumerator MoveBuffer(float dur)
    {
        canMove = false;
        yield return new WaitForSeconds(dur);
        canMove = true;
    }

    public IEnumerator Respawn(int p)
    {
        //seamless respawn
        if (p == 1)
        {
            p1.GetComponent<Rigidbody>().velocity = Vector3.zero;
            yield return new WaitForSeconds(0.3f);
            p1.transform.DOMove(p1SpawnPoint, 1);
            p1.transform.DORotate(new Vector3(-90 + 360, 0, 0), 1);
            yield return new WaitForSeconds(0.5f);
            canMove = true;
        }

        if (p == 2)
        {
            p2.GetComponent<Rigidbody>().velocity = Vector3.zero;
            yield return new WaitForSeconds(0.3f);
            p2.transform.DOMove(p2SpawnPoint, 1);
            p2.transform.DORotate(new Vector3(-90, 0, 0), 1);
            yield return new WaitForSeconds(0.5f);
            canMove = true;
        }

        StartCoroutine(TileReset());
        yield return null;
    }

/*
        Reset Tiles. 
    ------------------------------------------------------------------------------------------------------
     1.   Log positions of all tiles in Start().
     2.   On player death, Tween all tiles back to their respective starting location, hope it looks cool.   
    -------------------------------------------------------------------------------------------------------
*/
    private IEnumerator TileReset()
    {
        print(tiles[1].transform.position);
        for (int i = 0; i < tiles.Length; i++)
        {
            Vector3 tPos = tiles[i].transform.position;
            tiles[i].transform.DOMove(new Vector3(tPos.x, 0.5f, tPos.z), 0.75f);
            yield return new WaitForSeconds(0.05f);
        }

        yield return null;
    }

    private void LogPositions()
    {
        //players
        p1SpawnPoint = p1.transform.position;
        p2SpawnPoint = p2.transform.position;

        //tiles
        tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    public void Start()
    {
        LogPositions();
    }

    public GameObject p1, p2;
    public float moveIncrement, moveTime;
    public bool canMove = true;
    private Vector3 p1pos;
    private Vector3 p2pos;
    private Vector3 p1Origin, p2Origin;

    public bool p1OnGround, p2OnGround;
    public Vector3 p1SpawnPoint, p2SpawnPoint;

    public GameObject[] tiles;
    public Vector3[] tilePos;

    // Update is called once per frame
    void Update()
    {
        if (p1pos.y <= -3)
        {
            StartCoroutine(Respawn(1));
        }

        if (p2pos.y <= -3)
        {
            StartCoroutine(Respawn(2));
        }

        if (!GroundCheck("p1"))
        {
            canMove = false;
        }
        else if (GroundCheck("p1"))
        {
            canMove = true;
        }

        p1pos = p1.transform.position;
        p2pos = p2.transform.position;

        p1Origin = new Vector3(p1pos.x, p1pos.y + 0.5f, p1pos.z);
        p2Origin = new Vector3(p2pos.x, p2pos.y + 0.5f, p2pos.z);

        p1OnGround = GroundCheck("p1");
        p2OnGround = GroundCheck("p2");


        //movement 
        PlayerMovement();
    }

    void PlayerMovement()
    {
        if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            Move("f");
        }
        else if (Input.GetKeyDown(KeyCode.A) && canMove)
        {
            Move("l");
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            Move("b");
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            Move("r");
        }
    }

    public void Move(string dir) //f r l b
    {

        switch (dir)
        {
            case "f":
                p1.transform.DOMoveX(p1.transform.position.x - moveIncrement, moveTime);
                p2.transform.DOMoveX(p2.transform.position.x + moveIncrement, moveTime);
                StartCoroutine(MoveBuffer(moveTime));
                break;

            case "r":
                p1.transform.DOMoveZ(p1.transform.position.z + moveIncrement, moveTime);
                p2.transform.DOMoveZ(p2.transform.position.z - moveIncrement, moveTime);
                StartCoroutine(MoveBuffer(moveTime));
                break;

            case "l":
                p1.transform.DOMoveZ(p1.transform.position.z - moveIncrement, moveTime);
                p2.transform.DOMoveZ(p2.transform.position.z + moveIncrement, moveTime);
                StartCoroutine(MoveBuffer(moveTime));
                break;

            case "b":
                p1.transform.DOMoveX(p1.transform.position.x + moveIncrement, moveTime);
                p2.transform.DOMoveX(p2.transform.position.x - moveIncrement, moveTime);
                StartCoroutine(MoveBuffer(moveTime));
                break;
        }
    }

    private bool GroundCheck(string p)
    {
        if (p == "p1")
        {
            if (Physics.Raycast(p1Origin, Vector3.down, 0.6f))
            {
                return true;
            }

        }
        else if (p == "p2")
        {
            if (Physics.Raycast(p2Origin, Vector3.down, 0.6f))
            {
                return true;
            }
        }

        return false;
    }

}
