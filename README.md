# MockCarSimulator

This is the phase 1 of my TelemetryECU project: Core engine for real-time automotive data simulation and telemetry.

![menu](https://github.com/user-attachments/assets/7229f108-878d-437f-a425-6695586dd46f)

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
```
### Dynamic RPM Simulation ###
- Input-Driven Acceleration: The engine RPM responds dynamically to user interaction. By holding a key (or key press), the simulator triggers an                    acceleration curve, increasing RPM based on a "rev-up" constant.

- Natural Deceleration: Releasing the click initiates a deceleration curve (engine braking simulation), bringing the RPM back to a stable idle state.

- Redline Handling: Includes logic for RPM cut-off (Rev Limiter) to prevent "engine damage" within the simulation.

### Telemetry Broadcasting (Data Layer) ###
- Serialized Output: The simulator generates structured data packets (JSON/String format) containing RPM, Engine Temperature, and Fuel Pressure.

- Frontend Ready: These packets are broadcasted via Serial Port or WebSockets, allowing any frontend (React, Unity, or an ESP32-powered display) to consumeand   visualize the data in real-time.

- High Frequency: Optimized for a high refresh rate, ensuring smooth pointer movement on digital gauges.

### ECU Error Handling & Diagnostics ###
- Check Engine Logic: The system monitors engine health. If the RPM stays at the redline for too long or if temperature thresholds are exceeded, the                simulator triggers a "Check Engine" flag.

- Sensor Failures: Includes methods to simulate "noisy" or "dead" sensors, forcing the telemetry system to handle invalid data—essential for testing the            robustness of the future TCC dashboard.
```
## Roadmap

- [X] Phase 1 (This project): MockCarSimulator
- [ ] Phase 2: Database (Oracle or MySql)
- [ ] Phase 3: WebAPI
- [ ] Phase 4: TelemetryECU Software
- [ ] Phase 5: Interface
- [ ] BONUS PHASE: Mobile integration
