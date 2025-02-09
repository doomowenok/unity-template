# Empire

## Установка/Обновление внешних зависимостей

1. Убедитесь, что у вас установлен [.NET SDK](https://dotnet.microsoft.com/download).
2. Откройте Dependency Solution.
3. Восстановите зависимости:
   ```bash
   dotnet restore
   ```
4. Установите нужные используя Nuget GUI или командную строку:
    ```bash
    dotnet add package <имя пакета>
    ```
5. Соберите проект под релизной сборкой:
    ```bash
    dotnet build --configuration Release
    ```
6. Скопируйте полученные .dll, которые вам нужны и перенесите в проект игры.
    ```
    Game/Assets/ExternalDependency
    ```