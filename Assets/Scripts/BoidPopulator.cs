using System.Collections;
using System.Collections.Generic;
using AgentBehavior.NeuralNetwork;
using UnityEngine;

public class BoidPopulator : MonoBehaviour
{
    public BoidAgent agentPrefab;
    public List<BoidAgent> agents = new List<BoidAgent>();
    public InnovationIndexer innovationIndexer;

    [Range(10, 1000)] public int numBoids = 250;
    [Range(0.01f, 0.1f)] public float density = 0.08f;    
    
    // Start is called before the first frame update
    void Start()
    { 
        innovationIndexer = gameObject.AddComponent<InnovationIndexer>();
        for (int i = 0; i < numBoids; i++)
        {
            BoidAgent boidAgent = Instantiate(
                agentPrefab,
                density * numBoids * Random.insideUnitCircle,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform);
            boidAgent.name = "Boid" + i;
            const int numInputs = 3;
            const int numOutputs = 2;
            boidAgent.genome= new Genome(numInputs, numOutputs, innovationIndexer);;
            agents.Add(boidAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
