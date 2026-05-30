# FoodieSpot - Food and Drink Tracking App

[![Review Assignment Due Date](https://classroom.github.com/assets/deadline-readme-button-22041afd0340ce965d47ae6ef1cefeee28c7c493a6346c4f15d667ab976d596c.svg)](https://classroom.github.com/a/uM_GSLJS)

## Project Overview

FoodieSpot is a cross-platform mobile application developed for the Mobile Computing module (6G6Z0014). The app helps users track their meals, record nutrition information, capture food photos, log meal locations, and utilise device hardware features such as camera, GPS, text-to-speech, and haptic feedback.

- **Theme:** Food and Drink
- **Framework:** .NET MAUI (.NET 8.0)
- **Target Platforms:** Android (phone + tablet), Windows
- **Architecture:** MVVM (Model-View-ViewModel)

---

## Features

### Core Functionality (CRUD)

| Feature | Description |
| :--- | :--- |
| Add Food Item | Users can add new food/drink records with name, category, description, calories, macronutrients, and allergy notes. |
| View Details | Tap the Details button to see full nutritional information. |
| Delete Food Item | Swipe left on any food card to delete with confirmation dialog. |
| Search | Real-time search by name, category, description, or tags. |

### Hardware Integration (4 features)

| Hardware | API Used | Use Case |
| :--- | :--- | :--- |
| Camera | MediaPicker | Take photos of meals to visually document food. |
| Location and Geocoding | Geolocation, Geocoding | Get current GPS coordinates and convert to readable address (country/city). |
| Text-to-Speech | TextToSpeech | Read nutrition summaries aloud for accessibility. |
| Vibration and Haptic Feedback | Vibration, HapticFeedback | Provide tactile confirmation for user actions. |

### Accessibility

- Dark Mode support with automatic theme switching.
- Dynamic Font Scaling (Large Text Mode toggle in Settings).
- Screen Reader Support using SemanticProperties and SemanticScreenReader.

### UI/UX Design

- Consistent green theme (sage green 436644, warm peach E8A87C).
- Rounded cards, subtle shadows, responsive layout.
- Bottom TabBar navigation (Foods / Hardware / Settings).

---

## Technical Implementation

### Project Structure

```
FoodieSpot/
├── Models/ (FoodItem.cs)
├── Views/ (XAML pages)
│   ├── MainPage.xaml
│   ├── AddItemPage.xaml
│   ├── FoodDetailPage.xaml
│   ├── HardwarePage.xaml
│   └── SettingsPage.xaml
├── ViewModels/
├── Services/
│   ├── DatabaseService.cs
│   ├── SpeechService.cs
│   └── AccessibilityService.cs
└── Resources/
```

### Database (SQLite)

- **Package:** sqlite-net-pcl
- **Table:** FoodItems with columns: Id, Name, Category, Description, Calories, Protein, Carbs, Fat, AllergyNote, Tags, ImagePath, IsFavourite
- **Seeding:** 4 initial food items are inserted when the database is first created.

### Key APIs and Libraries

| Purpose | Library / API |
| :--- | :--- |
| UI | XAML + .NET MAUI Controls |
| Navigation | Shell + TabBar |
| Data Persistence | SQLite (sqlite-net-pcl) |
| Camera | MediaPicker |
| Location | Geolocation, Geocoding |
| Text-to-Speech | TextToSpeech |
| Vibration | Vibration, HapticFeedback |
| Accessibility | SemanticProperties, SemanticScreenReader |

---

## How to Run the Project

### Prerequisites

- Visual Studio 2022 with .NET MAUI workload installed
- Android SDK (via Visual Studio Installer)
- Android emulator (e.g., Pixel 7 API 34) or physical Android device

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/HubuManchester/fooddrink-chengzi-77.git
   ```

2. Open FoodDrinkApp.sln in Visual Studio.

3. Select an Android emulator from the run target dropdown.

4. Press F5 to build and run the app.

**Note:** The app uses SQLite locally. No external API key is required.

---

## Deployment

- **Primary Device:** Android phone emulator (Pixel 7 - API 34)
- **Secondary Device:** Android tablet emulator (Pixel Tablet - API 35) - demonstrates cross-platform capability
- The app can also run on Windows by selecting Windows Machine as the target.

---

## Assessment Criteria Mapping

| Criterion | Weight | How It Is Addressed |
| :--- | :--- | :--- |
| UI/UX Design and Accessibility | 30% | Consistent green theme, dark mode, dynamic font scaling, screen reader support |
| Use of Mobile Hardware | 20% | Camera, GPS, Text-to-Speech, Vibration and Haptic Feedback |
| Functionality | 20% | CRUD operations, swipe-to-delete, search, insights |
| Validation and Error Handling | 10% | Input validation, try-catch blocks, user-friendly error messages |
| Code Quality | 10% | MVVM pattern, naming conventions, comments, DRY principle |
| Deployment | 5% | Runs on Android phone + Android tablet emulator |
| GitHub Usage | 5% | Regular commits with meaningful messages, detailed README |

---

## Future Improvements

- Add favourite/star functionality for saved meals.
- Implement barcode scanning to automatically fetch nutritional data.
- Add cloud backup (Firebase/Azure) to sync data across devices.
- Integrate nutritional charts with more detailed analytics.

---

## Author

Rong Xiao  
Mobile Computing Assignment - 6G6Z0014  
Manchester Metropolitan University

---

## References

- .NET MAUI Documentation
- SQLite-net Documentation
- WCAG Accessibility Guidelines
- Assignment brief: 6G6Z0014_1CWK100.pdf

Last updated: May 2026