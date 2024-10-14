import numpy as np

class ConsensusFiltr:
    
    def __init__(self):
        pass
    
    @staticmethod
    def CalculateConsensusDot(agent : object, 
                                 neighbours : list[object], 
                                 distance : int, 
                                 distanceVector : np.array, 
                                 agentSpeed : np.array, 
                                 consensusGain : int, 
                                 p : int):
        
        NORM = 300**2 # TODO: remove

        consensusDot = 0
        for neighbour in neighbours:
            consensusDot += neighbour.oldConsensus - agent.oldConsensus
        consensusDot *= consensusGain
        consensusDot += 2*p*((distance/(NORM))**(p-1))*distanceVector.dot(agentSpeed)
        return consensusDot