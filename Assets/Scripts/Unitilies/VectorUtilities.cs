using UnityEngine;

public static class VectorUtilities
{
    /// <summary>
    /// Checks if the angle between two vectors is less than the given threshold.
    /// </summary>
    /// <param name="vectorA">The first vector.</param>
    /// <param name="vectorB">The second vector.</param>
    /// <param name="threshold">The angle threshold in degrees.</param>
    /// <returns>True if the angle is less than the threshold; otherwise, false.</returns>
    public static bool IsAngleLessThan(Vector3 vectorA, Vector3 vectorB, float threshold)
    {
        // Ensure the vectors are normalized to get accurate results
        vectorA.Normalize();
        vectorB.Normalize();

        // Calculate the angle between the vectors
        float angle = Vector3.Angle(vectorA, vectorB);

        // Check if the angle is less than the threshold
        return angle < threshold;
    }
}