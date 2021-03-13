using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class playermovement : MonoBehaviour
{
    public IEnumerator MoveBuffer(float dur)
    {
        canMove = false;
        yield return new WaitForSeconds(dur);
        canMove = true;

        if (!GroundCheck("p1"))
        {
            canMove = false;
        }
        else if (GroundCheck("p1"))
        {
            canMove = true;
        }
    }


    public GameObject p1, p2;
    public float moveIncrement, moveTime;
    public bool canMove = true;
    private Vector3 p1pos;
    private Vector3 p2pos;
    private Vector3 p1Origin, p2Origin;

    public bool p1OnGround, p2OnGround;

    // Update is called once per frame
    void Update()
    {
        p1pos = p1.transform.position;
        p2pos = p2.transform.position;

        p1Origin = new Vector3(p1pos.x, p1pos.y + 0.5f, p1pos.z);
        p2Origin = new Vector3(p2pos.x, p2pos.y + 0.5f, p2pos.z);

        p1OnGround = GroundCheck("p1");
        p2OnGround = GroundCheck("p2");


        //movement 
        if (Input.GetKeyDown(KeyCode.W) && canMove)
        {
            p1.transform.DOMoveX(p1.transform.position.x - moveIncrement, moveTime);
            p2.transform.DOMoveX(p2.transform.position.x + moveIncrement, moveTime);
            StartCoroutine(MoveBuffer(moveTime));
        }
        else if(Input.GetKeyDown(KeyCode.A) && canMove) {
            p1.transform.DOMoveZ(p1.transform.position.z - moveIncrement, moveTime);
            p2.transform.DOMoveZ(p2.transform.position.z + moveIncrement, moveTime);
            StartCoroutine(MoveBuffer(moveTime));
        }
        else if (Input.GetKeyDown(KeyCode.S) && canMove)
        {
            p1.transform.DOMoveX(p1.transform.position.x + moveIncrement, moveTime);
            p2.transform.DOMoveX(p2.transform.position.x - moveIncrement, moveTime);
            StartCoroutine(MoveBuffer(moveTime));
        }
        else if (Input.GetKeyDown(KeyCode.D) && canMove)
        {
            p1.transform.DOMoveZ(p1.transform.position.z + moveIncrement, moveTime);
            p2.transform.DOMoveZ(p2.transform.position.z - moveIncrement, moveTime);
            StartCoroutine(MoveBuffer(moveTime));
        }




    }

    public bool GroundCheck(string p)
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
