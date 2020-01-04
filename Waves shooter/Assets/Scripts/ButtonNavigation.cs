using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonNavigation : MonoBehaviour
{
    int index = 0;
    int totalMenuItems = 2;
    public float yOffset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (index <= totalMenuItems - 1)
            {
                index++;
                Vector2 position = transform.position;
                position.y += yOffset;
                transform.position = position;
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (index > 0)
            {
                index--;
                Vector2 position = transform.position;
                position.y -= yOffset;
                transform.position = position;
            }
        }
    }
}
