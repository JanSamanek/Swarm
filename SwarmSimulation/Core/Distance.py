import numpy as np

class DistanceHelper:
    
    @staticmethod
    def CalculateEuclideanDistance(center1, center2):
        distance = np.linalg.norm(np.array(center1) - np.array(center2)).astype(np.int64)
        return distance
    
    @staticmethod
    def CalculateSquaredEuclideanDistance(center1, center2):
        distance = DistanceHelper.CalculateEuclideanDistance(center1, center2) ** 2
        return distance