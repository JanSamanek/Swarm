if __name__ == "__main__":
    import pygame
    from Core.Swarm import SwarmManagerAPF
    from Core.Agents import APFAgent
    import json
    
    with open('Core/settings.json', 'r') as file:
        settings = json.load(file)
    apfSettings = settings["APF"]
    perceptionRange = apfSettings["perceptionRange"]
    desiredDistancee = apfSettings["desiredDistance"]
            
    pygame.init()
    
    SCREEN_WIDTH = 800
    SCREEN_HEIGHT = 600
    screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
    
    swarmManager = SwarmManagerAPF()
    
    swarmManager.CreateAgent(startPos=(380, 370))
    swarmManager.CreateAgent(startPos=(400, 310))
    swarmManager.CreateAgent(startPos=(480, 300))
    swarmManager.CreateAgent(startPos=(345, 320))
    swarmManager.CreateAgent(startPos=(520, 340))
    swarmManager.CreateAgent(startPos=(325, 380))
    swarmManager.CreateAgent(startPos=(430, 270))
    
    clock = pygame.time.Clock()
    FPS = 20
    
    run = True
    while run:
        
        dt = clock.tick(FPS) / 1000
        
        swarmManager.DrawAgents(screen, drawPerceptionRadiuses=False)
        swarmManager.UpadteRobotPositions(dt, desiredDistance=desiredDistancee)
        
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                run = False
    
        pygame.display.update()
        
        screen.fill((0, 0, 0))
        
    pygame.quit()
    