Deployment and Technical Overview of SpaceShip 2D Unity Game
3. Deployment Process of this 2D Unity Game

I followed the tutorial "Unity Tutorial: How to Make a 2D Space Shooter Game" by ZeroCool - Game Development ([link](https://www.youtube.com/@ZeroCoolGameDevelopment)) to deploy my SpaceShip 2D Unity game. There are multiple platforms available for deployment, but I chose itch.io as shown in the tutorial. Once deployed, I received a direct itch.io link to share with others so they can try out the game.

👉 My itch.io account: https://giaheplbro.itch.io/
👉 Game link on itch.io: [SpaceShip Game](https://giaheplbro.itch.io/spaceship)
4. Potential Improvements to this 2D Unity Game
New Features to Add:

    More levels with increasing difficulty.
    New enemy types with different behaviors and attack patterns.
    Additional weapons and ammunition types for more variety.
    Boss fights with unique attack mechanics.
    A spaceship upgrade system (e.g., speed, shield, firepower).

Bugs & Enhancements:

    Fix collision issues where the spaceship sometimes behaves unnaturally.
    Adjust enemy AI to make movement and attacks more dynamic.
    Improve visual effects and sound effects for better feedback.

5. Technicalities Between Unity and GitHub

To manage version control and share the game development process, I uploaded the project to GitHub. The video "How to Upload your Unity Projects to GitHub In 2023" by GDTitans ([link](https://www.youtube.com/watch?v=YymhtHtHDb8)) explains the process clearly.
Git Ignore in Unity

GitHub automatically ignores unnecessary files to save storage and improve version control. The .gitignore file for Unity excludes:

/Library/  
/Logs/  
/UserSettings/  
/Temp/  
/Build/  

These folders are not needed because Unity can regenerate them when importing the project.
How to Download and Open This Unity Project from GitHub

To download and open the game in Unity, I followed the tutorial "How to import GitHub projects into Unity!" by Unity Hero ([link](https://www.youtube.com/watch?v=I9QK_2QW9W8)).

After importing the project, I initially thought the scene was missing. However, the game scene was simply not loaded by default. To fix this, I navigated to Assets → Scenes in the Unity Editor and opened the main scene to restore everything.

This document provides a structured overview of how I deployed, improved, and managed my SpaceShip Unity game. 🚀 If needed, I can expand on any section with more details.


