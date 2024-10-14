if __name__ == "__main__":
    import pygame
    from Core.Swarm import SwarmManagerCAPF
    import json
    
    with open('Core/settings.json', 'r') as file:
        settings = json.load(file)
    capfSettings = settings["CAPF"]
    consensusSettings = capfSettings["consensusFiltr"]
    perceptionRange = capfSettings["perceptionRange"]
    enclosingPoint = capfSettings["enclosingPoint"]
            
    pygame.init()
    
    SCREEN_WIDTH = 800
    SCREEN_HEIGHT = 600
    screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
    
    swarmManager = SwarmManagerCAPF()
    swarmManager.CreateAgent(startPos=(632, 290))
    swarmManager.CreateAgent(startPos=(595, 405))
    swarmManager.CreateAgent(startPos=(485, 512))
    swarmManager.CreateAgent(startPos=(260, 508))
    swarmManager.CreateAgent(startPos=(375, 530))
    swarmManager.CreateAgent(startPos=(275, 465))
    swarmManager.CreateAgent(startPos=(160, 385))
    swarmManager.CreateAgent(startPos=(235, 220))
    swarmManager.CreateAgent(startPos=(398, 134))
    swarmManager.CreateAgent(startPos=(515, 155))
    swarmManager.CreateAgent(startPos=(625, 250))
    swarmManager.CreateAgent(startPos=(485, 470))
    swarmManager.CreateAgent(startPos=(340, 155))
    swarmManager.CreateAgent(startPos=(585, 190))
    swarmManager.CreateAgent(startPos=(190, 290))

    swarmManager.InitAgentConsensuses(enclosingPoint)
    
    clock = pygame.time.Clock()
    FPS = 20
        
    run = True
    while run:
        dt = clock.tick(FPS) / 1000

        pygame.draw.rect(screen, (255, 0, 0), pygame.Rect(*enclosingPoint, 5, 5))

        swarmManager.DrawAgents(screen, drawPerceptionRadiuses=False)
        swarmManager.UpdateNewConsensuses(dt, enclosingPoint, consensusSettings["consensusGain"], consensusSettings["mixingFunctionPower"])
        swarmManager.UpdateAgentsPositions(dt, enclosingPoint=enclosingPoint)
        swarmManager.UpdateOldConsensuses()
        
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                run = False
    
        pygame.display.update()
        
        screen.fill((0, 0, 0))
        
    pygame.quit()
    