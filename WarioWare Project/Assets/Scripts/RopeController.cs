using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// by Antoine
/// This script ise use to pull up by x height the rope of y height
/// </summary>

public class RopeController : MonoBehaviour
{
    #region Variables
    private LineRenderer rope;
    private float timer = 0;
    private bool win;

    [Header("Object attached")]
    public GameObject attachedTo;

    [Header ("Rope Settings")]
    public int ropeSize = 10;
    public int pullingUpRopeSize = 1;

    [Header("Level 3 Parameters")]
    public bool level3;
    public int pullingDownRopeSize = 1;
    public float delayToPullDown = 1;

    #endregion

    private void Start()
    {
        rope = GetComponent<LineRenderer>();
        attachedTo.transform.localPosition = new Vector3(0, ropeSize);                          //Set the position of the chest and therefore the rope height 
    }

    private void Update()
    {
        rope.SetPosition(1, attachedTo.transform.localPosition);                                //The rope is always attach to the chest

        if (rope.GetPosition(1).y > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                attachedTo.transform.position -= new Vector3(0, -pullingUpRopeSize);            //Pulling up the chest
            }
            else
            { 

                if (level3)
                {
                    timer += Time.deltaTime;

                    if (timer > delayToPullDown)
                    {
                        timer = 0;
                        attachedTo.transform.position -= new Vector3(0, pullingDownRopeSize);   //Automatically pulling down the chest
                    }
                }              
            }
        }
        else
        {
            if (!win)
            {
                win = true;
                rope.SetPosition(1, new Vector3(0, 0));
                GameManager.Instance.gameState = GameManager.GameState.Win;
            }          
        }    
    }
}
