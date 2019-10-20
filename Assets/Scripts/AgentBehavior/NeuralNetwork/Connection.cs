namespace AgentBehavior.NeuralNetwork {
    public class Connection {
        public Node inNode;
        public Node outNode;
        public float weight;
        private int _connectionInnovationNumber;
        
        public Connection(Node inNode, Node outNode, float weight, int connectionInnovationNumber) {
            this.inNode = inNode;
            this.outNode = outNode;
            this.weight = weight;
            _connectionInnovationNumber = connectionInnovationNumber;
        }

        public float ActivateBack() {
            return weight * inNode.GetActivation();
        }
    }
}
