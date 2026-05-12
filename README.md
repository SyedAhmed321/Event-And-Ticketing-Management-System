# Event & Ticketing Management System 🎟️

A full-stack web-based **Event Management & Ticketing Platform** integrated with an **Event Planning Marketplace**.
The system allows users to browse and book event tickets, create personal events, hire organizers, and manage vendors — all within a single centralized platform.

Developed as a semester project for:

* **SEL 310 – Web Engineering**
* **CSL 220 – Cloud Computing**
* Department of Software Engineering, Bahria University Karachi Campus

---

# 📌 Project Overview

This platform combines:

* Event ticket booking
* Event management
* Vendor marketplace
* Organizer hiring
* Personal event planning
* Real-time ticket availability
* QR-based ticket verification
* Role-based dashboards

The goal is to provide a modern, scalable, and cloud-ready solution similar to platforms like Eventbrite and Ticketmaster, while also supporting complete event planning workflows.

---

# 🚀 Core Features

## 🔐 Authentication & Authorization

* User Registration & Login
* JWT Authentication
* Role-Based Access Control
* Password Hashing using BCrypt
* User Profile Management

### Supported Roles

* Admin
* User
* Organizer
* Vendor

---

# 🎫 Event Ticketing System

* Browse public events
* Search & filter events
* View event details
* Real-time ticket availability
* Book tickets
* Generate digital tickets
* QR code ticket validation
* Booking history management

---

# 🎉 Personal Event Planning

Users can create personal events such as:

* Weddings
* Birthday Parties
* Corporate Events
* Seminars

Users can either:

* Hire professional organizers
  OR
* Select vendors individually

---

# 🏢 Vendor Marketplace

Vendor categories include:

* Venue
* Catering
* Decoration
* Photography
* Music
* Lighting

Features:

* Vendor registration
* Service listings
* Availability management
* Vendor booking requests

---

# 📊 Dashboard System

## User Dashboard

* View bookings
* View tickets
* Track personal events

## Organizer Dashboard

* Manage events
* Track ticket sales
* Monitor attendees

## Vendor Dashboard

* Manage services
* Handle requests
* Update availability

## Admin Dashboard

* Manage users
* Verify vendors
* Monitor platform activity
* View analytics

---

# ⚡ Real-Time Features

* Live ticket availability
* Temporary ticket reservation locking
* Auto-expiring reservations
* QR-based event check-in
* Notification system

---

# 🛠️ Tech Stack

## Frontend

* React.js
* Tailwind CSS
* Bootstrap

## Backend

* ASP.NET Core Web API
* RESTful APIs

## Database

* MongoDB Atlas

## Authentication

* JWT Authentication
* BCrypt Password Hashing

## DevOps & Deployment

* GitHub
* Vercel
* Azure Free Tier

## Testing

* Postman

---

# 🧠 System Architecture

```text
Frontend (React)
       ↓
Controllers
       ↓
Services
       ↓
Repositories
       ↓
MongoDB
```

The backend follows a clean layered architecture for scalability and maintainability.

---

# 📂 Project Structure

```text
EventTicketing.API
│
├── Controllers
├── Models
├── DTOs
├── Interfaces
│   ├── Services
│   └── Repositories
├── Services
├── Repositories
├── Configurations
├── Helpers
├── Middleware
├── Extensions
├── Utilities
└── Program.cs
```

---

# 🗄️ MongoDB Collections

The system uses the following collections:

```text
Users
Events
Bookings
Tickets
CheckIns
PersonalEvents
EventRequirements
Vendors
VendorServices
Organizers
OrganizerRequests
Notifications
Reservations
```

---

# 🔍 Recommended MongoDB Indexes

## Users

* email (unique)

## Events

* organizerId
* category
* city
* startDate

## Tickets

* qrCode (unique)
* eventId
* userId

## Bookings

* userId
* eventId
* bookingDate

## Reservations

* expiresAt (TTL index)

---

# 📡 API Modules

## Authentication Module

```http
POST /api/auth/register
POST /api/auth/login
```

## Events Module

```http
GET    /api/events
GET    /api/events/{id}
POST   /api/events
PUT    /api/events/{id}
DELETE /api/events/{id}
```

## Bookings Module

```http
POST   /api/bookings
GET    /api/bookings/my-bookings
DELETE /api/bookings/{id}
```

## Tickets Module

```http
GET /api/tickets/my-tickets
GET /api/tickets/{id}
```

---

# 🔐 Security Features

* JWT Authentication
* Password Hashing
* Role-Based Authorization
* Secure API Access
* Validation & Error Handling

---

# ☁️ Cloud Computing Concepts Used

* Cloud-hosted database using MongoDB Atlas
* Cloud deployment using Vercel/Azure
* Scalable REST APIs
* Real-time availability handling
* Distributed cloud architecture

---

# 📱 Responsive Design

The application is fully responsive and optimized for:

* Desktop
* Tablet
* Mobile Devices

---

# 📌 Future Enhancements

* Payment Gateway Integration
* Live Chat System
* Reviews & Ratings
* Wishlist/Favorites
* Seat Selection
* Coupons & Discounts
* Refund System
* Live Streaming

---

# 👨‍💻 Team Members

| Name                  | Role                                  |
| --------------------- | ------------------------------------- |
| Syed Ahmed Hassan     | Team Lead / Backend & API Integration |
| Hamza Jamil           | Frontend Architecture & UI            |
| M. Daniyal Jamil Awan | Authentication & User Module          |
| Abdul Ahad            | Dashboard & Event Management          |
| Hunzala Rehman Butt   | Testing, Deployment & Integration     |

---

# 📖 Learning Objectives

This project demonstrates practical implementation of:

* Web Engineering Concepts
* Cloud Computing Concepts
* REST API Development
* MongoDB Database Design
* Authentication & Authorization
* Layered Architecture
* Frontend–Backend Integration
* Cloud Deployment

---

# 📚 References

* [Eventbrite](https://www.eventbrite.com?utm_source=chatgpt.com)
* [Ticketmaster](https://www.ticketmaster.com?utm_source=chatgpt.com)
* [ASP.NET Core Documentation](https://learn.microsoft.com/aspnet/core?utm_source=chatgpt.com)
* [MongoDB Atlas Documentation](https://www.mongodb.com/docs/atlas?utm_source=chatgpt.com)
* [Vercel Documentation](https://vercel.com/docs?utm_source=chatgpt.com)
* [Azure Documentation](https://learn.microsoft.com/azure?utm_source=chatgpt.com)

---

# 📄 License

This project is developed for academic and educational purposes only.

---

# ⭐ Contributors

Special thanks to all team members and instructors for guidance and support throughout the project development lifecycle.
