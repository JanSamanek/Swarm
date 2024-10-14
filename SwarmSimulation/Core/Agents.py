import pygame          
from Core.Distance import DistanceHelper
from Core.Consensus import ConsensusFiltr
import numpy as np

class Agent(pygame.Rect):
    
    def __init__(self, startPos, ID, perceptionRange):
        super().__init__((*startPos, 10, 10))        
        self.perceptionRange : int = perceptionRange
        self.ID : int = ID
        self.agentsInPerceptionRange : list[Agent] = []
        
    def Move(self, controlInput, dt):
        positionDelta = np.array(controlInput*dt).astype(np.int64)
        self.move_ip(*positionDelta)
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
        self.newConsensus = None
        self.oldConsensus = None
        self.speed = np.array([0,0])
    
    def CalculateControlInput(self, n, o, p, gain):   

        distance = DistanceHelper.CalculateSquaredEuclideanDistance(o, self.center)
        distanceVector = np.array(self.center) - np.array(o)
        
        NORM = 300**2 # TODO: remove
        controlInput = -2 * distanceVector * (distance / (NORM))**(p-1) / ((n*self.newConsensus)**((p-1)/p))

        
        #########################################
        if self.ID == 0:
            print(f"controlInput : {controlInput}")    
        #########################################
        
        if gain is not None:
            controlInput = gain * controlInput
        
        # controlInput = np.clip(controlInput, -20, 20)
            
        return np.array(controlInput).astype(np.int64)
    
    def UpdateNewConsensus(self, dt, enclosingPoint, consensusGain, p):          
    
        distance = DistanceHelper.CalculateSquaredEuclideanDistance(enclosingPoint, self.center)
        distanceVector = np.array(self.center) - np.array(enclosingPoint)

        consensusDot = ConsensusFiltr.CalculateConsensusDot(self, 
                                                            self.agentsInPerceptionRange,
                                                            distance,
                                                            distanceVector,
                                                            self.speed,
                                                            consensusGain,
                                                            p)
        self.newConsensus += consensusDot*dt
        
        #########################################
        if self.ID == 0:
            print(f"consensus = {self.newConsensus}, consensusDot = {consensusDot}, distance = {distance}")
        #########################################

      
    def UpdateOldConsensus(self):
        self.oldConsensus = self.newConsensus
        
    def InitConsensus(self, enclosingPoint):
        distance = DistanceHelper.CalculateSquaredEuclideanDistance(enclosingPoint, self.center)
        self.oldConsensus = distance
        self.newConsensus = distance
        
    def Move(self, controlInput, dt):
        self.speed = controlInput    
        
        positionDelta = super().Move(controlInput, dt)

        #########################################
        if self.ID == 0:
            print(f"speed = {self.speed}, positionDelta = {positionDelta}")
        #########################################
    

class APFAgent(Agent):

    def __init__(self, startPos, ID, perceptionRange):
        super().__init__(startPos, ID, perceptionRange)
        
    def CalculateControlInput(self, desiredDistance, gain=None, saturation=None, deadzone=None):
        controlInput = np.array([0, 0], dtype=np.float64)
        
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self.center, agent.center)
            distanceError = desiredDistance - distance
            if deadzone is None or deadzone <= abs(distanceError):
                controlInput += (distanceError / distance) * (np.array(self.center) - np.array(agent.center)).astype(np.float64) 
                        
        if gain is not None:
            controlInput = gain * controlInput
            
        if saturation is not None:
            controlInput = np.clip(controlInput, -saturation, saturation)
        
        return controlInput

        