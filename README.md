# ShivsLabAPI

A [LabAPI](https://github.com/northwood-labs/LabAPI) plugin for SCP: Secret Laboratory that adds shivs.

## Features

- Players can craft a shiv by standing near a wall and running `.shiv` in chat
- Using a shiv performs an attack on the nearest player in front of you
- Configurable damage, range, and cooldowns
- Remote admin command to give shivs and toggle the system on/off

## Commands

| Command | Handler | Description |
|---|---|---|
| `.shiv` | Client | Attempt to craft a shiv |
| `giveshiv <player id>` | Remote Admin | Give a shiv to a player |
| `toggleshiv` | Remote Admin | Enable or disable shivs |

## Configuration

```yaml
shivs_lab_api:
  enabled: true
  success_chance: 10      # 1 in N chance of crafting successfully
  damage_amount: 1        # damage dealt to crafter on failed craft
  shiv_damage_amount: 40  # damage dealt to target on stab
  range: 1                # stab range in metres
  craft_cooldown: 30      # seconds between craft attempts
  attack_cooldown: 5      # seconds between stabs
```

## Installation

1. Download the latest `ShivsLabAPI.dll` from [Releases](https://github.com/Ducstii/ShivsLabAPI/releases/latest)
2. Drop it into your server's `LabAPI/plugins` folder
3. Restart the server

## Requirements

- SCP: Secret Laboratory Dedicated Server
- [LabAPI](https://github.com/northwood-labs/LabAPI) 1.1.6.1+