# Number-To-Words-Converter

This is a web application that can convert numbers to words which represent currency value in dollar and cent. This Repository consists of a Client App which is developed via React Typescript and a Web Service which is developed by Asp.Net 7. You can run react app and the service together to provide number values via React and get it converted to words by the web service solution. 



## Project Structure

There are Two folders in Number-To-Words-Converter which are:

- NumberToWords.Service: This folder contains the solution for the web service

- NumberToWords.UI: This folder contains the React application for the UI



NumberToWords.Service includes 3 different projects as following:

- NumberToWordsConverter.API: This project is the web API which contains the controllers and endpoints for receiving and responding to HTTP requests. This API project has a dependency on NumberToWordsConverter.Core.

- NumberToWordsConverter.Core: This project contains the business logic for converting numbers to words, models, constants. The API project call methods from this layer to convert numbers to words.

- UnitTests: This is our unit test project which test methods that we have in NumberToWordsConverter.Core for converting numbers and validation methods for validating models in the NumberToWordsConverter.API



## How to run

To be able work with the NumberToWords.Service via the React application, you need to run the API and the React app at the same time. 

Run API: To run the API you only need to clone the NumberToWords.Service folder and open the NumberToWordsConverter.sln via and IDE like Visual studio or Visual Studio Code. You do not need to configure any settings and it should be straight forward. Then you can set the NumberToWordsConverter.API as startup project and run the project.

Run React Application: To run the React app, You need to clone the NumberToWords.UI folder and running following commands in the directory:

1. npm install
2. npm run build 
3. npm start


Then it should open a browser with the react app running on it automatically. 


Run Test: To run tests for the NumberToWords.Service, you just need to open the NumberToWordsConverter.sln and Run all tests via Visual Studio.

## Why this approach is the best
There are several reasons why you might want to use ASP.NET Core REST API for server-side and React TypeScript for client-side when developing a web application. Here are some of the benefits:

- Separation of concerns: Separating the server-side and client-side code into separate technologies allows for better separation of concerns. The server-side API can focus on handling the logic and security of the solution, while the client-side React app can focus on providing an interactive user interface.

- Scalability: Using a REST API allows for greater scalability, as multiple clients can connect to the API and consume data simultaneously. You can change the UI from a React application to any other frameworks like Angular or even you can consume the API from a Windows application or a windows services if you need. So instead of changing the whole solution and service every time you require to change UI, you only need to change the UI itself which make the solution easier to maintain in a long run.

- Security: Using a REST API allows for better security, as the API can enforce authentication and authorization rules for clients accessing the data. ASP.NET Core provides built-in support for authentication and authorization, making it easier to secure your API. For now, I used CORS policy in the API to allow all request from any sources get accepted as the current solution is not data sensitive and it is a basic conversion but if we need we can upgrade the security with minimal changes.

- Performance: React TypeScript is known for its performance and efficiency, providing fast rendering and minimizing the number of DOM updates. This, combined with the scalability of the REST API, can provide a fast and efficient user experience.

- Maintainability: Separating the server-side and client-side code into separate technologies makes it easier to maintain the codebase. Changes to one part of the code won't affect the other, allowing you to make changes and updates without worrying about breaking the entire application.

Overall, using ASP.NET Core REST API for server-side and React TypeScript for client-side can provide a scalable, secure, performant, and maintainable solution for developing web applications. 

Business logic is also separated from the API project as there is NumberToWordsConverter.Core besides NumberToWordsConverter.API. There are several reasons why you should separate the business logic from the API project in ASP.NET Core:

- Separation of concerns: Separating the business logic from the API project allows for better separation of concerns. The API project can focus on handling HTTP requests and responses, while the business logic project can focus on implementing the core functionality of the application.

- Testability: Separating the business logic from the API project makes it easier to write automated tests for the business logic. By isolating the business logic in its own project, you can test it independently of the API project and without having to worry about the HTTP layer.

- Reusability: Separating the business logic from the API project makes it easier to reuse the code in other parts of the application. The business logic can be used by other projects, such as a background worker or a scheduled task, without having to duplicate the code.

- Maintainability: Separating the business logic from the API project makes it easier to maintain the codebase. Changes to the business logic won't affect the API project, allowing you to make changes and updates without worrying about breaking the API.

- Scalability: Separating the business logic from the API project allows for better scalability. As the application grows, you can scale the business logic separately from the API project. For example, you can move the business logic to a separate microservice if needed.

Overall, separating the business logic from the API project in ASP.NET Core can provide better separation of concerns, testability, reusability, maintainability, and scalability. It allows you to focus on the core functionality of the application without being tied to the HTTP layer.

## How the converter works
When the converter receives a decimal number, it first validates number to make sure it is greater than zero and it is not bigger than 999999999999999.99. It also checks if the fraction part of the number has only 2 digits. Mentioned validations are used because we try to convert the decimal number to words in dollar and cent currency. 

If the number is valid, it separates the integer part from fraction part of the number and convert them to words separately and add "Dollars" word for the integer part and "Cents" word for the fraction part. The method split each part to 3 digits number and generate words for each 3 numbers. Then it added notations for each 3 digits based on their place. The method uses constant strings for words to convert number to words. All constants can be found in NumberToWordsConverter.Core/Models/Constants.cs

The main advantage of my solution is being more generic compared to other solutions. If you want to convert bigger numbers than trillion like quadrillion, the only change that needs to be done is to add the notations to the NumberWords constant array and change the validation condition. It could be done simpler, but it would not be as generic as it is now.

