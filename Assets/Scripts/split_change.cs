using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Orientation { Vertical, Horizontal }
public enum Player { Player1, Player2 }

public class split_change : MonoBehaviour
{
    private MeshRenderer mesh;
    private FighterController controller;
    
    public Orientation orientation = Orientation.Horizontal;
    public Player player = Player.Player1;
    public Color playerOneColor;
    public Color playerTwoColor;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        controller = GetComponentInParent<FighterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (controller.controlSystem) {
            case ControlSystem.UpDown:
                if (orientation == Orientation.Vertical){
                    mesh.enabled = false;
                } 
                if (orientation == Orientation.Horizontal){
                    mesh.enabled = true;
                }

                if (player == Player.Player1){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerOneColor; //green
                }
                if (player == Player.Player2){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerTwoColor; //blue
                }
                break;
            case ControlSystem.DownUp:
                if (orientation == Orientation.Vertical){
                    mesh.enabled = false;
                }
                if (orientation == Orientation.Horizontal){
                    mesh.enabled = true;
                }
                if (player == Player.Player1){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerTwoColor; //blue
                }
                if (player == Player.Player2){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerOneColor; //green
                }
                break;
            case ControlSystem.LeftRight:
                if (orientation == Orientation.Horizontal){
                    mesh.enabled = false;
                }
                if (orientation == Orientation.Vertical){
                    mesh.enabled = true;
                }
                if (player == Player.Player1){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerOneColor;
                }
                if (player == Player.Player2){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerTwoColor;
                }
                break;
            case ControlSystem.RightLeft:
                if (orientation == Orientation.Horizontal){
                    mesh.enabled = false;
                }
                if (orientation == Orientation.Vertical){
                    mesh.enabled = true;
                }
                if (player == Player.Player1){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerTwoColor;
                }
                if (player == Player.Player2){
                    gameObject.GetComponent<MeshRenderer>().material.color = playerOneColor;
                }
                break;
        }
    }
}
