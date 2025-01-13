<p align="center">
    <img height="128" src=".idea/icon.png" alt="Icon of SchummelPartie">
</p>

<h1 align="center">SchummelPartie</h1>

<p align="center">Power up your Pummel Party experience with this customizable mod, built for friendly competition or solo practice.</p>

<div align="center">
    <a href="https://lyzev.github.io/discord"><img src="https://img.shields.io/discord/610120595765723137?logo=discord" alt="Discord"/></a>
    <br><br>
    <img src="https://img.shields.io/github/v/release/Lyzev/SchummelPartie" alt="GitHub latest release"/>
    <br>
    <img src="https://img.shields.io/github/downloads/Lyzev/SchummelPartie/total" alt="GitHub downloads"/>
    <br><br>
    <img src="https://img.shields.io/github/last-commit/Lyzev/SchummelPartie" alt="GitHub last commit"/>
    <img src="https://img.shields.io/github/commit-activity/w/Lyzev/SchummelPartie" alt="GitHub commit activity"/>
    <br>
    <img src="https://img.shields.io/github/languages/code-size/Lyzev/SchummelPartie" alt="GitHub code size in bytes"/>
    <img src="https://img.shields.io/github/contributors/Lyzev/SchummelPartie" alt="GitHub contributors"/> 
</div>

## Disclaimer

This project is an independent and unofficial modification designed to enhance the gameplay experience of Pummel Party in a fun and is for educational purposes only. The terms "hack" or "cheat" may be used as common synonyms for modding within the community, but they do not imply or involve any form of actual hacking or cheating. This project is not affiliated with, endorsed by, or authorized by Pummel Party, Rebuilt Games, or any of their affiliates.

Users are solely responsible for ensuring compliance with all applicable game policies and terms of service before using this software. The developers of this project disclaim all liability for any direct, indirect, incidental, or consequential damages arising from the use or misuse of this software. By using this software, you agree to do so at your own risk.

This project respects the intellectual property rights of Pummel Party and Rebuilt Games, and no claims of ownership over their intellectual property are made.

## Getting Started

This guide will help you set up everything you need to start using **SchummelPartie** with Pummel Party.

### Prerequisites

Before you begin, ensure you have the following:

1. **[Pummel Party](https://store.steampowered.com/app/880940/Pummel_Party/)** (Steam version): The base game required for the mod.  
2. **[MelonLoader](https://melonwiki.xyz/#/README)** (latest version): A universal mod loader for Unity games.  
   - See the manual here: [https://melonwiki.xyz/#/README](https://melonwiki.xyz/#/README)
   - It is recommended to use the [Automated Installation](https://melonwiki.xyz/#/README?id=automated-installation). (Download the installer and pick the game directory)
3. **[UrGUI](https://github.com/Lyzev/UrGUI/tree/main?tab=readme-ov-file#getting-started)** (latest version): A required library for SchummelPartie to function.
   - See the manual here: [https://github.com/Lyzev/UrGUI/tree/main?tab=readme-ov-file#getting-started](https://github.com/Lyzev/UrGUI/tree/main?tab=readme-ov-file#getting-started)

### Installation

Follow these steps to install SchummelPartie:

1. **Download SchummelPartie**  
   Go to the [releases page](https://github.com/Lyzev/SchummelPartie/releases) and download the latest version of the mod.

2. **Install MelonLoader**  
   If you haven't already installed MelonLoader, follow the steps in the tutorial linked above.

3. **Set Up UrGUI**  
   - Download the latest version of UrGUI from its [GitHub repository](https://github.com/Lyzev/UrGUI/tree/main?tab=readme-ov-file#getting-started).  
   - Place the necessary UrGUI files into your Pummel Party installation directory `UserLibs` (create if MelonLoader did not) as described in the UrGUI setup instructions.

4. **Install SchummelPartie**  
   - Locate the `Mods` folder in your Pummel Party installation directory.  
     *(If this folder doesn’t exist, MelonLoader will create it the first time you run the game.)*  
   - Place the downloaded `SchummelPartie.dll` file into the `Mods` folder.  

5. **Launch the Game**  
   Start Pummel Party through Steam. If everything is installed correctly, SchummelPartie will load automatically.

### Toggling the GUI

Once the game is running, you can toggle the mod’s graphical user interface (GUI) using the following keys (both work):

- Right Shift (R SHIFT)
- Insert

Use these keys to access and control the mod features easily.

### Notes and Troubleshooting

- Ensure your versions of MelonLoader and UrGUI are up to date for compatibility.  
- If the game doesn’t start or the mod doesn’t load, double-check the file paths and installation steps.  
- For further assistance, visit the [SchummelPartie GitHub page](https://github.com/Lyzev/SchummelPartie) or join the project’s support community.

## Modules

<details>
<summary>Debug</summary>
Toggle the Debug mode.

| Key                  | Description                                              | Host Required |
|----------------------|----------------------------------------------------------|---------------|
| T                    | Increase trophies in "Unlocks" menu.                     | No            |
| C                    | Increase crowns in "Unlocks" menu.                       | No            |
| R + Period (Ex: .)   | Reset unlocks in "Unlocks" menu.                         | No            |
| Shift + Left Click   | Spawn keys at the location of your mouse cursor.         | Yes           |
| Number               | Rolls your dice to the number you pressed.               | No            |
| Ctrl + Click         | Teleport to the location you clicked on.                 | Yes           |
| -                    | Collect 5 keys and 1 random item for the current player. | Yes           |
| Left/Right Shift + E | Force stops the current minigame.                        | Yes           |
| F11                  | Free Cam/No Clip (Use Shift and WASD)                    | No            |
| Numpad -             | Slow down animations                                     | Yes           |
| Numpad +             | Speeds up animations                                     | Yes           |
| P                    | Give 1 Key to everyone                                   | Yes           |
| Ο                    | Remove 1 Key from everyone                               | Yes           |
| Backtick (Ex: `)     | Open the console                                         | No            |
| F9                   | Forces 15 FPS                                            | Yes           |
| Up Arrow             | Show/Hide the arrows on the gameboard                    | No            |
| Left Shift + F10/F11 | Test connection to the game                              | Yes           |
</details>

<details>
<summary>Force Present</summary>
Forces the present to be the one you want.
</details>

<details>
<summary>Animal Arithmetic</summary>
Show the answer to the animal arithmetic.
</details>

<details>
<summary>Mystery Maze</summary>
Shows the path to the exit.
</details>

<details>
<summary>Mortar Mayhem</summary>
Show the answer to the mortar mayhem.
</details>

<details>
<summary>Batty Batter</summary>
Automatically hit the ball.
</details>

<details>
<summary>Spooky Spikes</summary>
Automatically crouch or jump when needed.
</details>

<details>
<summary>Rockin Rythm</summary>
Automatically hit the notes.
</details>

<details>
<summary>Pack And Pile</summary>
Automatically place boxes.
</details>

<details>
<summary>Treasure Hunt</summary>
Shows the path to the treasure.
</details>

<details>
<summary>Swift Shooters</summary>
Automatically shoot the good targets.
</details>

<details>
<summary>Bomber</summary>
Bombs are infinite.
</details>

<details>
<summary>Finder</summary>
Show the position of the other players.
</details>

<details>
<summary>Barn Brawl</summary>
God Mode, Infinite Shotgun (Press F), Burst Shotgun, ESP.
</details>

<details>
<summary>Presents</summary>
Automatically collect the best presents.
</details>

<details>
<summary>Selfish Stride</summary>
Show the target bridge.
</details>

<details>
<summary>Memory Menu</summary>
Show the target food.
</details>

<details>
<summary>Tanks</summary>
Rapid Fire.
</details>

<details>
<summary>Sidestep Slope</summary>
God Mode.
</details>

<details>
<summary>Speedy Spotlights</summary>
Show the position of the other players.
</details>

<details>
<summary>Daring Dogfight</summary>
God Mode, Kill All, Burst Shot, ESP.
</details>

<details>
<summary>Elemental Mages</summary>
Instantly pick up crystals and disable camera shake.
</details>

<details>
<summary>Crown Capture</summary>
No Punch Interval, No Stun, Always Crown
</details>

<details>
<summary>Explosive Exchange</summary>
No Punch Interval, No Stun, Always Crown
</details>

<details>
<summary>AirJump</summary>
Allows you to jump in the air.
</details>

<details>
<summary>Speed</summary>
Allows you to change your speed.
</details>

<details>
<summary>Graphical User Interface</summary>
Toggle the GUI with Insert or RightShift.
</details>

## Contributing

Thank you for considering contributing to this Pummel Party hacked client! Your contributions will help to make it even better.

### How to set up the development environment

1. Clone the repository.
2. Open the solution in your preferred IDE (e.g.: Rider, Visual Studio).
3. Done!

**NOTE:**  
*The project is built with .NET 4.8. You will need to have it installed to build the project.  
Don't forget to install the prerequisites mentioned in the "Prerequisites" section.*


## Bugs and Suggestions

### Discord

For assistance with minor concerns, feel free to join our supportive community on
the [Discord server](https://lyzev.github.io/discord). Our friendly members and staff are ready to help you.

### GitHub

To ensure a prompt and effective resolution of bugs or to share your suggestions, please submit them through
the [issue tracker](https://github.com/Lyzev/SchummelPartie/issues) of this repository. Kindly utilize the provided templates
and make sure to include all relevant details that would help us understand your issue better. Your cooperation is
greatly appreciated.
