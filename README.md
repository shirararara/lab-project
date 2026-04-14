# RefactoringTool - Лабораторна робота №1

## Тема
Дослідження предметної області. Виявлення функціональних вимог до інструменту рефакторингу.

## Варіант №9 - Додавання параметра (Add Parameter) (Литвиненко К.В.)

## Опис методу рефакторингу
Додавання параметра - це метод рефакторингу, при якому до існуючого методу
додається новий параметр, щоб передати йому необхідні дані ззовні.

### Вхідні умови:
- Існуючий метод без потрібного параметра
- Назва нового параметра та його тип

### Вихідні умови:
- Сигнатура методу містить новий параметр
- Всі виклики методу оновлені з новим аргументом

## Архітектура проєкту

### Проєкти:
- Core - бібліотека класів з логікою рефакторингу
- UI - Windows Forms інтерфейс користувача
- Tests - модульні тести (xUnit)

### Основний клас: AddParameterRefactoring

| Метод | Параметри | Повертає | Опис |
|-------|-----------|----------|------|
| MethodExists | sourceCode, methodName | bool | Перевіряє чи існує метод у коді |
| HasParameter | sourceCode, methodName, parameterName | bool | Перевіряє наявність параметра |
| AddParameter | sourceCode, methodName, parameterType, parameterName | string | Додає параметр до методу |
| UpdateMethodCalls | sourceCode, methodName, defaultValue | string | Оновлює виклики методу |
| IsValidParameterName | parameterName | bool | Перевіряє валідність імені параметра |

## TDD - Red стадія

Проєкт знаходиться на першому кроці TDD:
- Прототипи класів створені (без реалізації)
- 10 модульних тестів написані
- Всі тести червоні (NotImplementedException)

## Тести

| № | Назва тесту | Ситуація | Очікуваний результат |
|---|-------------|----------|----------------------|
| 1 | MethodExists_WhenMethodPresent_ReturnsTrue | Метод існує в коді | true |
| 2 | MethodExists_WhenMethodAbsent_ReturnsFalse | Метод відсутній в коді | false |
| 3 | MethodExists_WhenSourceCodeEmpty_ReturnsFalse | Порожній рядок коду | false |
| 4 | AddParameter_MethodSignatureIsCorrect | Додавання параметра до методу | коректна сигнатура |
| 5 | AddParameter_AddsParameterToMethod | Параметр присутній у результаті | рядок містить параметр |
| 6 | AddParameter_MethodAlreadyHasParams_AddsNewParam | Метод вже має параметри | новий параметр додано |
| 7 | UpdateMethodCalls_AddsDefaultArgument | Оновлення викликів методу | виклики містять аргумент |
| 8 | HasParameter_WhenParameterExists_ReturnsTrue | Параметр існує в методі | true |
| 9 | IsValidParameterName_ValidName_ReturnsTrue | Валідне ім'я параметра | true |
| 10 | IsValidParameterName_StartsWithDigit_ReturnsFalse | Ім'я починається з цифри | false |