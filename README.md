## ⚙️ Core Functionality
A full-stack project that works with World of Tanks data and displays it through a web interface. The application collects data from an external API and XML files, then converts it into a consistent and usable format.

The backend processes and normalizes the data, handling differences between sources such as missing fields or different structures. The data is mapped into C# models and stored in MongoDB, allowing efficient querying.

The system also creates folders for each tank and stores related file paths, connecting the database with the file system.

### 🔄 Data Flow

```
World of Tanks API + XML
↓
Data Parsing
↓
Data Transformation (DTO)
↓
MongoDB Storage
↓
REST API
↓
Blazor Frontend (UI)
```

On the frontend, the data is displayed in a way similar to how the game presents it. Tank stats such as gun, engine, and armor are grouped and shown in a structured format, making them easier to understand and compare. The UI allows users to browse tanks, search, and open a detailed view for each vehicle.

The data is prepared in a way that allows further configuration, so the system can be extended without major changes.

---

<img width="1462" height="868" alt="Képernyőkép 2026-04-10 191814" src="https://github.com/user-attachments/assets/6807bdad-efb6-4e0a-9753-264645085623" />

## 🚀 Functionalities

* Advanced filtering (tier, nation, class)
* Crew skill calculations
* Real-time stat adjustments
