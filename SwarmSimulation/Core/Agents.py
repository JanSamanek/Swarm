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

class CAPFAgent(Agent):
    
    def __init__(self, startPos, ID, perceptionRange):
        super().__init__(startPos, ID, perceptionRange)
        self.consensus = None
        self.speed = np.array([0,0])
        
    def CalculateOverallError(self, desiredDistance):
        distanceError = 0
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self, agent)
            distanceError += distance - desiredDistance
        return distanceError
    
    def CalculateControlInput(self, dt, numberOfAgents, desiredDistance, consensusGain, mixingFunctionPower, saturation):
        
        self.UpdateConsensus(dt, desiredDistance, consensusGain, mixingFunctionPower)  
        
        sum_1 = 0
        sum_2 = 0
        controlInput = np.array([0,0]).astype(np.float64)
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self, agent)
            distanceError = distance - desiredDistance
            sum_1 += distanceError**2 
            sum_2 += distanceError * (np.array(self.center) - np.array(agent.center)) / distance 
        
        controlInput = 2*(sum_1**(mixingFunctionPower-1))*sum_2
        controlInput /= ((numberOfAgents*self.consensus)**((mixingFunctionPower-1)/mixingFunctionPower))
        
        if saturation is not None:
            controlInput = np.clip(controlInput, -saturation, saturation)
        
        return -controlInput
      
    def InitConsensus(self, desiredDistance):
        self.consensus = self.CalculateOverallError(desiredDistance)
    
    def UpdateConsensus(self, dt, desiredDistance, consensusGain, mixingFunctionPower):          
        sum_1 = 0
        sum_2 = 0
        sum_3 = 0
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self, agent)
            distanceError = distance - desiredDistance
            sum_1 += consensusGain*(agent.consensus - self.consensus)
            sum_2 += distanceError**2 
            sum_3 += distanceError * (np.array(self.center) - np.array(agent.center)).dot(self.speed - agent.speed) / distance 
    
        consensusDot = sum_1 + 2*mixingFunctionPower*(sum_2**(mixingFunctionPower-1))*sum_3
        self.consensus += consensusDot * dt
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

        