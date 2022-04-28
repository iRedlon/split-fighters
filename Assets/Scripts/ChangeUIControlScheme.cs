using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeUIControlScheme : MonoBehaviour
{
    public Image LT;

    public Image LB;

    public Image LS;

    public Image RT;

    public Image RB;

    public Image RS;

    public Color NO_COLOR = new Color32(0, 0, 0, 255);
    public Color LEFT_SIDE_COLOR = new Color32(0, 0, 0, 255);
    public Color RIGHT_SIDE_COLOR = new Color32(0, 0, 0, 255);

    public void setControlScheme(string leftSideControl) {
        switch (leftSideControl) {
            case "left":
            LT.color = LEFT_SIDE_COLOR;;
            LB.color = LEFT_SIDE_COLOR;
            LS.color = LEFT_SIDE_COLOR;
            RT.color = NO_COLOR;
            RS.color = RIGHT_SIDE_COLOR;
            RB.color = RIGHT_SIDE_COLOR;
            break;
            case "right":
            LT.color = NO_COLOR;
            LB.color = LEFT_SIDE_COLOR;
            LS.color = LEFT_SIDE_COLOR;
            RT.color = RIGHT_SIDE_COLOR;
            RS.color = RIGHT_SIDE_COLOR;
            RB.color = RIGHT_SIDE_COLOR;
            break;
            case "up":
            LT.color = LEFT_SIDE_COLOR;
            LB.color = LEFT_SIDE_COLOR;
            LS.color = NO_COLOR;
            RT.color = NO_COLOR;
            RB.color = RIGHT_SIDE_COLOR;
            RS.color = RIGHT_SIDE_COLOR;
            break;
            case "down":
            LT.color = NO_COLOR;
            LB.color = LEFT_SIDE_COLOR;
            LS.color = LEFT_SIDE_COLOR;
            RT.color = RIGHT_SIDE_COLOR;
            RB.color = RIGHT_SIDE_COLOR;
            RS.color = NO_COLOR;
            break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        setControlScheme("left");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
