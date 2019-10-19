using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AgentBehavior.NeuralNetwork {
    public class Node {
        public enum Type {
            Input,
            Output,
            Hidden
        }
        private Type _type;
        private int _nodeInnovationNum;
        private ArrayList _inputs;
        private float _activation;

        public Node(Type type, int nodeInnovationNum) {
            _type = type;
            _nodeInnovationNum = nodeInnovationNum;
            _inputs = new ArrayList();
        }

        public void AddInput(Connection inputConnection) {
            _inputs.Add(inputConnection);
        }

        public float GetActivation() {
            if (_type == Type.Input) {
                return _activation;
            }
            var sum = 0f;
            foreach (Connection input in _inputs) {
                sum += input.ActivateBack();
            }
            return sigmoid(sum);
        }

        public void SetActivation(float activation) {
            if (_type != Type.Input) {
                Debug.LogError("setActiation() called on node that isn't an input");
            } else {
                _activation = activation;
            }
        }

        private float sigmoid(float x) {
            return (float)((2 / (Math.Exp(-x) + 1)) - 1);
        }
    }
}
