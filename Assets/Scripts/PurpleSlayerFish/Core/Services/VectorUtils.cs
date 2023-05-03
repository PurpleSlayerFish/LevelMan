using UnityEngine;

namespace PurpleSlayerFish.Core.Services
{
    public struct VectorUtils
    {
        private MathUtils _mathUtils;
        public bool InDistance(Vector3 deltaPosition, float distance) => deltaPosition.sqrMagnitude <= Mathf.Pow(distance, 2);
        
        
        public bool IsPointInsideBox(Vector3 pointToCheck, Vector3 pointA, Vector3 pointB) => 
            pointToCheck.x >= Mathf.Min(pointA.x, pointB.x) && pointToCheck.x <= Mathf.Max(pointA.x, pointB.x) &&
            pointToCheck.y >= Mathf.Min(pointA.y, pointB.y) && pointToCheck.y <= Mathf.Max(pointA.y, pointB.y) &&
            pointToCheck.z >= Mathf.Min(pointA.z, pointB.z) && pointToCheck.z <= Mathf.Max(pointA.z, pointB.z);

        //todo
        public bool CheckRayIntersection(Vector2 rayOrigin, float rayRotation, Vector2 point, float offset)
        {
            Vector2 pointToCircle = point - rayOrigin;
            float dotProduct = Vector2.Dot(pointToCircle, _mathUtils.Direction2dFromRotate(rayRotation));
    
            if (dotProduct < 0)
                return false;
            if (pointToCircle.sqrMagnitude - dotProduct * dotProduct > offset * offset)
                return false;
            return true;
        }
    }
}