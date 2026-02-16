# 💎 The Ivory Lounge  
### Salon Appointment Management System  

![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC-blue)
![Dapper](https://img.shields.io/badge/ORM-Dapper-orange)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap-purple))

---

## 📌 Overview

**The Ivory Lounge** is a full-stack Salon Appointment Management System built using ASP.NET MVC, Dapper ORM, and SQL Server.

The system intelligently:

- Allocates consecutive time slots based on service duration  
- Checks stylist availability dynamically  
- Prevents double booking  
- Updates UI using AJAX without full page reload  

This project demonstrates real-world booking logic and optimized database handling.

---

## 🏗 Tech Stack

| Layer        | Technology |
|--------------|------------|
| Frontend     | HTML5, CSS3, Bootstrap 5 |
| Client-Side  | jQuery, AJAX |
| Backend      | ASP.NET MVC (C#) |
| ORM          | Dapper |
| Database     | SQL Server |
| Hosting      | IIS / IIS Express |

---

## ✨ Key Features

### 👤 Customer Features
- Register & Login
- Browse available salon services
- Real-time slot & stylist availability
- Automatic multi-slot booking
- View My Appointments
- Cancel appointments dynamically (AJAX)

### 🛠 Admin Features
- Manage Services (duration & price)
- Manage Stylists
- Manage Time Slots
- View & Manage Appointments
- Stored procedure-based booking control

---

## 🧠 Smart Booking Logic

### 🔹 Automatic Slot Allocation
If a service duration is 90 minutes:

System verifies:
- Consecutive slot availability
- Stylist availability
- Booking conflicts

---

## 💇 Available Stylist During Slot Generation

When generating available slots, the system:

1. Fetches all stylists.
2. Checks if stylist is already booked on selected date.
3. Filters only those stylists with:
   - Available consecutive time slots
   - No conflicting appointments

### Logic Flow

User selects:
Service + Date

↓
System checks:
ServiceDuration
Required Slots
StylistTimeSlot availability
Existing Appointments

↓
Returns:
Available Time Slots
Available Stylists for that slot
