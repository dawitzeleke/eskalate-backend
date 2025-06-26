# 🧱 Clean Architecture API with MongoDB, JWT, and Cloudinary

This project is a Clean Architecture-based .NET Web API designed with scalability and maintainability in mind. It uses MongoDB as its primary database, JWT for secure authentication, and Cloudinary for image handling.

---

## 🚀 Getting Started

### ✅ Prerequisites

Make sure you have the following installed:
- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- [MongoDB](https://www.mongodb.com/try/download/community) (or MongoDB Atlas)
- [Cloudinary Account](https://cloudinary.com/)
- [Postman](https://www.postman.com/) or Swagger UI for API testing

---

## ⚙️ Setting Up the Project Locally

1. **Clone the Repository**

```bash
git clone https://github.com/dawitzeleke/eskalate-backend
cd eskalate-backend

```

# Render Deployment Instructions

## 1. Prerequisites
- You need a MongoDB database (e.g., MongoDB Atlas)
- You need a Cloudinary account for file uploads
- You need JWT secret and settings

## 2. Environment Variables (set in Render dashboard)
- `MONGODB_CONNECTION_STRING`: Your MongoDB connection string
- `MONGODB_DATABASE_NAME`: Your MongoDB database name
- `CLOUDINARY_CLOUD_NAME`: Your Cloudinary cloud name
- `CLOUDINARY_API_KEY`: Your Cloudinary API key
- `CLOUDINARY_API_SECRET`: Your Cloudinary API secret
- `JWT_SECRET`: Your JWT secret
- `JWT_ISSUER`: Your JWT issuer (e.g., `https://your-app.onrender.com`)
- `JWT_AUDIENCE`: Your JWT audience (e.g., `https://your-app.onrender.com`)

## 3. Docker Build & Start Commands (set in Render dashboard)
- **Build Command:**
  ```
  docker build -t webapi .
  ```
- **Start Command:**
  ```
  docker run -p 10000:80 webapi
  ```
  (Render will map port 10000 to 80 by default)

## 4. Exposed Port
- Set the exposed port to `80` in the Render service settings.

## 5. Health Check Path
- `/swagger` or `/` (if you want to use Swagger UI for health check)

## 6. Notes
- Make sure your `appsettings.Production.json` is present and uses environment variables as shown.
- If you use a custom domain, update your JWT settings accordingly.
