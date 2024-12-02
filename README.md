# MQTT Broker Implementation

This is a .NET-based MQTT broker implementation designed to facilitate communication between multiple MQTT clients. It includes an API layer using CQRS with Mediator, and utilizes Entity Framework for ORM. This service is intended to run on a Raspberry Pi for low-cost, efficient edge computing.

## Features

- **MQTT Broker**: Handles the publishing and subscribing of messages for MQTT clients.
- **API Layer**: Exposes a RESTful API to interact with the broker for configuration and monitoring.
- **CQRS**: Separates read and write operations for better scalability and maintainability using Mediator.
- **Mediator**: Used to implement this separation and handle requests in a decoupled manner.
- **Entity Framework**: Used as the ORM to handle database operations.
- **Clean Architecture**: The project follows Clean Architecture principles to separate concerns and promote testability, maintainability, and scalability.
- **Deployment**: Can be deployed on a Raspberry Pi for a lightweight, efficient MQTT solution.
