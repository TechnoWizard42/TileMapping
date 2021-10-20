using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public List<Transform> points;
    public Transform platform;
    int goalPoint = 0;
    public float moveSpeed = 2;

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //change the position of the platform (move towards the goal point)

        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, Time.deltaTime * moveSpeed);

        //check if we are in very close proximity to the next point
        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f)
        {

            //if so change goal point to the next one
            if (goalPoint == points.Count - 1)

                goalPoint = 0;
            else
                goalPoint++;
        }
    }
}
