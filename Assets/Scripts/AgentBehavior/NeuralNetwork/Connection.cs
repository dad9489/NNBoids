namespace AgentBehavior.NeuralNetwork {
    public class Connection {
        private Node _inNode;
        private Node _outNode;
        private float _weight;
        private int _connectionInnovationNumber;
        
        public Connection(Node inNode, Node outNode, float weight, int connectionInnovationNumber) {
            _inNode = inNode;
            _outNode = outNode;
            _weight = weight;
            _connectionInnovationNumber = connectionInnovationNumber;
        }

        public float ActivateBack() {
            return _weight * _inNode.GetActivation();
        }
    }
}
