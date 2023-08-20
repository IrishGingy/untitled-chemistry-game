using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public class FishWeightGenerator : MonoBehaviour
//{
//    public float meanWeight = 10f;        // Mean weight of fish in pounds
//    public float stdDeviation = 2f;       // Standard deviation of fish weights

//    public float minWeight = 1f;          // Minimum possible weight
//    public float maxWeight = 30f;         // Maximum possible weight

//    private void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            // Generate a random value following a Gaussian distribution
//            float gaussianValue = GenerateGaussianRandom();

//            float normalizedValue = (gaussianValue + 1f) / 2f;  // Normalize to [0, 1]

//            // Map the Gaussian value to the desired weight range
//            float randomWeight = Mathf.Lerp(minWeight, maxWeight, normalizedValue);

//            Debug.Log("Generated Gaussian Value: " + gaussianValue);
//            Debug.Log("Normalized Value: " + normalizedValue);
//            Debug.Log("Generated Weight: " + randomWeight);
//        }
//    }

//    private float GenerateGaussianRandom()
//    {
//        // Generate two uniform random values between 0 and 1
//        float u1 = Random.value;
//        float u2 = Random.value;

//        // Transform the uniform random values to a Gaussian distribution using Box-Muller transform
//        float z0 = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Cos(2f * Mathf.PI * u2);

//        // Map the transformed value to the range [-1, 1] to get a standard Gaussian distribution
//        float gaussianValue = z0;

//        return gaussianValue;
//    }
//}

public class FishWeightGenerator : MonoBehaviour
{
    public AnimationCurve weightCurve; // Assign this in the inspector
    public float minWeight = 1f;
    public float maxWeight = 10f;

    private float avgWeight;

    public (float, float) CalculateWeightAndPoints(FishType fType)
    {
        // calculate weight
        //float randomTime = Random.value; // Generate random time [0, 1]
        //float curveValue = weightCurve.Evaluate(randomTime);
        avgWeight = fType.avgWeight;
        float posStDev = avgWeight / 2.0f;
        float negStDev = -(avgWeight / 2.0f);
        float weight = Random.Range(avgWeight + negStDev, avgWeight + posStDev);
        Debug.Log(weight);


        // Map curve value to weight range
        //float randomWeight = Mathf.Lerp(minWeight, maxWeight, curveValue);

        // calculate point value (TODO: CHANGE THIS TO ACTUALLY CALCULATE POINT VALUE)
        float pointValue = 4;

        return (weight, pointValue);
    }
}
