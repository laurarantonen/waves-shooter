using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationHelper : MonoBehaviour
{
    public GameObject player;

    void Update()
    {
        CheckRightTurn();
        CheckLeftTurn();
    }

    private void CheckRightTurn()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.GetComponent<Animator>().Play("Player_turn_right");
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Debug.Log("Right key was released.");
            player.GetComponent<Animator>().Play("Player_idle");
        }
    }

    private void CheckLeftTurn()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.GetComponent<Animator>().Play("Player_turn_left");
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Debug.Log("Left key was released.");
            player.GetComponent<Animator>().Play("Player_idle");
        }
    }

}
