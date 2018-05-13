# Kombatant

<p align="center">
    <img src="https://i.imgur.com/tLR7gDU.png" align="center" alt="Kombatant Logo" />
    <br /><br />
    An Advanced Open-Source Combat Assist Botbase for RebornBuddy
</p>

## Credits

Made by [Freiheit][Freiheit Github] with :two_hearts: and :cookie:.

Inspired by blasthoss' [MudAssist][MudAssist Github].

Uses [zzi][zzi Github]'s WPF Wrapper from [LeveGen][LeveGen Github].

## Features

Kombatant comes with tons of features to make your life easier. Below is a short list of some of the highlights per category.

### Combat Routine

- [x] __CR Behaviour Control__

  Allows fine-grained control over which behaviours you want the combat routine to potentially use. Don't want your routine to attack? What about healing? Perhaps today is a no-buff day.

- [x] __Smart Pull__

  When enabled, Kombatant will check whether you are in a party and if there is a tank present. The botbase will refrain from pulling mobs the tank has not tagged yet.

- [x] __"Select Routine" Button__

  Quickly pop open the Combat Routine selection dialogue from within the botbase settings to change combat routines on the fly. Easy as peasy!

### Convenience

- [x] __Autonomous Mode__

  Enabling this option influences how combat routines and plugins work, for instance your combat routine will automatically move closer to an enemy so it can be engaged. In return, you will lose a certain amount of control as the botbase will try to act fully autonomously.

- [x] __Auto Accept/Complete Quests__

  Automatically accepts open quests and will try to complete them if possible.

- [x] __Auto Commence Duty__

  Automatically commences a duty after x seconds.

- [x] __Auto Duty Notify__

  Automatically notifies you with a random sound snippet when your duty is ready.

- [x] __Auto Emote__

  Are you grinning or sad? Well, why not show it! Auto Emote can be used to keep up an expression emote like "/taunt" or "/grin" - even during battle and without disturbing your normal chat.

- [x] __Auto End ACT Encounters__

  Do you even parse, bro? When this option is active and an Advanced Combat Tracker process is found, Kombatant will send "/echo end" to the chat so ACT stops the encounter when the party is not engaged in combat for a set amount of time. No more fiddling with raid timers or having overlapping/fractured encounters.

- [x] __Auto Face Target__

  Automatically sets the game's "Face Target" option to the desired value.

- [x] __Auto FATE Synchronization__

  Targeting a FATE mob that requires level synchronization will automatically prompt Kombatant to sync to the FATE level if this option is enabled.

- [x] __Auto Skip Cutscenes__

  Skip most of the cutscenes. And advance the dialogue in those that cannot be skipped.

- [x] __Auto Skip Dialogue__

  When enabled, the botbase automatically skips through all dialogue.

- [x] __Auto Sprint__

  Will use Sprint whenever it is available.

- [ ] __Mechanics Warning__

  Automatically faces away on gaze attacks and stops actions on standstill debuffs.

  *Note: Implemented but buggy, only works when Auto Face Target is disabled.*

### Targeting

- [x] __Auto FATE Targeting Filter__

  When you are within a FATE, only FATE-relevant mobs as well as already aggro'd mobs will be selected through the targeting strategy.

- [x] __Auto Targeting__

  If enabled, the botbase will automatically select a target. Various different strategies for target selection are available. As a Red Mage, I love BestAoE which will select the target that will cause the most AoE damage.

- [x] __Max. Range Filter__

  When set to a value > 0 it will limit the range for target selection. Setting it to 0 will use the maximum pull range of the currently selected CR.

- [x] __Target Whitelist to farm specific mobs__

  Special filter for farming just a few mobs in an area. Park the bot, set the targets, start.

### Movement

- [x] __Auto Follow__

  Will cause the bot to automatically follow either the party leader, the party tank or a fixed character at a set distance.

  *Note: Due to technical limitations Auto Follow does not support jumping.*

- [x] __Auto Mount/Unmount for Auto Follow__

  The leader mounts up, so the bot will mount up.

- [ ] __Auto Takeoff/Land for Auto Follow__

  Will automatically take to the sky or land when the leader does it. Still requires some work because the method used is kind of wonky...

- [x] __Follow Path Generation__

  Switch between None, Navgraph (using the mesh) or Offmesh (using the same path as the leader).

- [x] __Enable/Disable Avoidance__

  Toggles avoidance support on and off. When enabled, Kombatant will automatically move out of telegraphed attacks advertised by the AvoidanceManager. Never be the dummy again!

- [x] __Pause Avoidance on Bosses__

  Will automatically pause the avoidance when fighting known dungeon bosses to ensure you do not forget to turn off avoidance.

  *Note: Does contain all dungeon bosses from A Realm Reborn, Heavensward and Stormblood. Does not contain any deep dungeon bosses, raid bosses or trials.*

### Hotkeys

- [ ] __Hotkeys__

  Implemented, but no UI because WPF without processing it through the compiler is utter bullshit.

- [x] __Dynamic Hotkeys__

  Allows the user to specify hotkeys that will control i.e. the combat routine's built-in hotkeys and chain multiple keys together to one press (i.e. disable Kombatant's auto targeting while also disabling Kefka's AOE with one keypress).

## Requirements
This botbase requires the international version of RebornBuddy.

There is **no** explicit support for the Chinese version of the bot.

## Installation

1. Clone or unpack Kombatant into your `.\RebornBuddy\BotBases` folder.
2. (Optional) Install the fonts in `.\RebornBuddy\BotBases\Kombatant\Resources\Font`.
3. Restart RebornBuddy.
4. Select `Kombatant` from the Botbase dropdown.

## Known Issues And Limitations

 - **There is no support for changing the hotkeys via the UI.**

   This is a limitation of the current implementation and might be revised in the future.

 - **Mechanic Warnings only work when Auto Face Target is turned off.**

   The exact reason for this behaviour is currently unknown.

- **Auto-Follow Takeoff needs a bit of love.**
  It works but the solution is dodgy and prone to errors.

## FAQ

 - **Q:** Why is the Chinese version not supported?

   **A:** The Chinese version of RebornBuddy is not on par with it's international twin when it comes to features and API. I do not want to strip out several features and keep patching the botbase for what are essentially two different products. Sorry.

 - **Q:** How do I add additional sounds for the Duty Notify?

   **A:** Convert them to WAV files and copy them into `.\Resources\Audio`.

 - **Q:** How do I edit the hotkeys or add dynamic hotkeys?

   **A:** Navigate to `.\RebornBuddy\Settings\<CHARACTER NAME>\Kombatant` and edit the `Hotkeys.json` file.

   TODO: Detailed instructions will follow here (if I do not forget to add them...).









[Freiheit Github]: https://github.com/Fryheit "Freiheit on Github"
[zzi Github]: https://github.com/zzi-zzi-zzi "zzi on Github"
[LeveGen Github]: https://github.com/zzi-zzi-zzi/LeveGen "LeveGen on Github"
[MudAssist Github]: https://github.com/mudbuddy/mud "MudAssist on Github"