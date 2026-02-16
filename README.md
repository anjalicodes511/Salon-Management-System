# 💎 The Ivory Lounge  
### Salon Appointment Management System  

![ASP.NET MVC](https://img.shields.io/badge/ASP.NET-MVC-blue)
![Dapper](https://img.shields.io/badge/ORM-Dapper-orange)
![SQL Server](https://img.shields.io/badge/Database-SQL%20Server-red)
![Bootstrap](https://img.shields.io/badge/UI-Bootstrap-purple)

---

## 📌 Overview

**The Ivory Lounge** is a full-stack, end-to-end Salon Management System built using ASP.NET MVC, Dapper ORM, and SQL Server.

The system intelligently:

- Allocates consecutive time slots based on service duration  
- Checks stylist availability dynamically  
- Prevents double booking  
- Updates UI seamlessly using AJAX without full page reload  

This project demonstrates **real-world booking logic**, optimized database handling, and a dynamic user experience.

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
- Automatic multi-slot booking for long services  
- View My Appointments  
- Cancel appointments dynamically via AJAX  

### 🛠 Admin Features
- Manage Services (duration & price)  
- Manage Stylists  
- Manage Time Slots  
- View & Manage Appointments  
- Stored procedure-based booking control  
- Generate Reports: Appointment, Revenue, Monthly Revenue, Most Booked Services  

---

## 🧠 Smart Booking Logic

### 🔹 Automatic Slot Allocation
For services with extended durations (e.g., 90 minutes):

- The system checks for consecutive slot availability  
- Confirms stylist availability  
- Prevents conflicts with existing appointments  

### 🔹 Available Stylist During Slot Generation

1. Fetch all stylists  
2. Check if stylist is booked on selected date  
3. Filter stylists with:
   - Consecutive available time slots  
   - No conflicting appointments  

**Flow:**

User selects **Service + Date** →  
System calculates:
- Service duration & required consecutive slots  
- Stylist availability for those slots  
- Existing appointment conflicts  

↓  
Returns:  
- Available Time Slots  
- Available Stylists  

Customer selects preferred stylist → Appointment booked automatically  

---

### 🔹 Example Scenario

- **Service:** Hair Coloring (90 mins)  
- **Date:** 18-Feb-2026  

System checks:
- Which 3 consecutive slots are available  
- Which stylist is free for all 3 slots  
- Prevents double booking  

Result:  
- Displays available slots and stylists for booking  

---
