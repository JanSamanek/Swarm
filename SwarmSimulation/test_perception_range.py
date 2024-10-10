if __name__ == "__main__":
    import pygame
    from Core.Swarm import SwarmManager
    
    pygame.init()
    
    SCREEN_WIDTH = 800
    SCREEN_HEIGHT = 600
    
    screen = pygame.display.set_mode((SCREEN_WIDTH, SCREEN_HEIGHT))
    
    robotManager = SwarmManager()
    
    robotManager.AddAgentToSwarm((380, 370))
    robotManager.AddAgentToSwarm((400, 310))
    robotManager.AddAgentToSwarm((480, 300))
    robotManager.AddAgentToSwarm((200, 180))
    robotManager.AddAgentToSwarm((520, 340))
    robotManager.AddAgentToSwarm((325, 380))
    robotManager.AddAgentToSwarm((430, 270))
    robotManager.AddAgentToSwarm((200, 450))
    robotManager.AddAgentToSwarm((450, 200))
    
    GREEN = "\033[92m"
    RESET = "\033[0m"
    
    print(f"{GREEN} TEST : perception range {RESET}")
    
    run = True
    while run:
        robotManager.UpdateAgentsInPerceptionRange()
        for i in range(robotManager.agentCounter -1):
            robot = robotManager.agents[i]
            runInner = True
            while(runInner):
                robot.DrawPerceptionRadius(screen)
                robotManager.DrawAgents(screen, drawPerceptionRadiuses=False)
                robot.DrawAgentsInPerceptionRange(screen)
                robot.Draw(screen, (255, 0, 0))
            
                for event in pygame.event.get():
                    if event.type == pygame.KEYDOWN:
                        runInner = False
                        if event.key == pygame.K_ESCAPE:
                            run = False
                
                pygame.display.update()
                
                screen.fill((0, 0, 0))
        
    pygame.quit()