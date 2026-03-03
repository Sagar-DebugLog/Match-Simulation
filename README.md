# Match Simulation Assignment

## Overview

This project is a simple match simulation made in Unity.

* 10 players are created automatically.
* Every 1–2 seconds, one random player kills another.
* Killer gets +1 score.
* Dead player respawns after 3 seconds.
* Match ends when:

  * A player reaches 10 kills, or
  * 3 minutes are completed.

The leaderboard updates live and shows the highest score on top.
A winner panel appears when the match ends.

---

## Architecture Decisions

I did not put all logic inside one GameManager.

Instead, I separated the systems:

* **GameInitializer** – Starts the match.
* **MatchController** – Controls match flow.
* **PlayerModel** – Stores player data only.
* **ScoreSystem** – Handles scoring logic.
* **TimerSystem** – Handles match timer.
* **KillSystem** – Simulates random kills.
* **RespawnSystem** – Handles respawn delay.
* **UI scripts** – Only update UI using events.

All gameplay logic is written in normal C# classes.
UI and gameplay are connected using events.

---

## Why I Structured It This Way

I separated everything to keep the code clean and easy to manage.

Benefits:

* Easy to read and understand.
* Easy to modify one system without breaking others.
* UI does not control gameplay logic.
* Systems are reusable.
* Better structure for scaling in future.

This avoids putting all logic inside one MonoBehaviour.

---

## Extensibility

I added a simple Sudden Death mode.

If time finishes and top two players have the same score:

* The match continues.
* The next kill decides the winner.

This is controlled from MatchConfig.

---

## How I Would Scale This

### Real Multiplayer Integration

* Replace KillSystem with server-based kill events.
* Server would control score and match state.
* MatchController structure would remain the same.
* Player data would sync from server.

---

### 50 Players

* Increase player count in MatchConfig.
* Optimize leaderboard sorting if needed.
* Use object pooling for UI items.

The architecture does not require major changes.

---

### Production-Level Mobile Game

* Use object pooling instead of Destroy/Instantiate.
* Avoid unnecessary sorting.
* Pre-allocate lists.
* Reduce garbage allocations.