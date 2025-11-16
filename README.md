# Brisbane Airport Flight and Passenger Management System

A comprehensive flight management application built with C# and .NET 9.0, demonstrating object-oriented design principles and software engineering best practices.

## How It's Made:

This application was developed as part of the CAB201 Object Oriented Design and Implementation course at QUT. The system handles real-world airline operations including user management, flight booking, and loyalty programs.

The architecture follows a layered approach with clear separation between presentation, business logic, and data access layers. I implemented several design patterns including Strategy for seat booking behavior, Facade for complex operation orchestration, and Repository for data abstraction.

The codebase demonstrates SOLID principles throughout, with dependency injection enabling testability and loose coupling. Each class has a single responsibility, and the system is designed for extensibility - ready for GUI implementation or database integration in future iterations.

## Optimizations

### Performance Considerations
- In-memory storage provides fast data access for the prototype
- Strategy pattern eliminates conditional logic in booking operations
- Static flight memory reduces object creation overhead

### Code Quality Optimizations
- Eliminated code duplication through inheritance and composition
- Implemented comprehensive input validation to prevent runtime errors
- Used interfaces to enable easy swapping of implementations

## Lessons Learned

Building this system taught me several valuable lessons:

1. **Incremental Development**: Working through user stories one at a time helped maintain focus and catch issues early
2. **Separation of Concerns**: Keeping UI logic separate from business logic made the system much more maintainable
3. **Design Patterns Matter**: The Strategy pattern simplified complex seat booking logic that could have become messy with conditionals
4. **Testing is Essential**: The provided test cases caught several edge cases I hadn't considered
5. **Documentation Pays Off**: Comprehensive comments made debugging and future modifications much easier

## Examples:

- **User Management**: Three distinct user types with inheritance hierarchy
- **Flight Operations**: Complete arrival/departure flight lifecycle management
- **Booking System**: Intelligent seat assignment with conflict resolution
- **Loyalty Program**: Points calculation system for frequent flyers
- **Authentication**: Secure login with password validation
- **Delay Propagation**: Automatic updates to related flights when delays occur

## About

**Daniel Bray**  
Email: daniel.bray@connect.qut.edu.au  
Phone: 0492426521

This project showcases advanced C# programming skills, object-oriented design expertise, and a commitment to industry-standard software engineering practices. The system handles complex airline operations while maintaining clean, extensible code architecture.

---

## Getting Started

### Prerequisites
- .NET 9.0 SDK or later
- Visual Studio 2022 or VS Code (optional)

### Build Instructions

```bash
# Navigate to project directory
cd "Brisbane Airport Flight and Passenger Management System"

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run the application
dotnet run
```

### Running the Application

The application starts with a welcome screen where users can:
- Register as a new user (Traveller, Frequent Flyer, or Flight Manager)
- Login with existing credentials
- Access appropriate features based on user type

## Architecture

### System Architecture
Shows the overall layered structure with presentation, facade, service, domain, and repository layers.

![Architecture Diagram](PlantUML/ArchitectureDiagram/Architecture%20Diagram.png)

### Class Diagram
Complete object model showing relationships between all classes including inheritance hierarchies and associations.

![Class Diagram](PlantUML/ClassDiagram/Class%20Diagram.png)

### User Hierarchy
Inheritance structure demonstrating how Traveller and Frequent Flyer extend the base User class, while FlightManager provides administrative capabilities.

![User Type Hierarchy](PlantUML/UserTypeHierachyAndRelationships/User%20Type%20Hierachy%20and%20Relationships.png)

### Booking Flow Sequence
Step-by-step interaction diagram showing how users book flights, including seat selection and conflict resolution.

![Flight Booking Sequence](PlantUML/FLightBookingSequence/Flight%20Booking%20Sequence.png)

### Login Flow Sequence
Authentication process from user input through validation to session establishment.

![User Type Hierarchy](PlantUML/UserTypeHierachyAndRelationships/User%20Type%20Hierarchy%20and%20Relationships.png)

### Package Structure
Organization of code into logical packages showing dependency relationships.

![Package Structure](PlantUML/PackageStructure/Package%20Structure.png)

## Design Discussion

### Architecture Overview

The system uses a **layered architecture** with five distinct layers:

1. **Presentation Layer**: Console-based user interface with menu controllers
2. **Facade Layer**: Simplifies complex operations for the UI layer
3. **Service Layer**: Contains business logic and domain services
4. **Domain Layer**: Core business entities and rules
5. **Repository Layer**: Data access abstraction

This separation ensures that changes in one layer don't ripple through the entire system, making maintenance and future enhancements much easier.

### Key Design Patterns

**Strategy Pattern**: Used for seat booking where different user types have different booking behaviors. Frequent flyers can override any seat while regular travellers get the next available seat.

**Facade Pattern**: Simplifies complex workflows like user registration and flight booking into single method calls for the UI layer.

**Repository Pattern**: Abstracts data storage, allowing easy switching from in-memory to database storage without changing business logic.

**Dependency Injection**: All dependencies are constructor-injected, enabling loose coupling and making the system highly testable.

### Object-Oriented Design

The system demonstrates several OOP principles:

- **Inheritance**: User hierarchy with specialized classes for different user types
- **Polymorphism**: Booking strategies behave differently based on user type
- **Encapsulation**: Private fields with public properties control access to internal state
- **Abstraction**: Interfaces define contracts without exposing implementation details

### Extensibility Considerations

The system is designed for future enhancements:

- **GUI Implementation**: Business logic is completely separate from console UI
- **Database Integration**: Repository pattern allows swapping storage mechanisms
- **Additional Airlines**: Airline data is structured for easy extension
- **New User Types**: Inheritance hierarchy supports adding new user categories

## What to Improve

### Database Integration
Replace in-memory storage with persistent database using Entity Framework Core and SQL Server for data persistence and proper connection management.

### Modern User Interface
Upgrade from console UI to a web application using ASP.NET Core MVC with responsive design and real-time updates via SignalR.

