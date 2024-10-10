if __name__ == "__main__":
    import pygame
    from Core.Swarm import SwarmManager
    import json
    
    with open('Core/settings.json', 'r') as file:
        settings = json.load(file)
            
    pygame.init()
    
    SCREEN_WIDTH = 800
    SCREEN_HEIGHT = 600
    screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
    
    swarmManager = SwarmManager()
    
    swarmManager.AddAgentToSwarm((380, 370))
    swarmManager.AddAgentToSwarm((400, 310))
    swarmManager.AddAgentToSwarm((480, 300))
    swarmManager.AddAgentToSwarm((345, 320))
    swarmManager.AddAgentToSwarm((520, 340))
    swarmManager.AddAgentToSwarm((325, 380))
    swarmManager.AddAgentToSwarm((430, 270))
    
    clock = pygame.time.Clock()
    FPS = 20
    
    run = True
    while run:
        
        dt = clock.tick(FPS) / 1000
        
        swarmManager.DrawAgents(screen, drawPerceptionRadiuses=False)
        swarmManager.APF(dt, desiredDistance=settings["desiredDistance"])
        
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                run = False
    
        pygame.display.update()
        
        screen.fill((0, 0, 0))
        
    pygame.quit()
    