# CourierServiceApp_Core
CourierServiceApp - Delivery Cost &amp; Time Estimator

CourierServiceApp is a modular, testable, and production-grade .NET 6+ console application that simulates a courier delivery system. It calculates delivery cost, discount, and estimated delivery time for packages based on dynamic input, using a fleet of delivery vehicles.

Built using Clean Architecture principles and Dependency Injection, the app is designed for maintainability, extensibility, and real-world performance.

---

## Technologies Used

- [.NET 6 Console App](https://learn.microsoft.com/en-us/dotnet/core/)
- C# 9
- Dependency Injection
- SOLID principles
- Custom Exception Handling

---

##  How to Run the Project (.NET 6 CLI)

### Prerequisites
- [.NET 6 SDK or later](https://dotnet.microsoft.com/download) installed
- Terminal or Command Prompt

---

### 🚀 Run the App from the Command Line

#### 1. Open Terminal and Navigate to the Project Root
```bash
cd path/to/CourierServiceApp
```

#### 2. Build the Project
```bash
dotnet build
```

#### 3. Run the App
```bash
dotnet run
```

---

### 🧪 Sample Input
You’ll be prompted to enter inputs like this:
```
Enter base delivery cost and number of packages:
100 5
PKG1 50 30 OFR001
PKG2 75 125 OFR008
PKG3 175 100 OFR003
PKG4 110 60 OFR002
PKG5 155 95 NA
Enter vehicle info (count speed max_weight):
2 70 200
```

---

### 📤 Sample Output
```
PKG1 0 750 4.00
PKG2 0 1475 1.79
PKG3 0 2350 1.43
PKG4 105 1395 0.86
PKG5 0 2125 4.21
```



