import pygame          
from Core.Distance import DistanceHelper
import numpy as np

class Agent(pygame.Rect):
    
    def __init__(self, startPos, ID, perceptionRange):
        super().__init__((*startPos, 10, 10))        
        self.perceptionRange : int = perceptionRange
        self.ID : int = ID
        self.agentsInPerceptionRange : list[Agent] = []
        
    def Move(self, controlInput, dt):
        positionDelta = (controlInput[0]*dt, controlInput[1]*dt)
        self.move_ip(positionDelta)
        
    def Draw(self, screen, color=(255, 255, 255)):
        pygame.draw.rect(screen, color, self)
        
    def DrawPerceptionRadius(self, screen, color=(0, 0, 255, 128)):
        pygame.draw.circle(screen, (0,0,0), self.center, self.perceptionRange + 2)
        pygame.draw.circle(screen, color, self.center, self.perceptionRange)
        
    def DrawAgentsInPerceptionRange(self, screen, color=(0, 255, 0)):
        for agentInRange in self.agentsInPerceptionRange:
            agentInRange.Draw(screen, color)

class CAPFAgent(Agent):
    
    def __init__(self, startPos, ID, perceptionRange):
        super().__init__(startPos, ID, perceptionRange)
        self.consensus = self.CalculateOverallError()
        
    def CalculateControlInput(self, desiredDistance, mixingFunctionPower):
        pass        
            
    def CalculateOverallError(self, desiredDistance):
        distanceError = 0
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self, agent)
            distanceError += desiredDistance - distance
        return distanceError
    
    def UpdateConsensus(self, dt, consensusGain, mixingFunctionPower, speed):
        consensus_dot = 0
        for agent in self.agentsInPerceptionRange:
            consensus_dot += agent.consensus - self.consensus
        consensusGain *= consensusGain
        # TODO :
        
        


class APFAgent(Agent):

    def __init__(self, startPos, ID, perceptionRange):
        super().__init__(startPos, ID, perceptionRange)
        
    def CalculateControlInput(self, desiredDistance, gain=None, saturation=None, deadzone=None):
        controlInput = np.array([0, 0], dtype=np.float64)
        
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self, agent)
            distanceError = desiredDistance - distance
            
            if deadzone is None or deadzone <= abs(distanceError):
                controlInput += (distanceError / distance) * (np.array(self.center) - np.array(agent.center)).astype(np.float64) 
                        
        if gain is not None:
            controlInput = gain * controlInput
            
        if saturation is not None:
            controlInput = np.clip(controlInput, -saturation, saturation)
        
        return controlInput

        