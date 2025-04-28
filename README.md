# Memento

**Memento** is a user-friendly ASP.NET Core MVC application designed to help users manage their diary entries and set personal goals. This application allows users to create, edit, and delete diary entries, as well as add and track goals. Developed using .NET Core MVC, it provides a seamless experience for organizing thoughts and aspirations.

## Table of Contents
- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Prerequisites](#prerequisites)
- [Setup Instructions](#setup-instructions)
- [Contributors](#contributors)

## Overview
**Memento** simplifies personal reflection and goal management by providing a streamlined interface for users to maintain their diary and track their goals. Built on ASP.NET Core MVC with Entity Framework for database interactions, it ensures a smooth experience for users to document their thoughts and ambitions.

## Features
- **Diary Entries**: Create, edit, and delete diary entries with titles, content, and timestamps.
- **Goal Tracking**: Add, view, and update personal goals with deadlines and completion status.
- **User Authentication**: Secure login and signup process to maintain user privacy.
- **User Profiles**: View and edit personal profile information.
- **Search and Sort**: Easily navigate through diary entries and goals.


## Prerequisites
To run the *Memento* application on your local machine, you will need the following installed:

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [.NET Core SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [ADO.NET](https://learn.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview)

## Setup Instructions
Follow these steps to get *Memento* up and running on your machine:

1. **Clone the repository**:
    ```bash
    git clone https://github.com/Smituz/MementoMVC.git
    cd Memento
    ```

2. **Open the solution in Visual Studio**:
    - Open `Mementov2.sln` in Visual Studio.

3. **Configure Database Connection**:
    - Update the `appsettings.json` file with your SQL Server credentials:
      ```json
      "ConnectionStrings": {
        "DefaultConnection": "Server=your_server_name;Database=MementoDB;Trusted_Connection=True;MultipleActiveResultSets=true"
      }
      ```

4. **Run Migrations**:
    - Run the following command in the Package Manager Console to apply migrations and set up the database:
      ```powershell
      Update-Database
      ```

5. **Run the Application**:
    - Press `F5` in Visual Studio or run the project using the terminal:
      ```bash
      dotnet run
      ```

6. **Access the Application**:
    - Navigate to `http://localhost:7231` in your web browser to use the app.

## Contributors
- **[Smit Bhansali](https://github.com/Smituz/)** - Developer
- **[Saif Huseni](https://github.com/saifhuseni)** - Developer
