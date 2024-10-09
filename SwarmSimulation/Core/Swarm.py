from Core.Distance import DistanceHelper
import json
import pygame
import numpy as np

class SwarmManager:
    
    def __init__(self) -> None:
        self.agents : list[Agent] = []
        self.agentCounter = 1
        
        with open('Core/settings.json', 'r') as file:
            settings = json.load(file)

        swarmSettings = settings["swarmSettings"]
        alogrithmSettings = swarmSettings["algorithmSettings"]
        self.damping = alogrithmSettings["damping"]
        self.saturation = alogrithmSettings["saturation"]
        self.deadzone = alogrithmSettings["deadzone"]
        
        
    def CreateAgent(self, startingPos : tuple[int, int]):
        self.agents.append(Agent(startingPos, ID=self.agentCounter))
        self.agentCounter += 1 
                        
    def UpdateAgentPositions(self, dt, desiredDistance):
        self.UpdateAgentsInPerceptionRange()
        for agent in self.agents:
            (controlInputX, controlInputY) = agent.APF(desiredDistance, self.damping, self.saturation, self.deadzone)
            agent.Move(controlInputX, controlInputY, dt)
    
    def UpdateAgentsInPerceptionRange(self):
        for agentToUpdate in self.agents:
            for agent in self.agents:
                if agentToUpdate.ID != agent.ID:
                    distance = DistanceHelper.CalculateEuclideanDistance(agentToUpdate, agent)
                    if distance <= agentToUpdate.perceptionRange:
                        agentToUpdate.UpdateAgentsInPerceptionRange(agent)
                        
    def DrawAgents(self, screen, drawPerceptionRadiuses=False):
        if drawPerceptionRadiuses:
            for agent in self.agents:
                agent.DrawPerceptionRadius(screen)     
        for agent in self.agents:
            agent.Draw(screen)
            
            
class Agent(pygame.Rect):
    
    def __init__(self, startPos, ID, perceptionRange=200):
        super().__init__((*startPos, 10, 10))        
        self.perceptionRange : float = perceptionRange
        self.ID : int = ID
        self.agentsInPerceptionRange : list[Agent] = []
        self.consensus : float = ConsensusFiltr.CalculateMainConsensus(self)
        self.consensusFiltr : ConsensusFiltr = ConsensusFiltr(self.consensus)
                
    def UpdateAgentsInPerceptionRange(self, agent):
        self.agentsInPerceptionRange.append(agent)
        
    def Move(self, speedX, speedY, dt):
        positionDelta = (speedX*dt, speedY*dt)
        self.move_ip(positionDelta)
        
    def APF(self, desiredDistance, damping=None, saturation=None, deadzone=None):
        xControlInput, yControlInput = 0, 0
        
        for agent in self.agentsInPerceptionRange:
            distance = DistanceHelper.CalculateEuclideanDistance(self, agent)
            distance_error = desiredDistance - distance
            
            if deadzone is None or deadzone <= abs(distance_error):
                xControlInput += (distance_error / distance) * (self.centerx - agent.centerx) 
                yControlInput += (distance_error / distance) * (self.centery - agent.centery)
                        
        if damping is not None:
            xControlInput = damping * xControlInput
            yControlInput = damping * yControlInput
            
        if saturation is not None:
            xControlInput = np.clip(xControlInput, -saturation, saturation)
            yControlInput = np.clip(yControlInput, -saturation, saturation)
            
        return (xControlInput, yControlInput)

    def CAPF(self, desiredDistance, mixingFunctionPower):
        pass        
    
    def Draw(self, screen, color=(255, 255, 255)):
        pygame.draw.rect(screen, color, self)
        
    def DrawPerceptionRadius(self, screen, color=(0, 0, 255, 128)):
        pygame.draw.circle(screen, color, self.center, self.perceptionRange)
        
    def DrawAgentsInPerceptionRange(self, screen, color=(0, 255, 0)):
        for agentInRange in self.agentsInPerceptionRange:
            agentInRange.Draw(screen, color)
        
    
class ConsensusFiltr():
    
    def __init__(self, initValue):
        self.consensus = initValue
        
    def UpdateConsensus(self, agent : Agent, desiredDistance : float, mixingFunctionPower : int):
        consensusUpdate = 0
        for agent in agent.agentsInPerceptionRange:
            pass
    
    @staticmethod
    def CalculateMainConsensus(agent : Agent):
        consensusMain = 0
        for agentInPerceptionRange in agent.agentsInPerceptionRange:
            consensusMain +=  agentInPerceptionRange.consensus - agent.consensus 
        return consensusMain