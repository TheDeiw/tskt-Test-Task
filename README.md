# Ukrainian Tank Destroyer
**Purpose of creation**: Created as test task.

## Project Overview
- **Description**: A game where the player controls a tank, destroys enemies using laser, avoids attacks, and aims to survive as long as possible.
- **Core Mechanics**:
  - Player movement and camera-based rotation.
  - Shooting lasers in enemies by clicking on space.
  - Enemy spawning around the player by clicking on E or button in UI.
  - Health system for player and enemies.
  - UI for displaying health and game-over screen.

## Project Structure

- **Scenes**:
  - Scene 0: Main menu.
  - Scene 1: Gameplay scene.
- **Key Objects (Prefabs)**:
  - `Player`: Player tank with `CharacterController` and `Animator` components.
    - `HealthBar`: Canvas, attached to the player, with slider which dedicates amount of health in player.
    - `TankWheels` and `TankGun`: Models of the player. `TankWheels` have moving animation. 
  - `Enemy`: Enemies that chase the player.
  - `Bullet`: Bullets fired by enemies.
  - `Laser`: Laser is fired by player.
  - `Explode`: Particle system object.
  - `UI`: Interface elements (Main Menu, LosePanel).

## Script Descriptions

### PlayerMovement.cs

#### Purpose

Manages player movement, camera-based rotation, and walking animations.

#### Components

- Attached to the player `GameObject`.
- Requires `CharacterController` and `Animator`.

#### Public Variables

- `controller`: `CharacterController` — Component for movement.
- `cam`: `Transform` — Camera transform for rotation.
- `speed`: `float` — Movement speed (default: 12f).
- `turnSmoothTime`: `float` — Smoothing time for rotation (default: 0.1f).
- `gravity`: `float` — Gravity value (default: -9.81f).
- `groundCheck`: `Transform` — Ground check position.
- `groundDistance`: `float` — Ground check radius (default: 0.4f).
- `groundMask`: `LayerMask` — Layer mask for ground.
- `animator`: `Animator` — For animations.
- `moveDirectionForGun`: `Vector3` — Movement direction for the gun.

#### Private Variables

- `velocity`: `Vector3` — Velocity vector for gravity.
- `isGrounded`: `bool` — Whether the player is grounded.
- `turnSmoothVelocity`: `float` — For rotation smoothing.

#### Methods

- `Update()`: Handles movement, gravity, and animations per frame.

#### Logic

1. Checks if the player is grounded using `Physics.CheckSphere`.
2. Gets input (`Horizontal`, `Vertical`) for movement.
3. Triggers walking animation via `animator.SetFloat`.
4. Calculates direction and rotation angle based on the camera.
5. Moves the player using `controller.Move`.
6. Applies gravity.

---

### EnemyLogic.cs

#### Purpose

Controls enemy movement toward the player.

#### Public Variables

- `speed`: `float` — Movement speed (default: 5f).

#### Private Variables

- `target`: `Transform` — Player’s transform.

#### Methods

- `Start()`: Finds the player by the "Player" tag.
- `Update()`: Moves the enemy toward the player.

#### Logic

1. Faces the player using `transform.LookAt`.
2. Moves toward the player at `speed`.

---

### BulletDetection.cs

#### Purpose

Handles damage from bullets and updates health.

#### Public Variables

- `maxHealth`: `int` — Maximum health.
- `healthBar`: `HealthBar` — Reference to the health bar.
- `explosionEffect`: `ParticleSystem` — Reference to explosion particle system.

#### Private Variables

- `currentHealth`: `int` — Current health.

#### Methods

- `Start()`: Initializes health and `HealthBar`.
- `Update()`: Destroys the object if health &lt;= 0.
- `OnTriggerEnter()`: Handles bullet collisions.

#### Logic

1. Reduces health on collision with a "Bullet" tagged object.
2. Updates `HealthBar`.
3. Destroys the bullet.

---

### EnemySpawner.cs

#### Purpose

Spawns enemies around the player.

#### Public Variables

- `enemyPrefab`: `GameObject` — Enemy prefab.
- `player`: `GameObject` — Reference to the player.
- `minSpawnRadius`: `float` — Minimum spawn radius (default: 10f).
- `maxSpawnRadius`: `float` — Maximum spawn radius (default: 20f).

#### Methods

- `Update()`: Calls `SpawnEnemy()` when "E" is pressed.
- `SpawnEnemy()`: Spawns an enemy at a random position in radius between 20 and 30 meters.

#### Logic

1. Generates a random angle and distance.
2. Checks if the spawn position is free using `Physics.CheckSphere`.
3. Instantiates the enemy via `Instantiate`.

---

### CheckDeath.cs

#### Purpose

Checks for player death and displays the game-over screen.

#### Public Variables

- `healthBar`: `HealthBar` — Reference to the health bar.
- `losepanel`: `GameObject` — Game-over panel.

#### Methods

- `Start()`: Initializes the game.
- `Update()`: Checks health status.

#### Logic

1. If health &lt;= 0, activates `losepanel` and pauses time.

---

### UI_Buttons.cs

#### Purpose

Manages UI buttons (play, menu, quit).

#### Methods

- `PlayGame()`: Loads the gameplay scene.
- `ToMenu()`: Returns to the main menu.
- `QuitGame()`: Exits the game.

#### Logic

Uses `SceneManager` and `Application.Quit`.

---

### BulletMovement.cs

#### Purpose

Controls bullet movement.

#### Private Variables

- `direction`: `Vector3` — Movement direction.
- `speed`: `float` — Bullet speed.

#### Methods

- `SetDirection()`: Sets direction and speed.
- `Update()`: Moves the bullet.
- `OnTriggerEnter()`: Destroys the bullet on wall collision.

#### Logic

1. Moves the bullet in the set direction.
2. Destroys on collision with a "Wall" tagged object.

---

### BulletSpawner.cs

#### Purpose

Spawns bullets that target the player.

#### Public Variables

- `bulletSpeed`: `float` — Bullet speed (default: 5f).
- `bulletPrefab`: `GameObject` — Bullet prefab.

#### Methods

- `Start()`: Starts periodic shooting.
- `LaunchBullet()`: Spawns a bullet.

#### Logic

1. Finds the player.
2. Spawns a bullet aimed at the player.

---

### HealthBar.cs

#### Purpose

Displays health via a slider.

#### Public Variables

- `slider`: `Slider` — UI slider.
- `gradient`: `Gradient` — Color gradient.
- `fill`: `Image` — Slider fill.
- `explosionSound`: `AudioSource` — Damage sound.

#### Methods

- `SetHealth()`: Updates health.
- `SetMaxHealth()`: Sets maximum health.

#### Logic

1. Updates slider value and fill color.

---

### EnemeDestroy.cs

#### Purpose

Controls the player’s gun and destroys enemies with a laser.

#### Public Variables

- `tankGun`: `GameObject` — Gun object.
- `rotationSpeed`: `float` — Rotation speed (default: 5f).
- `playerMovement`: `PlayerMovement` — Reference to player movement.
- `laserPrefab`: `GameObject` — Laser prefab.
- `beamDuration`: `float` — Laser duration (default: 0.3f).
- `explodeSound`: `AudioSource` — Explosion sound.

#### Methods

- `Update()`: Manages rotation and shooting.
- `FindTheClosest()`: Finds the nearest enemy.

`GunRotation()`: Rotates the gun.

#### Logic

1. Finds the closest enemy.
2. Rotates the gun toward the enemy or player movement direction.
3. Fires a laser and destroys the enemy when "Space" is pressed.

---

### Billboard.cs

#### Purpose

Showcase healthbar canvas in front of camera 

---

## Script Interactions

- `PlayerMovement` shares `moveDirectionForGun` with `EnemeDestroy` for gun rotation.
- `BulletSpawner` and `BulletMovement` interact via `SetDirection`.
- `BulletDetection` updates `HealthBar` on damage.
- `EnemyLogic` targets the player from `PlayerMovement`.

## UI and Audio

- `HealthBar`: Displays health with a gradient and explosion sound.
- `CheckDeath`: Shows the game-over screen.
- Audio: Explosion (`explodeSound`), damage (`explosionSound`).

## Game Screenshots
Menu
![image](https://github.com/user-attachments/assets/3a42f077-ba26-4231-a7bb-1b513eae8c4f)

Game
![image](https://github.com/user-attachments/assets/7618c944-fe06-431b-a002-0fd1e2c8196c)
![image](https://github.com/user-attachments/assets/6841ccca-420b-4812-977b-b56ec41f76f9)


