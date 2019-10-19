using UnityEngine;

namespace AgentBehavior.NeuralNetwork {
    public class InnovationIndexer : MonoBehaviour
    {
        private int _nodeInnovationNum = 0;
        private int _connectionInnovationNum = 0;

        public int GetNextNodeInnovation() {
            return _nodeInnovationNum++;
        }

        public int GetNextConnectionInnovation() {
            return _connectionInnovationNum++;
        }
    }
}
