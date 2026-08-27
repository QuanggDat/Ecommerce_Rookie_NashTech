<div align="center">

# 🛍️ Ecommerce Rookie — NashTech

**A full-stack e-commerce platform built with ASP.NET Core 6, Entity Framework Core and React**

[![.NET](https://img.shields.io/badge/.NET-6.0-512BD4?style=flat-square&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-Web%20API-5C2D91?style=flat-square&logo=dotnet&logoColor=white)](https://learn.microsoft.com/aspnet/core)
[![EF Core](https://img.shields.io/badge/EF%20Core-6.0.29-512BD4?style=flat-square)](https://learn.microsoft.com/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat-square&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/sql-server)
[![React](https://img.shields.io/badge/React-18.3-61DAFB?style=flat-square&logo=react&logoColor=black)](https://react.dev/)
[![Redux Toolkit](https://img.shields.io/badge/Redux%20Toolkit-1.9-764ABC?style=flat-square&logo=redux&logoColor=white)](https://redux-toolkit.js.org/)
[![Tailwind CSS](https://img.shields.io/badge/Tailwind%20CSS-3.4-06B6D4?style=flat-square&logo=tailwindcss&logoColor=white)](https://tailwindcss.com/)
[![JWT](https://img.shields.io/badge/Auth-JWT%20Bearer-000000?style=flat-square&logo=jsonwebtokens&logoColor=white)](https://jwt.io/)

[Overview](#-overview) · [Architecture](#-architecture) · [Database](#-database-design) · [Getting Started](#-getting-started) · [API](#-api-reference)

</div>

---

## 📖 Overview

**Ecommerce Rookie** is a training project that demonstrates a production-shaped e-commerce system built on a **layered (N-tier) architecture**. A single ASP.NET Core Web API serves two independent front-ends — an **MVC storefront** for customers and a **React SPA** for administrators — over a stateless, JWT-secured HTTP boundary.

| | |
|---|---|
| **Backend** | ASP.NET Core 6 Web API · Swagger / OpenAPI |
| **Persistence** | Entity Framework Core 6 (Code-First) · SQL Server |
| **Identity** | ASP.NET Core Identity · JWT Bearer authentication |
| **Storefront** | ASP.NET Core MVC (Razor Views) · Session-based cart |
| **Admin Portal** | React 18 · Redux Toolkit · React Router 6 · Tailwind CSS |

---

## ✨ Features

<table>
<tr>
<th width="50%">🛒 Customer</th>
<th width="50%">🛠️ Administrator</th>
</tr>
<tr valign="top">
<td>

- Browse the home page with category menu & featured products
- Filter and search products by category
- View detailed product information
- Register a new account
- Log in / log out (JWT issued by the API)
- Add items to a session-backed shopping cart
- Place an order with receiver details
- Track personal order history & status

</td>
<td>

- Secure log in / log out
- Manage categories — create, update, delete, search & paginate
- Manage products — name, category, price, images, description, timestamps
- Browse the customer directory with search & paging
- Ban / un-ban customer accounts
- Review orders and advance their status

</td>
</tr>
</table>

---

## 🏛 Architecture

The solution follows a strict one-way dependency flow. Presentation layers never touch the `DbContext` directly — every read and write is funnelled through the service layer.

```mermaid
flowchart TB
    subgraph CLIENT["🖥️ Presentation Tier"]
        direction LR
        CFE["<b>Customer Storefront</b><br/>ASP.NET Core MVC<br/><i>Razor · Session Cart</i>"]
        AFE["<b>Admin Portal</b><br/>React 18 SPA<br/><i>Redux Toolkit · Tailwind</i>"]
    end

    subgraph API["⚙️ Application Tier — ASP.NET Core 6 Web API"]
        direction TB
        MW["<b>Middleware Pipeline</b><br/>HTTPS Redirection · JWT Bearer · Authorization · Swagger"]
        CTRL["<b>Controllers</b><br/>User · Category · Product · Order · OrderDetail"]
        SVC["<b>Service Layer</b><br/>UserService · CategoryService · ProductService<br/>OrderService · OrderDetailService"]
        MW --> CTRL --> SVC
    end

    subgraph DATA["🗄️ Data Tier"]
        direction TB
        CTX["<b>AppDbContext</b><br/>EF Core 6 · IdentityDbContext"]
        DB[("<b>SQL Server</b><br/>Code-First Migrations")]
        CTX --> DB
    end

    SHARED["📦 <b>Shared Contracts</b><br/>Models · ViewModels · Enums"]

    CFE -- "HTTP + Bearer<br/>typed HttpClient" --> MW
    AFE -- "HTTP + Bearer<br/>axios" --> MW
    SVC --> CTX

    SHARED -.-> CFE
    SHARED -.-> AFE
    SHARED -.-> CTRL
    SHARED -.-> SVC

    classDef client fill:#DBEAFE,stroke:#2563EB,stroke-width:2px,color:#1E3A8A
    classDef api fill:#EDE9FE,stroke:#7C3AED,stroke-width:2px,color:#4C1D95
    classDef data fill:#DCFCE7,stroke:#16A34A,stroke-width:2px,color:#14532D
    classDef shared fill:#FEF3C7,stroke:#D97706,stroke-width:2px,color:#78350F

    class CFE,AFE client
    class MW,CTRL,SVC api
    class CTX,DB data
    class SHARED shared
```

### Project Structure

| Project | Layer | Responsibility |
|---|---|---|
| `Ecommerce_Rookie_NashTech` | **API** | Controllers, JWT & Identity configuration, DI registration, Swagger |
| `Services` | **Business** | Business rules, validation, orchestration, entity ↔ model mapping |
| `Data` | **Data Access** | EF Core entities, `AppDbContext`, migrations, enums, request/result models |
| `ViewModels` | **Contracts** | Response DTOs shared across API and clients |
| `Ecommerce_Rookie_NashTech_CustomerFrontend` | **UI** | MVC storefront with typed API clients and a session cart |
| `Ecommerce_Rookie_NashTech_AdminFrontend/admin_fe` | **UI** | React admin SPA (Redux Toolkit, axios, Tailwind) |

### Request Lifecycle

```mermaid
sequenceDiagram
    autonumber
    participant U as 👤 Client
    participant C as Controller
    participant S as Service
    participant X as AppDbContext
    participant D as SQL Server

    U->>C: HTTP request + Bearer token
    activate C
    C->>C: Authenticate & authorize (JWT)
    C->>S: Invoke with request Model
    activate S
    S->>X: LINQ query / change tracking
    activate X
    X->>D: Parameterised SQL
    D-->>X: Rows
    deactivate X
    X-->>S: Entities
    S->>S: Map to ViewModel
    S-->>C: ResultModel (Succeed, Data, ErrorMessage)
    deactivate S
    alt Succeed
        C-->>U: 200 OK + payload
    else Failed
        C-->>U: 4xx / 5xx + ErrorMessage
    end
    deactivate C
```

---

## 🗃 Database Design

Code-First schema built on ASP.NET Core Identity with `Guid` primary keys throughout.

```mermaid
erDiagram
    USER ||--o{ ORDER : "places"
    USER }o--|| ROLE : "assigned"
    USER ||--o{ USER_ROLE : "has"
    ROLE ||--o{ USER_ROLE : "granted to"
    CATEGORY ||--o{ PRODUCT : "groups"
    ORDER ||--|{ ORDER_DETAIL : "contains"
    PRODUCT ||--o{ ORDER_DETAIL : "ordered as"
    ORDER ||--o| PAYMENT : "settled by"

    USER {
        Guid id PK
        string userName UK
        string email UK
        string passwordHash
        string phoneNumber
        Guid roleId FK
        string fullName
        string image
        string address
        DateTime dob
        EGender gender "Male|Female|Other"
        bool banStatus
    }

    ROLE {
        Guid id PK
        string name "Admin|Customers"
    }

    USER_ROLE {
        Guid userId PK
        Guid roleId PK
    }

    CATEGORY {
        Guid id PK
        string name
        string description
        DateTime createDate
        DateTime updateDate
    }

    PRODUCT {
        Guid id PK
        string name
        Guid categoryId FK
        double price
        string image
        string description
        DateTime createDate
        DateTime updateDate
    }

    ORDER {
        Guid id PK
        Guid customersId FK
        DateTime orderDate
        string receiverFullname
        string receiverPhonenumber
        string receiverAddress
        EOrderStatus status "Pending|Progress|Confirmed|Cancel|Completed"
    }

    ORDER_DETAIL {
        Guid id PK
        Guid orderId FK
        Guid productId FK
        double price
        int quantity
        double totalPrice
    }

    PAYMENT {
        Guid id PK
        Guid orderId FK
        double amount
        EPayType payType "Cash|Transfer"
        DateTime payTime
    }
```

### Order Status Flow

```mermaid
stateDiagram-v2
    direction LR
    [*] --> Pending: customer checks out
    Pending --> Progress: admin picks up
    Progress --> Confirmed: stock reserved
    Confirmed --> Completed: delivered & paid
    Pending --> Cancel: cancelled
    Progress --> Cancel: cancelled
    Completed --> [*]
    Cancel --> [*]
```

---

## 🚀 Getting Started

### Prerequisites

| Requirement | Version |
|---|---|
| [.NET SDK](https://dotnet.microsoft.com/download) | 6.0 or later |
| [SQL Server](https://www.microsoft.com/sql-server) | 2019+ / LocalDB / Express |
| [Node.js](https://nodejs.org/) | 16 or later (admin portal) |
| [EF Core Tools](https://learn.microsoft.com/ef/core/cli/dotnet) | `dotnet tool install --global dotnet-ef` |

### 1 · Clone the repository

```bash
git clone https://github.com/QuanggDat/Ecommerce_Rookie_NashTech.git
cd Ecommerce_Rookie_NashTech
```

### 2 · Configure the API

Create `Ecommerce_Rookie_NashTech/appsettings.json` (or use `dotnet user-secrets`):

```jsonc
{
  "ConnectionStrings": {
    "MyDB": "Server=localhost;Database=Ecommerce_Rookie_NashTech;Trusted_Connection=True;TrustServerCertificate=True"
  },
  "Jwt": {
    "Key": "<a-long-random-secret-of-at-least-32-characters>",
    "Issuer": "https://localhost:7115"
  },
  "Logging": { "LogLevel": { "Default": "Information" } },
  "AllowedHosts": "*"
}
```

> [!IMPORTANT]
> `Jwt:Key` is the HMAC signing secret. Keep it out of source control and use at least 32 characters — a shorter key will cause token generation to fail.

### 3 · Apply the database migrations

```bash
dotnet ef database update --project Data --startup-project Ecommerce_Rookie_NashTech
```

### 4 · Run the Web API

```bash
dotnet run --project Ecommerce_Rookie_NashTech
```

→ Swagger UI: **https://localhost:7115/swagger**

### 5 · Run the customer storefront

```bash
dotnet run --project Ecommerce_Rookie_NashTech_CustomerFrontend
```

→ Storefront: **https://localhost:7230**

### 6 · Run the admin portal

```bash
cd Ecommerce_Rookie_NashTech_AdminFrontend/admin_fe
npm install
npm start
```

Create a `.env` file in `admin_fe` pointing at the API:

```env
REACT_APP_API_URI=https://localhost:7115/api
```

→ Admin portal: **http://localhost:3000**

---

## 🔌 API Reference

Base URL: `https://localhost:7115/api` — every response is wrapped in `ResultModel { Succeed, Data, ErrorMessage }`.

<details open>
<summary><b>👤 User</b> — <code>/api/User</code></summary>

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/Login` | Authenticate and receive a JWT access token |
| `POST` | `/RegisterCustomer` | Register a new customer account |
| `GET` | `/GetAllCustomerslWithSearchAndPaging` | List customers with search & pagination |
| `GET` | `/GetById/{id}` | Retrieve a single user |
| `PUT` | `/Update` | Update a user profile |
| `PUT` | `/BanUser/{id}` | Ban a customer account |
| `PUT` | `/UnBanUser/{id}` | Restore a banned account |

</details>

<details>
<summary><b>🏷️ Category</b> — <code>/api/Category</code></summary>

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/Create` | Create a category |
| `GET` | `/GetAll` | List every category |
| `GET` | `/GetAllWithSearchAndPaging` | List categories with search & pagination |
| `GET` | `/GetById/{id}` | Retrieve a single category |
| `PUT` | `/Update` | Update a category |
| `DELETE` | `/Delete/{id}` | Delete a category |

</details>

<details>
<summary><b>📦 Product</b> — <code>/api/Product</code></summary>

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/Create` | Create a product |
| `GET` | `/GetAllWithSearchAndPaging` | List products with search & pagination |
| `GET` | `/GetByCategoryId` | Filter products by category |
| `GET` | `/GetById/{id}` | Retrieve a single product |
| `PUT` | `/Update` | Update a product |
| `DELETE` | `/Delete/{id}` | Delete a product |

</details>

<details>
<summary><b>🧾 Order &amp; OrderDetail</b> — <code>/api/Order</code>, <code>/api/OrderDetail</code></summary>

| Method | Endpoint | Description |
|---|---|---|
| `POST` | `/Order/Create` | Place a new order |
| `GET` | `/Order/GetAllOrderByCustomerIdWithSearchAndPaging/{userId}` | Paginated order history for a customer |
| `GET` | `/Order/GetById/{id}` | Retrieve a single order |
| `PUT` | `/Order/Update` | Advance an order's status |
| `GET` | `/OrderDetail/GetAllWithSearchAndPaging/{orderId}` | Paginated line items for an order |

</details>

### Authentication

Protected endpoints expect a bearer token issued by `POST /api/User/Login`:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

---

## 🧭 Roadmap

- [ ] Product rating & reviews
- [ ] Online payment gateway integration (currently `Cash` / `Transfer`)
- [ ] IdentityServer4 as a centralised auth provider
- [ ] Unit & integration test coverage
- [ ] Docker Compose for API + SQL Server
- [ ] CI/CD pipeline

---

<div align="center">

Built as part of the **NashTech Rookie Program** · Maintained by [@QuanggDat](https://github.com/QuanggDat)

</div>
