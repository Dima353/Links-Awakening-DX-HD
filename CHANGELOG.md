# Обновленная версия The Legend of Zelda: Link's Awakening DX HD
### Дополнения от PG Team
 * Добавлен Redux патч.
 * Добавлена русская локализация от Zelda64Rus v2.2.
 * Добавлена сборка нативной Linux-версии.
 * Добавлена регулировка непрозрачности теней by FeraiAI
 * Добавлен подсчёт времени проведенного в игре by LouisSF
 * Добавлена возможность отключения диалоговых окон о поднятии камней, части-силы и жёлудя-хранителя. by FeraiAI
 
# Исправления взяты из репозитория: 
https://github.com/BigheadSMZ/Zelda-LA-DX-HD-Updated

### **The Legend of Zelda: Link's Awakening Changelog:**
## **v1.1.7**
### **Sword Fixes Update:**
  * Modify attack sword hitboxes. Frame 1: hitbox added, 2: hitbox expanded, 3: hitbox reduced.
  * Modify the hitbox of bushes to match their sprite/tile size. This fixes wonky "cutting" with sword.
  * Allow collecting items with the sword when holding it out in charging state.
  * Level 2 sword shot now matches height of Link and doesn't collide when standing on raised blocks.
  * Fix the hookshot where it would never fire again when trying to use it with other weapons.
  * Item collection box height can exceed its width or 8. Fixes collection range of items like rupees.
  * Add a hack-fix when hitting the switch to raise blocks while jumping over them and getting stuck.
  * Renamed FPS Lock to "Vertical Sync" as this is a far more accurate description of what it does.
  * Make UI scale slider static 1-11, 11=Auto-Detect, and only apply scale if screen can handle it.
  * New brick texture for main menu that better matches DX on GBC. Can swap to old texture.
  * New Miscellaneous page. Currently only contains option to swap to old menu brick texture.

## **v1.1.6**
### **General Fixes Update:**
  * Expand size of most UI elements on settings pages to better fix non-english languages.
  * Translate "Controller" on controller selection button on Game Settings page.
  * Restore fisherman's line "You have to have more passion. Live a little." when exiting pond early.
  * Add option to "Audio Settings" to mute powerup music (Piece of Power/Guardian Acorn).
  * Remember scaling settings on window resize when not set to Auto-Detect.
  * Change "Controller" selection text to "GamePad" in all langauges.
  * Fix potential crash when using items immediately after save is loaded.

## **v1.1.5**
### **The Features Update:**
  * Add controller type selection to Game Settings menu: XBox, Nintendo, Playstation.
  * Make UI Scale slider "Auto-Detect" the rightmost value.
  * Freeze the game world while playing the ocarina.
  * Hookshot can be retracted with a second button press.
  * Translate "Back" and "Select" on the menus. Thanks to [xsm2](https://github.com/xsm2).
  * Translate "Thief" across all language files. Thanks to [xsm2](https://github.com/xsm2).

## **v1.1.4**
### **Bugfix Update:**
  * Fix sprite of hole dug with shovel overwriting rocks pushed over it.
  * When attacking and standing still, it's possible to change facing direction.
  * Shooting arrows left and right when next to a south wall no longer triggers collision.
  * Goomba can no longer be attacked after it has already been stomped.
  * Hardhat Beetle is affected by knockback effect of piece of power and red tunic.

## **v1.1.3**
### **The Languages Update:**
  * Fixed muting audio when window is out of focus if option is enabled.
  * Playtime is now tracked and visible on file selection. Thanks to [LouisSF](https://github.com/LouisSF).  

### **v1.1.2 Changelog:**
#### **Emergency Update:**
 * Don't apply smaller collision scale to holes already smaller than 16x16.

### **v1.1.1 Changelog:**
#### **Minor Update:**
 * Reimplement the option to unlock FPS.
 * Fix alligator can throw with unlocked FPS (credit @buttcheeks69).
 * Fix incorrectly calculated custom text height for confirm button (credit @squiddingme).
 * More accurate timing when picking up sword on beach (credit: @buttcheeks69).
 * Show key counter on HUD when in dungeons (credit: @squiddingme)
 * Custom sprite for key counter.
 * Add option to display items in the bottom right (credit: @squiddingme).
 * Pay the Shop Keeper to no longer be called "Thief" after stealing.
 * Reduced collision size / pull strength of holes to more closely match the original game.

### **v1.1.0 Changelog:**
#### **General:**
 * Intro and title screen reworked to better match original game.
 * Always show the title screen even when skipping intro video.
 * Fix crashing and many other issues when starting in "Fullscreen" with "Borderless" disabled.
 * Keyboard controls will now properly save.
 * Swimming sound effect when in deep water was added.
 * Low hearts will play the beeping sound.
 * Photographs are now colorized.
 * Save files reference hearts/health as "maxHearts/maxHealth" instead of "Hearth".
 * New version compatible with old save files but not vice versa.
 
 #### **User Interface:**
 * Swapped the Start and Select buttons from the original release.
 * Quit button on the main menu after the title screen.
 * Restored commented out Audio Settings page and move volume controls there.
 * Restored commented out UI scale code and added it to Graphics Settings page.
 * Added option to mute sound when window is not active in Audio Settings page.
 * Rename "Back to Game" button text to "Return to Game".
 * Added "Save & Quit" button on the in-game pause menu.
 * Game scale can be adjusted with LT and RT buttons.
 * Low hearts beep can be disabled from game settings menu.
 * Screen shake when taking damage can be disable from game settings menu.
 
 #### **Items:**
 * Items can be assigned to two additional buttons: LB and RB.
 * Inventory cursor sound was added.
 * Items can be used when pushing against objects.
 * Cooldown of the sword has been removed.
 * The sword can be charged while using the shield.
 * The sword can be charged while jumping off cliffs.
 * The sword can be charged while underwater in 2D.
 * The shield can be used while charging the sword.
 * The shield plays the proper sound effect when blocking.
 * Bombs will appear in the shop after buying the shovel.
 * Bomb-arrows now deal full damage to the initial target.
 * Pegasus Boots directional influence has been reworked.
 * Bracelets pick up Flying Rooster or Smasher Ball instantly.
 * Piece of Power and Guardian Acorn can be collected with sword.
 * Piece of Power and Guardian Acorn proper sound and music delay.
 * Piece of Power and Guardian Acorn will not lose music in some dungeons.
 * Knockback effect (piece of power) of sword lvl 2 removed.
 * Knockback effect (piece of power) of red tunic restored.
 
 #### **Enemies:**
 * Stunned enemies no longer deal damage.
 * Enemies dying from burning no longer deal damage.
 * Goponga Swamp flowers can be killed with Lvl 2 sword + spin attack/piece of power/red tunic.
 * Blade Traps will now collide with pushed blocks.
 * Green Zol damage delay while spawning.
 * Shy Guy facing direction fixed while charging sword.
 * Pols Voice can be defeated with the ocarina.
 * Flying Tile sound loop now more accurate to original.
 * All bosses have had their damage field deactivated on death.
 * Lanmola body (worm in desert) can now deal damage.
 * Stone Hinox (color dungeon) AI fixed from deadlocking into "jump" state.
 * Armos Knight (south of Face Shrine) can be hit with sword spin.
 * Final boss Aghanim's Shadow sound effect for bursting projectile.
 * Final boss Ganon's Shadow can be damaged with spin attack.
 * Final boss Lanmola can be attacked with spin attack.
 * Final boss DethI can be killed with boomerang.
 
 #### **Overworld:**
 * Mabe/Animal Village music always takes priority over piece of power/acorn music.
 * Trendy game timings are closer to original game.
 * Owl conversations now properly freeze all enemies until he flies away.
 * Marin will teach Ballad of the Wind Fish after acquiring Ocarina.
 * Fixed Marin's message when attacking chickens to not default to sadistic comment.
 * When Marin sings to the walrus it now matches the duration of the original game.
 * Fixed rare issue in Dream Shrine where sleeping sprite would float past the bed.
 * Randomized sound effect frequency at Seashell Mansion when counting shells.
 * Richard's Villa maze sequence can no longer be cheated by jumping the holes.
 * Richard's castle photograph event fixed.
 * The ghost sequence must be respected: house by the bay  grave stone.
 * The ghost must be returned before his photo and sprinkling powder on grave.
 * The ghost return sequence now freezes the animations of nearby monsters.
 * Fixed getting the ghost photograph before returning him to the grave.
 * Increased the "grab" distance of the raft so it can't be clipped through.
 * Sign post maze now starts at the correct sign.
 * Flying rooster can no longer skip the flame trap on the way to Turtle Rock.
 * Fixed dying when holding onto the flying rooster.
 
 #### **Dungeons:**
 * Softlock fixed when picking up instruments.
 * Increased interaction range of dungeon teleporters.
 * Tail Cave (dungeon 1) block near trap is now pushable.
 * Face Shrine (dungeon 6) now references the correct key.
 * Face Shrine (dungeon 6) doors can only be opened with statues.
 * Face Shrine (dungeon 6) fixed spot where it was possible to get stuck in a wall.
 * Eagle Tower (dungeon 7) pull lever now works with small analog left/right values.
 * Eagle Tower (dungeon 7) when falling in holes Z-axis is remembered.
 * Turtle Rock (dungeon 8) fixed locked door that took a key but did not open.
 * Turtle Rock (dungeon 8) breakable wall was fixed (it couldn't be broken).
 * Play the secret discovery chime when taking the correct path in the egg.