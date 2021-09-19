# Galaga-Clone

Clone of the [Galaga](https://en.wikipedia.org/wiki/Galaga) game.

Controlling a starship, the player is tasked with destroying the Galaga forces in each stage while avoiding enemies and projectiles. Some enemies can capture a player's ship via a tractor beam, which can be rescued to transform the player into a "dual fighter" with additional firepower.

**Technologies:**
C#, Unity

**To Do:**
- [ ] **Visual styling.**
- [ ] Behaviour of the ship capturing.
- [ ] Polish the movement of the enemies.
- [ ] Expanding an enemy formation.
- [x] Behaviour of the ship/player.
- [x] Basic collision and wrap enemies on the screen.
- [x] Behaviour of the enemies units.
- [x] Basic movement of the enemies.


![galaga2](https://user-images.githubusercontent.com/13272856/127693051-8f819d2f-e7c2-400d-a237-86f20f5915e4.gif)

**Custom tools:**
- **Grid Editor** - a tool for generating the grid

![image](https://user-images.githubusercontent.com/13272856/133926862-b5ad5405-3728-4820-ae33-5a3fbeb9bc2f.png)
![image](https://user-images.githubusercontent.com/13272856/133926864-9e43a618-7459-4767-96cf-71be2e3b9433.png)

- **Waypont Editor** -  a tool for creating wayponts used for movement by pattern of the enemies.

When **Instantiate wayponts** is checked create a waypont by clicking on the screen. When ready, click **Save prefab**. Created prefab can be used in the **SpawnPoint** script.

![image](https://user-images.githubusercontent.com/13272856/133926937-3f742cdf-f766-453f-96ab-d72851ecda66.png)
![image](https://user-images.githubusercontent.com/13272856/133927036-9e933a9d-f0a5-421b-872c-8fd1889b1b5c.png)
![image](https://user-images.githubusercontent.com/13272856/133927093-817faa05-ab83-49b2-bfce-a5d6bb8efa3d.png)



