# BouncingBallBonanza

## Videos
<a href="https://l.messenger.com/l.php?u=https%3A%2F%2Fyoutu.be%2FDPqXwkaUSH8&h=AT0y-_ZwUQGQK-HCyz7C-m5sWNXDIAjY0nPpp16pNvbBujApTdr2NVj5gklAY6jjv5Bu2FiPXfG57Gyhkr55AcmMyuSyCBU-X5e50DmbUrvQOvXS_eEun1FQ2ZDLmp7f7tbntS9jRnVdRIObX036Dw">Trailer</a>

<a href="https://drive.google.com/drive/folders/1W6pgkqhY7gSMC-r_0--Vu4M786TSt2g_?usp=sharing">Demo</a>

## User Guide
* The player can move freely throughout the level (by physically moving) and hit the ball by smacking the hands (controllers) against the yellow ball.
* Each level will have tiles on the wall that have various effects. The most common ones are point tiles, denoted by the number of points they are worth (10, 20, or 30).
* The goal of each level is to hit as many point tiles as possible within the time limit. If all point tiles are cleared, the player gains a bonus 100 points.
* Each level increases the number of point tiles you need to score as well as negative tiles such as the -50 points and game over tiles.

## Setup
* Ensure the Vive headset and controllers are properly set up and configured.
* Navigate to **BBB\fullscreen_nondev_mono\build\bin\x64\Debug\BBB.exe**
* Run the executable.
* In the menu scene, hit the ball towards the New Game panel.
* Play the game.

## Features
* Cube world environment with tiles which grant various effects.
* Special tiles:
  * -50 points
  * 2x multiplier on the next point tile
  * Additional time
  * Freeze timer and slow ball down momentarily
  * Increase ball speed momentarily
  * Throws (allows player to grab the ball and throw it)
  * Game over tiles - the game is over if the player hits one of these tiles
* Cube scales according to the player’s height. The height is calibrated when the gameplay scene starts (after the menu scene).
* Ball bounces have visual and sound effects.

## Technical Issues
* One challenge was handling the player object within Unity. We wanted to use the player’s position and height to adjust the cube world. Moreover, we had to dive into the player’s hand objects in order to handle collisions properly.

## Assets
* Menu Music - https://www.youtube.com/watch?v=802h7vcQNSk
* Level 1-2 Music - https://www.youtube.com/watch?v=ov86aOphmNs
* Level 3+ Music - https://www.youtube.com/watch?v=ittmg2-tTGU
* -50 Tile Image - https://toppng.com/free-image/red-x-red-x-PNG-free-PNG-Images_201446
* X2 Tile Image - https://www.botoxforoab.com/botox-treatment
* Throw Tile Image - https://www.kindpng.com/imgv/himTxxx_rugby-player-throwing-the-ball-person-throwing-ball/
* Hourglass Tile Image - https://www.pngwing.com/en/search?q=hourglass
* Snowflake Tile Image - https://www.vecteezy.com/free-png/snowflake
* Flame Tile Image - https://gallery.yopriceville.com/Free-Clipart-Pictures/Fire-PNG/Fire_Flame_Transparent_PNG_Clip_Art
* Bubbles Tiles Image - https://www.freepnglogos.com/pics/bubbles
* Bounce Sound Effect - https://www.videvo.net/royalty-free-sound-effects/bounce/
* Failed Level Sound Effect - https://www.youtube.com/watch?v=D2_r4q2imnQ
* Success Level Sound Effect - https://pixabay.com/sound-effects/search/success/
* Point Tile Collision Sound Effect - https://pixabay.com/sound-effects/search/coin/
* -50 Tile Collision Sound Effect - https://pixabay.com/sound-effects/search/error/
* Hourglass Tile Collision Sound Effect - https://pixabay.com/sound-effects/search/ticking/
* Snowflake Tile Collision Sound Effect - https://pixabay.com/sound-effects/search/freezing/
* Flame Tile Collision Sound Effect - https://pixabay.com/sound-effects/search/vroom/
* Ball Hit Sound Effect - https://pixabay.com/sound-effects/search/slap/
* Running Out Of Time Sound Effect - https://pixabay.com/sound-effects/search/ticking/
