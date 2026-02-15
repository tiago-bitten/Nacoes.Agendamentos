# Loote Git Flow

Argument: `$ARGUMENTS` (must be `master` or `desenv` — the target branch to merge into)

Execute the full git flow below **in order**. Stop immediately if any step fails and report the error.

---

## Step 0: Validate argument

- The argument must be `master` or `desenv`. If empty or invalid, ask the user which target branch to use.

---

## Step 1: Code Review

- Run the `/lt-review` command against all changed files.
- If violations are found, **fix them all automatically**, then re-run the review to confirm zero violations.
- Only proceed when the review passes with no violations.
- Run `dotnet build` to confirm compilation succeeds after any fixes.

---

## Step 2: Analyze changes

- Run `git status` and `git diff` (staged + unstaged) to understand all changes.
- Identify the key areas modified (e.g., features added, bugs fixed, refactors done).
- Determine a short, descriptive branch name in kebab-case based on the changes (e.g., `feat/sefaz-nfe-integration`, `fix/batch-validation`, `refactor/user-context`).
  - Prefixes: `feat/`, `fix/`, `refactor/`, `chore/`, `docs/`

---

## Step 3: Create branch and commit

- Create the branch: `git checkout -b <branch-name>`
- Stage all relevant changed files (avoid secrets like `.env`).
- Create a commit with a clear message summarizing the changes. End with:
  ```
  Co-Authored-By: Claude Opus 4.6 <noreply@anthropic.com>
  ```

---

## Step 4: Create GitHub issues

- For each **distinct logical change** (new feature, bug fix, config change, etc.), create a GitHub issue using `gh issue create`.
  - Title: short imperative description (e.g., "Add SEFAZ NF-e integration")
  - Body: brief description of what was done
  - Labels: use appropriate labels if available (`enhancement`, `bug`, `chore`, etc.) — check available labels first with `gh label list`
- After creating each issue, **close it immediately** with `gh issue close <number>` since the work is already done.
- List the created issues at the end for the user.

---

## Step 5: Merge into target branch

- Switch to the target branch (`master` or `desenv`): `git checkout <target>`
- Pull latest: `git pull origin <target>`
- Merge the feature branch: `git merge <branch-name>`
- If there are merge conflicts, report them to the user and stop.

---

## Step 6: Cleanup

- Delete the feature branch locally: `git branch -d <branch-name>`
- Confirm the current branch is the target branch and is clean with `git status`.

---

## Step 7: Summary

Report to the user:
1. Branch name that was created and merged
2. Commit hash and message
3. GitHub issues created (with numbers and links)
4. Current branch and status

---

## Important

- NEVER force push or use destructive git commands.
- NEVER push to remote unless the user explicitly asks.
- If anything fails, stop and report — do not try to recover silently.
- Always use `git status` to verify state between steps.
