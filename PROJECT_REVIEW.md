# Project Review (Grab the Treasure)

Date: 2026-02-21

## Current state
- The project is in a playable state with clear core loop scripts (player movement, coin pickup, chest completion, level loading).
- Repository has recent cleanup commits and no uncommitted changes before this review.
- Codebase is still in an "early-to-mid maturity" stage: functional but with technical debt concentrated in scene flow, singleton/session management, and coupling between gameplay objects.

## What should be improved first

### 1) Scene flow and level loading (high priority)
- There are multiple scripts loading scenes directly with hard-coded indices or names (`LevelHandler`, `NewLevelHandler`, `Pause`, `Player`, `Chest`).
- This increases risk of breakage when scenes are renamed/reordered and duplicates logic.
- Recommendation:
  - Consolidate loading into one `SceneLoader` service (single API).
  - Prefer scene names/constants or ScriptableObject config over build index literals.

### 2) Runtime coupling and object lookup (high priority)
- Runtime lookups like `FindObjectOfType<Chest>()`, `GameObject.FindWithTag(...)`, and global static holders increase fragility and make testing harder.
- Recommendation:
  - Wire references via inspector where possible.
  - Introduce lightweight event-based communication for pickups/completion (`CoinCollected`, `LevelCompleted`).

### 3) Session/score ownership is inconsistent (high priority)
- `GameSession` tracks lives/coins, while `Chest` also tracks score and updates UI directly.
- Coin pickup currently pushes score to `Chest`, not session, which mixes UI + progression responsibilities.
- Recommendation:
  - Define a single owner for run score and meta progression.
  - Keep UI as a subscriber/view layer.

### 4) Dead code and debug noise (medium priority)
- Many `print(...)` calls, commented blocks, and unused fields/methods remain.
- Recommendation:
  - Remove dead/commented code.
  - Replace raw `print` with controlled logging or remove for production builds.

### 5) Persistence safeguards (medium priority)
- `PlayerPrefs.DeleteAll()` is used from gameplay scripts. This is convenient but dangerous long-term.
- Recommendation:
  - Scope resets to project-specific keys.
  - Add a simple migration/version key strategy.

## Refactoring plan (incremental)
1. Create `SceneLoader` and migrate all scene transitions to it.
2. Introduce `ScoreService`/`RunState` and move score writes out of `Chest`.
3. Replace object-finding calls with serialized refs/events.
4. Clean dead code and add naming/style consistency pass.
5. Add Unity Test Framework tests for score persistence and scene transition guards.

## Verdict
Yes — improvements and refactoring are needed, but this is a healthy point for incremental cleanup rather than rewrite. The game logic appears understandable and can be stabilized significantly with 2-3 focused refactor passes.
