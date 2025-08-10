# GEMINI.md
This file provides guidance to GEMINI when working with code in this repository.

## High-Level Architecture

This project is a Reversi game implemented as a WPF (Windows Presentation Foundation) desktop application using C# and targeting .NET Framework 4.6.1.

The codebase is structured as follows:

*   **UI Layer:** XAML files (e.g., `MainWindow.xaml`, `SettingWindow.xaml`) and their corresponding C# code-behind files define the user interface and handle user interactions.
*   **Model Layer:** The `ReversiWpf/Model/` directory contains C# files that implement the core Reversi game logic, data structures, and settings (e.g., `MyReversi.cs`, `ReversiAnz.cs`, `ReversiSetting.cs`).
*   **Documentation:** API documentation is generated using Doxygen and can be found in the `docs/` directory.

## Common Development Tasks

### Build

To build the project, use the .NET CLI:

```bash
dotnet build ReversiWpf.sln
```

### Testing

There are no dedicated automated test projects found in this repository.

### Linting

Linting is typically integrated with the build process or handled by IDE extensions for C# projects. No explicit linting commands are provided.

## Project Specifics

*   The project aims to implement the Reversi algorithm.
*   Future development plans include updating the AI using TensorFlow.
