import numpy as np

class DistanceHelper:
    
    @staticmethod
    def CalculateEuclideanDistance(robot1, robot2):
        distance = np.linalg.norm(np.array(robot1.center) - np.array(robot2.center))
        return distance
    
    @staticmethod
    def CalculateSquaredEuclideanDistance(center1, center2):
        distance = np.sum((np.array(center1) - np.array(center2)) ** 2)
        return distance