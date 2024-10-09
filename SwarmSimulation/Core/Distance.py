import numpy as np

class DistanceHelper:
    
    @staticmethod
    def CalculateEuclideanDistance(robot1, robot2):
        distance = np.linalg.norm(np.array(robot1.center) - np.array(robot2.center))
        return distance
    
    @staticmethod
    def CalculateSquaredEuclideanDistance(robot1, robot2):
        distance = np.sum((np.array(robot1.center) - np.array(robot2.center)) ** 2)
        return distance