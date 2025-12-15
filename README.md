# Projekt Zaliczeniowy - ASP.NET Core MVC

**Ostateczna, kompletna i przetestowana wersja projektu (zawierająca wszystkie wymagania podstawowe oraz punkty dodatkowe) znajduje się na gałęzi (branch):**

##  `FinalCommitGotowyProjektZaliczenie` 

---

##  Opis Projektu
Aplikacja webowa stworzona w technologii ASP.NET Core MVC służąca do zarządzania bazą książek oraz wydawnictw. Projekt realizuje wszystkie wymagania stawiane w ramach zaliczenia laboratoriów, włącznie z zadaniami dodatkowymi.

### Zaimplementowane funkcjonalności:

1.  **Podstawowe (Core):**
    *   **Trwałość danych:** Baza danych **SQLite** (Entity Framework Core).
    *   **CRUD Książki:** Pełna obsługa (Lista, Szczegóły, Dodawanie, Edycja, Usuwanie).
    *   **Relacje:** Relacja jeden-do-wielu między Książką a Wydawcą (Book -> Publisher).
    *   **CRUD Wydawcy:** Osobny kontroler do zarządzania słownikiem wydawców.
    *   **Stronicowanie:** Lista książek obsługuje paginację (mechanizm `PagingListAsync`).

2.  **Dodatkowe:**
    *   **Moduł Identity:** Wdrożony system rejestracji i logowania użytkowników. Dostęp do edycji danych jest zabezpieczony atrybutem `[Authorize]`.
    *   **WebAPI + JavaScript:** Formularz dodawania książki wykorzystuje **Fetch API** do dynamicznego wyszukiwania wydawców w bazie (bez przeładowania strony), korzystając z dedykowanego kontrolera API (`ApiPublishersController`).

---

##  Jak uruchomić projekt?

1.  Sklonuj repozytorium.
2.  Przełącz się na odpowiednią gałąź:
    ```bash
    git checkout FinalCommitGotowyProjektZaliczenie
    ```
3.  Otwórz terminal w folderze projektu i wykonaj aktualizację bazy danych (migracje są w kodzie):
    ```bash
    dotnet ef database update
    ```
4.  Uruchom aplikację:
    ```bash
    dotnet run
    ```

---

##  Technologie
*   .NET 8 / 9
*   ASP.NET Core MVC
*   Entity Framework Core (SQLite)
*   ASP.NET Core Identity
*   JavaScript (Fetch API)
*   Bootstrap 5

---
**Autor:** Piotr Bacior 15 722 WSEI Kraków
