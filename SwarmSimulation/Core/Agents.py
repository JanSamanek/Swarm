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
        return positionDelta
        
    def Draw(self, screen, color=(255, 255, 255)):
        pygame.draw.rect(screen, color, self)
        
    def DrawPerceptionRadius(self, screen, color=(0, 0, 255, 128)):
        pygame.draw.circle(screen, (0,0,0), self.center, self.perceptionRange + 2)
        pygame.draw.circle(screen, color, self.center, self.perceptionRange)
        
    def DrawAgentsInPerceptionRange(self, screen, color=(0, 255, 0)):
        for agentInRange in self.agentsInPerceptionRange:
            agentInRange.Draw(screen, color)

max_distance_temporary = 200 # TODO: remove
class CAPFAgent(Agent):
    
    def __init__(self, startPos, ID, perceptionRange):
        super().__init__(startPos, ID, perceptionRange)
        self.consensus = None
        self.speed = np.array([0,0])
    
    def CalculateControlInput(self, dt, numberOfAgents, enclosingPoint, consensusGain, p, gain):
        
        self.UpdateConsensus(dt, enclosingPoint, consensusGain, p)  
        
        distance = DistanceHelper.CalculateSquaredEuclideanDistance(enclosingPoint, self.center)
        controlInput = -2*(np.array(self.center) - np.array(enclosingPoint)) \
                         *((distance/(max_distance_temporary**2))**(p-1)) \
                         /((numberOfAgents*self.consensus)**((p-1)/p))

        if gain is not None:
            controlInput = gain * controlInput
        
        return controlInput
      
    def InitConsensus(self, enclosingPoint):
        self.consensus = DistanceHelper.CalculateSquaredEuclideanDistance(enclosingPoint, self.center)
    
    def UpdateConsensus(self, dt, enclosingPoint, consensusGain, p):          
        consensusDot = 0
        for agent in self.agentsInPerceptionRange:
            consensusDot += consensusGain*(agent.consensus - self.consensus)
        distance = DistanceHelper.CalculateSquaredEuclideanDistance(enclosingPoint, self.center)
        consensusDot += 2*p*((distance/(max_distance_temporary**2))**(p-1))* \
                        (np.array(self.centerx) - np.array(enclosingPoint)).dot(self.speed)

        self.consensus += consensusDot*dt
        print(f"{self.ID} : {self.consensus}")
        
    def Move(self, controlInput, dt):
        self.speed = controlInput
        print(f"{self.ID} : {self.speed*dt}")
        super().Move(controlInput, dt)


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

        