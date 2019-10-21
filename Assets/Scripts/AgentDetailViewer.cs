using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AgentBehavior.NeuralNetwork;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.XR.WSA.Input;

[RequireComponent(typeof(UILineRenderer))]
public class AgentDetailViewer : MonoBehaviour {
    public GameObject nodePrefab;
    public GameObject lineRendererPrefab;
    private BoidAgent prevSelected = null;
    private ArrayList drawnNodes = new ArrayList();
    private Dictionary<Node, GameObject> nodeToObj = new Dictionary<Node, GameObject>();
    private Dictionary<string, Node> objNameToNode = new Dictionary<string, Node>();
    private void OnGUI() { //TODO make this more efficient
        BoidAgent selectedAgent = GlobalManager.Instance.selectedAgent;
        if (selectedAgent != prevSelected) {  //if we are selecting a new agent on this step
            prevSelected = selectedAgent;
            nodeToObj = new Dictionary<Node, GameObject>();
            objNameToNode = new Dictionary<string, Node>();
            drawnNodes = new ArrayList();
            Debug.Log("new selected");
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
            
            var genomeLayerRows = genome.layerRows;
            var maxNodes = -1;
            foreach (ArrayList layerRow in genomeLayerRows) {
                maxNodes = Math.Max(maxNodes, layerRow.Count);
            }
            var scale = Mathf.Min((20f / maxNodes), 5);
            var nodeIndex = 0;
            for (int i = 0; i < genomeLayerRows.Count; i++) {
                var genomeLayer = (ArrayList) genomeLayerRows[i];
                for (int j = 0; j < genomeLayer.Count; j++) {
                    var node = (Node) genomeLayer[j];
                    var nodePosition = screenBottomLeft;
                    nodePosition.x += (i+0.5f)*scale*(1.25f);
                    if (genomeLayer.Count < 4) {
                        // for 1 through 3, space them out on the 1/2 mark for 1, 1/3rd marks for 2, 1/4th marks for 3
                        nodePosition.y = nodePosition.y + 
                                         ((j+1)* ((Math.Abs(screenTopRight.y) + Math.Abs(screenBottomLeft.y))/(genomeLayer.Count+1)));
                    }
                    else {
                        nodePosition.y -= scale*(j + 0.5f);
                    }
                    var newNode = Instantiate(
                        nodePrefab,
                        nodePosition,
                        Quaternion.identity,
                        transform);
                    newNode.transform.localScale = new Vector3(scale, scale, scale);
                    newNode.name = "Node" + nodeIndex++;
                    var image = newNode.GetComponent<Image>();
                    image.color = WeightToColor(node.GetActivation());
                    nodeToObj.Add(node, newNode);
                    objNameToNode.Add(newNode.name, node);
                    drawnNodes.Add(newNode);
                }
            }
            
            var connections = genome.connections;
            foreach (Connection connection in connections) {
                var lineRendererPos = transform.position;
                lineRendererPos.z = -1;
                GameObject lineRendererObj = Instantiate(lineRendererPrefab, lineRendererPos, Quaternion.identity, transform);
                lineRendererObj.transform.SetAsFirstSibling();
                var lineRenderer = lineRendererObj.GetComponent<UILineRenderer>();
                lineRenderer.color = WeightToColor(connection.weight);
                lineRenderer.Points = new Vector2[2];
                lineRenderer.lineList = false;
                lineRenderer.Points[0] = nodeToObj[connection.inNode].gameObject.transform.localPosition;
                lineRenderer.Points[1] = nodeToObj[connection.outNode].gameObject.transform.localPosition;
            }

        }
        else {
            if (drawnNodes.Count > 0) {
                foreach (GameObject nodeObj in drawnNodes) {
                    var node = objNameToNode[nodeObj.name];
                    var image = nodeObj.GetComponent<Image>();
                    image.color = WeightToColor(node.GetActivation());
                }
            }
        }
    }

    /// <summary>
    /// Converts an float in range -1 to 1 into a color on a gradient of blue to red
    /// </summary>
    /// <param name="weight"></param>
    /// <returns></returns>
    private static Color WeightToColor(float weight) {
        // blue: 0, 148, 255
        // red: 255, 20, 28
//        var r = map(weight, -1, 1, 0, 255);
//        var g = map(weight, -1, 1, 148, 20);
//        var b = map(weight, -1, 1, 255, 28);
        var r = map(weight, -1, 1, 0, 1);
//        var g = 0;
        var b = map(weight, -1, 1, 1, 0);
//        var a = map(weight, -1, 1, 255, 28);
        return new Color(r, 0, b, 1);
    }

    private static float map(float x, float inMin, float inMax, float outMin, float outMax) {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
