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
    private void Update() { //TODO make this more efficient
        BoidAgent selectedAgent = GlobalManager.Instance.selectedAgent;
        if (selectedAgent != prevSelected) {
            prevSelected = selectedAgent;
            Genome genome = selectedAgent.genome;
            for (int i = 0; i < genome.layerRows.Count; i++) {
                drawnNodes.Add(Instantiate(
                    nodePrefab,
                    new Vector3(0, 0, 0),
                    Quaternion.identity,
                    transform));
            }
        }
    }
}
