using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Vector3 tempPos;

    [SerializeField]
    private int minX, maxX;

    private readonly string PLAYER_TAG = "Player";

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag(PLAYER_TAG);
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player tag not found!");
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            tempPos = transform.position;
            tempPos.x = player.position.x;

            if (tempPos.x < minX)
            {
                tempPos.x = minX;
            }

            if (tempPos.x > maxX)
            {
                tempPos.x = maxX;
            }

            transform.position = tempPos;
        }
    }
}
