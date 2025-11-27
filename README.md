# 🦾 HeyDEAN - Digital Execution, Assisting Nicely

DEAN er en ```personlig assistent``` bygget med **React + TypeScript** på frontend og **ASP.NET Core Web API** på backend.  
Assistenten bruger **speech recognition**, **JWT-auth**, og kan vise **Notes, Tasks og Events**, genereret ud fra dine voice-commands.

---

## 🚀 Features

### 🗣️ Voice Interaction
- Brug browser mikrofonen til at tale med DEAN. (Kun Google Chrome pt.)
- Automatisk transskription via browserens `SpeechRecognition` API.
- DEAN tager prompts og svarer med:
  - Tekstbeskeder eller lister (Notes, Tasks, Events)

### 💬 Chat med DEAN
- Bruger og DEAN har en samtale i form a chatbubbles.

### 📝 Data Håndtering
DEAN henter data via API'et, gennem ```intentions```.
Disse er baseret på hvad brugeren gerne vil gennem CRUD operations vha. DEAN:
- ```Get my notes.```                -> henter **Notes** liste  
- ```What are my tasks?```           -> henter **Tasks** liste 
- ```Do I have any events today? ``` -> henter **Events** kalender ellers liste

### 🔐 Authentication
- Login med brugernavn + password (Unik GUIDs & Hashed passwords)
- JWT tokens (access + refresh)


### 🧩 Komponentbaseret struktur
- `VoiceRecButton` – håndterer al speech recognition
- `MultiPanel` – universal liste-komponent til Notes/Tasks/Events
- `DeanPage` – hovedassistenten
- `AuthContext` – tokenstyring

---

## 📂 Projektstruktur

```bash
HeyDEAN/
│
├── 🗂️ HeyDEAN_API/                        # API hovedmappe          
│   ├── 📁 Controllers/                    # API controllers
│   ├── 📁 Data/                           # DbContext og Seeders
│   ├── 📁 DTOs/                           # Data Transferable Objects
│   ├── 📁 Extensions/                     # Mapping til DTOs
│   ├── 📁 Models/                         # Data modeller
│   ├── 📁 Repositories/                   # Repository pattern filer
│   ├── 📁 Services/                       # Service pattern filer
│   └── ⚙️ Program.cs                      # Where the magic is built
│
├── 🗂️ HeyDEAN_Frontend/                   # Frontend hovedmappe
│   ├── 📁 src/                            # Source med alle filer/mapper
│   │   ├── 📁 components/                 # Mappe til forskellige komponenter
│   │   │   ├── VoiceRecButton.tsx
│   │   │   ├── Panel.tsx
│   │   │   └── ChatBubble.tsx
│   │   ├── 📁 pages/                      # Mappe til forskellige sider
│   │   │   ├── LoginPage.tsx
│   │   │   └── DeanPage.tsx
│   │   ├── 📁 context/                    # Mappe til context mellem Auth og API
│   │   │   └── AuthContext.tsx
│   │   ├── 📁 lib/                        # Indeholder JWT authentication filer
│   │   │   ├── api.ts
│   │   │   └── auth.ts
│   │   └── 📜 main.tsx                    # Where the magic empowered
│   └── 🖼️ index.html                      # Where the magic is shown

```

---

## 🛠️ Tech Stack
### 🖼️ Frontend
* React
* TypeScript
* Vite
* TailwindCSS
* react-speech-recognition (WebSpeech API integration)

### 🧰 Backend
* ASP.NET Core 9
* Entity Framework Core
* JWT Auth
* MySQL

For at installere og køre systemet, er der disse krav:
* MySQL workbench/server (Systemet er opsat til at køre MySQL, men kan ændres til andre SQL udbydere)
<br>Denne skal helst køre før man tænder for API.

---

## 🔨 Installation
For at køre programmet via ```localhost```, skal man køre API, database og frontend hver for sig.
### 🔗 API
Efter download, gå til mappen ```HeyDEAN_API```, derefter kør disse kommandoer:
```bash
cd HeyDEAN_API
dotnet restore
dotnet ef migrations add initialCreate
dotnet ef database update
dotnet run
```
Nu er der en kørende API med tilgang til database, som kører på:
```bash
http://localhost:5152
```
For at prøve eller teste, gøres det via Swagger UI:
```bash
http://localhost:5152/swagger
```

### 🖌️ Frontend
Gå til mappen ```HeyDEAN_Frontend```, og kør disse kommandoer:
```bash
npm install
npm run dev
```
Nu kører frontend på:
```
http://localhost:5173
```

---

## 📌 API Endpoints
### 🧑‍💻 Authentication
| Method | Endpoint                  | Description                                    |
| ------ | ------------------------- | ---------------------------------------------- |
| `POST` | `/api/auth/register`      | Register a new user                            |
| `POST` | `/api/auth/login`         | Login and receive JWT + Refresh token          |
| `POST` | `/api/auth/refresh-token` | Refresh access + refresh token                 |
| `GET`  | `/api/auth`               | Test protected route (requires authentication) |
| `GET`  | `/api/auth/admin-only`    | Admin-only protected route                     |

### 🤖 DEAN Assistance
| Method | Endpoint        | Description                                  |
| ------ | --------------- | -------------------------------------------- |
| `POST` | `/api/dean/ask` | Send a prompt to DEAN and receive a response |

### 📝 Notes
| Method   | Endpoint          | Description                          |
| -------- | ----------------- | ------------------------------------ |
| `GET`    | `/api/notes`      | Get all notes                        |
| `GET`    | `/api/notes/{id}` | Get a single note                    |
| `POST`   | `/api/notes`      | Create a note                        |
| `PUT`    | `/api/notes/{id}` | Update a note                        |
| `DELETE` | `/api/notes/{id}` | Delete a note                        |

### ✅ Tasks
| Method   | Endpoint                   | Description              |
| -------- | -------------------------- | ------------------------ |
| `GET`    | `/api/tasks`               | Get all tasks            |
| `GET`    | `/api/tasks/{id}`          | Get a single task        |
| `POST`   | `/api/tasks`               | Create a task            |
| `PATCH`  | `/api/tasks/{id}/complete` | Mark a task as completed |
| `DELETE` | `/api/tasks/{id}`          | Delete a task            |

### 📅 Events
| Method   | Endpoint           | Description        |
| -------- | ------------------ | ------------------ |
| `GET`    | `/api/events`      | Get all events     |
| `GET`    | `/api/events/{id}` | Get a single event |
| `POST`   | `/api/events`      | Create an event    |
| `DELETE` | `/api/events/{id}` | Delete an event    |

---

## 💡 Vision
DEAN skal på sigt kunne:
* Forstå kontekst gennem tale
* Udføre handlinger (CRUD operations)
* Integrere kalender styring
* Kører i flere formater end browser (Browser, App, Widget, Desktop?)

---

## ⏳ Yderligere udvikling

* Brugerstyret data (kun brugerens egen data bliver vist)
* Dockerization til nemmere kørsel
* AI integration til bedre assistance (Ollama, GTP, etc)
* Mulighed for Appudvikling (Native)
* Andre muligheder for Voice recognitions (Er der andet end ```react-speech-recognition```?)
* Custom persona og stemme til DEAN
