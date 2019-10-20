using System;
using System.Collections;
using System.Collections.Generic;
using AgentBehavior.NeuralNetwork;
using UnityEngine;
using UnityEngine.UI;

public class AgentDetailViewer : MonoBehaviour {
    private BoidAgent prevSelected = null;
    private ArrayList drawnNodes = new ArrayList();
    public GameObject nodePrefab;
    private int scale = 4;
    private void Update() { //TODO make this more efficient
        BoidAgent selectedAgent = GlobalManager.Instance.selectedAgent;
        if (selectedAgent != prevSelected) {  //if we are selecting a new agent on this step
            prevSelected = selectedAgent;
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
            
            for (int i = 0; i < genome.layerRows.Count; i++) {
                var nodePosition = screenTopRight;
                nodePosition.x -= scale*(0.5f);
                nodePosition.y -= scale*(i + 0.5f);
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
