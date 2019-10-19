using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AgentBehavior.NeuralNetwork {
    public class Genome
    {
        private ArrayList _nodes = new ArrayList();
        private ArrayList _inputs = new ArrayList();
        private ArrayList _outputs = new ArrayList();
        private ArrayList _connections = new ArrayList();
        private InnovationIndexer _innovationIndexer;
        
        public Genome(int numInputs, int numOutputs, InnovationIndexer innovationIndexer) {
            _innovationIndexer = innovationIndexer;
            for (int i = 0; i < numInputs; i++) {
                var newInput = new Node(Node.Type.Input, _innovationIndexer.GetNextNodeInnovation());
                _nodes.Add(newInput);
                _inputs.Add(newInput);
            }
            for (int i = 0; i < numOutputs; i++) {
                var newOutput = new Node(Node.Type.Output, _innovationIndexer.GetNextNodeInnovation());
                _nodes.Add(newOutput);
                _outputs.Add(newOutput);
            }
            for (int i = 0; i < numInputs; i++) {
                for (int j = 0; j < numOutputs; j++) {
                    AddConnection((Node) _inputs[i], (Node) _outputs[j], Random.Range(-1f, 1f));
                }
            }
        }
        
        /// <summary>
        /// Given a vector of input values, returns a Vector2 of the evaluated position
        /// using the network's brain to "think".
        /// </summary>
        /// <param name="inputValues">A vector of containing TODO what are the inputs? </param>
        /// <returns></returns>
        public Vector2 Evaluate(List<float> inputValues) {
            if (_inputs.Count != inputValues.Count) {
                Debug.LogError("Mismatch of network inputs and inputted values");
            }
            for (int i = 0; i < inputValues.Count; i++) {
                Node node = (Node) _inputs[i];
                node.SetActivation(inputValues[i]);
            }
            List<float> outputValues = new List<float>();
            for (int i = 0; i < _outputs.Count; i++) {
                Node node = (Node) _outputs[i];
                outputValues.Add(node.GetActivation());
            }
            return new Vector2(outputValues[0], outputValues[1]);
        }

        private void AddConnection(Node fromNode, Node toNode, float weight) {
            Connection connection = new Connection(fromNode, toNode, weight,
                _innovationIndexer.GetNextConnectionInnovation());
            toNode.AddInput(connection);
            _connections.Add(connection);
        }
    }
}
