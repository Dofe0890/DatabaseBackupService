# DatabaseBackupService

A Windows Service developed in C# for automated SQL Server database backups. It integrates with Windows Task Scheduler to ensure reliable and consistent backups without user interaction.

## Features

- Automatically backs up a specified SQL Server database
- Generates `.bak` files with timestamped or GUID-based filenames
- Configurable source database, backup interval, and storage location
- Uses Task Scheduler for timed execution

## Technologies

- C# (.NET Framework or .NET Core)
- Windows Service
- SQL Server
- Task Scheduler

## Use Cases

- Daily/weekly automatic backups of critical databases
- Lightweight solution for on-premise database backup needs
- Can be extended to include FTP/cloud upload

## Configuration

Update `app.config` or external `.json`/`.xml` config file with:

- `ConnectionString`
- `BackupPath`
- `DatabaseName`
- Optional backup naming strategy (timestamp or GUID)

## Setup

1. Install the service using `sc.exe` or `InstallUtil`.
2. Register the executable with Task Scheduler for scheduled execution.
3. Ensure the service has permissions to access SQL Server and write to the backup directory.

## Security Tip

Avoid storing plain-text SQL Server credentials. Use Windows Authentication where possible or secure credential storage solutions.
