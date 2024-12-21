# Task Tracker CLI

Task Tracker CLI is a simple command-line application to manage your tasks. You can add, update, delete, and track the status of tasks.

## Features

- Add tasks
- Update tasks
- Delete tasks
- Mark tasks as in-progress or done
- List all tasks or filter by status (todo, in-progress, done)

## Usage

### Add a Task
```
"Task Tracker Cli" add "Task description"
```

### Update a Task
```
"Task Tracker Cli" update <id> "Updated description"
```

### Delete a Task
```
"Task Tracker Cli" delete <id>
```

### Mark Task as In-Progress
```
"Task Tracker Cli" mark-in-progress <id>
```

### Mark Task as Done
```
"Task Tracker Cli" mark-done <id>
```

### List Tasks
- List all tasks:
```
"Task Tracker Cli" list
```
- List tasks by status:
```
"Task Tracker Cli" list done
```

## Requirements

- C#
- Newtonsoft.Json library

## Setup

1. Clone the repository.
2. Build the project.
3. Run the application from the command line.

