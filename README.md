# MockCarSimulator

This is the phase 1 of my TelemetryECU project: Core engine for real-time automotive data simulation and telemetry.

## Contents

- [Project Overview](#projectoverview)
- [Tech Stack](#techstack)
- [Core Features](#corefeatures)
- [Road Map](#roadmap)

## Project Overview

MockCarSimulator is the foundational phase of the TelemetryECU ecosystem. Developed as part of my Final Graduation Project (TCC) for the Systems Analysis and Development degree, this module serves as a high-fidelity car simulator designed to generate, process, and broadcast mock automotive data.

The goal of this phase is to establish a robust C#-based logic layer that will eventually interface with real-world hardware (ESP32) and a web-based dashboard for live performance monitoring.

## Tech Stack

- Language: C# (.NET 10)
- Communication: Serial Port / WebSockets (for future ESP32 integration)
- Focus: Real-time data processing and automotive logic

## Core Features
´´´Dynamic RPM Simulation
      - Input-Driven Acceleration: The engine RPM responds dynamically to user interaction. By holding a key (or key press), the simulator triggers an                     acceleration curve, increasing RPM based on a "rev-up" constant.
      - Natural Deceleration: Releasing the click initiates a deceleration curve (engine braking simulation), bringing the RPM back to a stable idle state.
      - Redline Handling: Includes logic for RPM cut-off (Rev Limiter) to prevent "engine damage" within the simulation.
  ´´´
## Roadmap

[X] Phase 1 (This project): MockCarSimulator
[ ] Phase 2: Database (Oracle or MySql)
[ ] Phase 3: WebAPI
[ ] Phase 4: TelemetryECU Software
[ ] Phase 5: Interface
[ ] BONUS PHASE: Mobile integration
