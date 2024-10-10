from Core.Agents import APFAgent, CAPFAgent
from Core.Distance import DistanceHelper
import json

class SwarmManager:
    
    def __init__(self):
        self.agents = []
        self.agentCounter = 0
        
        with open('Core/settings.json', 'r') as file:
            self.settings = json.load(file)
        
        self.UpdateAgentsInPerceptionRange()
    
    def UpdateAgentsInPerceptionRange(self):
        for agentToUpdate in self.agents:
            agentToUpdate.agentsInPerceptionRange = []
            for agent in self.agents:
                if agentToUpdate.ID != agent.ID:
                    distance = DistanceHelper.CalculateEuclideanDistance(agentToUpdate, agent)
                    if distance <= agentToUpdate.perceptionRange:
                        agentToUpdate.agentsInPerceptionRange.append(agent)
                        
    def DrawAgents(self, screen, drawPerceptionRadiuses=False):
        if drawPerceptionRadiuses:
            for agent in self.agents:
                agent.DrawPerceptionRadius(screen)     
        for agent in self.agents:
            agent.Draw(screen)
            

class SwarmManagerAPF(SwarmManager):
                      
    def __init__(self):
        super().__init__()
        self.apfSettings = self.settings["APF"]
        
    def UpadteRobotPositions(self, dt, desiredDistance):
        self.UpdateAgentsInPerceptionRange()
        for agent in self.agents:
            controlInput = agent.CalculateControlInput(desiredDistance, 
                                                       self.apfSettings["gain"], 
                                                       self.apfSettings["saturation"], 
                                                       self.apfSettings["deadzone"])
            agent.Move(controlInput, dt)
    
    
    def CreateAgent(self, startPos):
        self.agents.append(APFAgent(startPos, 
                                    ID=self.agentCounter, 
                                    perceptionRange=self.apfSettings["perceptionRange"]))
        self.agentCounter += 1 
        
class SwarmManagerCAPF(SwarmManager):
    
    def __init__(self):
        super().__init__()
        self.capfSettings = self.settings["CAPF"]
        self.agents : list[CAPFAgent]    
    
    def CreateAgent(self, startPos):
        self.agents.append(CAPFAgent(startPos, 
                                     ID=self.agentCounter, 
                                     perceptionRange=self.capfSettings["perceptionRange"]))
        self.agentCounter += 1 
    
    
    def InitAgentConsensuses(self, desiredDistance):
        self.UpdateAgentsInPerceptionRange()
        for agent in self.agents:
            agent.InitConsensus(desiredDistance)
        
    def UpadteRobotPositions(self, dt, desiredDistance):
        self.UpdateAgentsInPerceptionRange()
        
        for agent in self.agents:
            controlInput = agent.CalculateControlInput(dt, 
                                                       self.agentCounter, 
                                                       desiredDistance, 
                                                       self.capfSettings["consensusGain"], 
                                                       self.capfSettings["mixingFunctionPower"],
                                                       self.capfSettings["saturation"])
            agent.Move(controlInput, dt)