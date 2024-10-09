if __name__ == "__main__":
    import pygame
    from Core.Swarm import SwarmManager
    
    pygame.init()
    
    SCREEN_WIDTH = 800
    SCREEN_HEIGHT = 600
    screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
    
    swarmManager = SwarmManager()
    
    swarmManager.CreateAgent((380, 370))
    swarmManager.CreateAgent((400, 310))
    swarmManager.CreateAgent((480, 300))
    swarmManager.CreateAgent((345, 320))
    swarmManager.CreateAgent((520, 340))
    swarmManager.CreateAgent((325, 380))
    swarmManager.CreateAgent((430, 270))
    
    clock = pygame.time.Clock()
    FPS = 20
    
    run = True
    while run:
        
        dt = clock.tick(FPS) / 1000
        
        swarmManager.DrawAgents(screen, drawPerceptionRadiuses=False)
        swarmManager.UpdateAgentPositions(dt, desiredDistance=100)
        
        for event in pygame.event.get():
            if event.type == pygame.QUIT:
                run = False
    
        pygame.display.update()
        
        screen.fill((0, 0, 0))
        
    pygame.quit()
    