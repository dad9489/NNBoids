using System;
using System.Collections;
using System.Collections.Generic;
using AgentBehavior.NeuralNetwork;
using UnityEngine;
using UnityEngine.UI;

public class AgentDetailViewer : MonoBehaviour {
    public GameObject nodePrefab;
    [Range(1, 25)]public int numNodes = 1; //TODO temporary
    [Range(1, 25)] public int numRows = 1; //TODO temporary

    private int prevNumNodes = -1;  //TODO temporary
    private int prevNumRows = -1; //TODO temporary
    private BoidAgent prevSelected = null;
    private ArrayList drawnNodes = new ArrayList();
    private void Update() { //TODO make this more efficient
        BoidAgent selectedAgent = GlobalManager.Instance.selectedAgent;
        if (selectedAgent != prevSelected || numNodes != prevNumNodes || numRows != prevNumRows) {  //if we are selecting a new agent on this step
            prevSelected = selectedAgent;
            prevNumNodes = numNodes;
            prevNumRows = numRows;
            foreach (GameObject node in drawnNodes) {
                Destroy(node);
            }
            
            Genome genome = selectedAgent.genome;
            Camera cam = Camera.allCameras[1]; //TODO hacky - if more cameras are added this will need to change (also should we be doing this in update?)

            var position = transform.position;
            var screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            screenBottomLeft.z = 0;
            var screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));
            screenTopRight.z = 0;
            Debug.Log("screenBottomLeft: "+screenBottomLeft+", screenTopRight: "+screenTopRight);
            
            var scale = Mathf.Min((20f / numNodes), 5);
            for (int i = 0; i < numNodes; i++) {
                for (int j = 0; j < numRows; j++) {
                    var nodePosition = screenTopRight;
                    nodePosition.x -= (j+1)*scale*(1.25f);
                    if (numNodes < 4) {
                        // for 1 through 3, space them out on the 1/2 mark for 1, 1/3rd marks for 2, 1/4th marks for 3
                        nodePosition.y = screenTopRight.y - 
                                         ((i+1)* ((Math.Abs(screenTopRight.y) + Math.Abs(screenBottomLeft.y))/(numNodes+1)));
                    }
                    else {
                        nodePosition.y -= scale*(i + 0.5f);
                    }
                    var newNode = Instantiate(
                        nodePrefab,
                        nodePosition,
                        Quaternion.identity,
                        transform);
                    newNode.transform.localScale = new Vector3(scale, scale, scale);
                    drawnNodes.Add(newNode);
                }
            }
        }
    }
}
