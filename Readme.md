# Currency to Words Translator (English)

## Background

This service assumes that we're currently working with dollars amounts and English language.

Few ideas about extensibility may include:

- Support for another language such as Spanish
- Support for Country name for currency such as Australian dollars
- Support for another currency such as Euros

The implementation is heavily influenced by the idea of thousands in common currency usage (and by extension millions, billions).

Another interesting enhancement would be to support regional dielect such as using the word `1 crore` for 10 millions https://en.wikipedia.org/wiki/Crore


## Show me the app

Please run the solution in Visual Studio. I'm using VS 2022 Community edition.

This solution is built with .NET and React. We need following runtimes installed on our local machine:

- Node 18 (should be okay with higher versions also I think). Download link is here https://nodejs.org/en/blog/release/v18.20.2 . For Windows installation, please also install the C++ libraries when prompted by the installation wizard.
- .NET 8 . Download link is here https://dotnet.microsoft.com/en-us/download/dotnet/8.0

Visual Studio should be able to detect the launch profiles on load.

The UI is hosted at https://localhost:5173/

The API swagger is hosted at https://localhost:7172/swagger/index.html

We want to run Multiple Projects including `Words.Server` and `words.client` on Start. If this is not the case, please use `Configure Startup Projects` option under the profiles options at `Start` dropdown.

Please wait a few seconds on Start until you see the swagger page open up which means backend API is ready and you may start interacting with UI.


## Solution design

I have favored towards creating a general purpose algorithm that works similar to our literal knowledge of counting currencies.

This solution is friendly towards supporting other language translations in future.

The algorithm creates tokens from a given number. Each token contains thousands(or less) part of the number.

Overall, we process the tokens from the leftmost side of the number and it gives us a sense of how big the denominator is.

From complexity perspective, we're not using any nested loops. We process chunks one by one and have a completed result by the end of a single loop.

For example, let's consider the input `212456.34`;

**Step 1:** Get the dollars and the cents parts such as `212456` and `34`

**Step 2:** Get the chunks from `212456` which will be `212` and `456`

**Step 3:** Translate each chunk using a dedicated strategy

**Step 4:** Translate the cents using a dedicated strategy

The strategies are created based on whether we're working with one of these:

- Numbers greater or equal to 1000
- Numbers less or equal to 100
- Cents


## User interface

I have created a super basic React web app to connect with the API and get the transalation for the given number. You can enter a number in input and click `Convert` to see the translation.


## Unit tests

The main business logic is being tested in the `CurrencyServiceTests` class to see whether the translation works correctly for a variety of inputs and use cases.

You may use these inputs for your testing in browser.

## TODO

Please note that there're a number of items I'd improve in case of having an application ready for production deployment.

Some pending tasks include:

- Input sanitisation and handling of bad data including FluentValidation
- Detailed error handling and logging
- Auth setup
- UI polishing
- Integration testing on controller
- Performance testing

For this prototype, please assume it is currently suitable for local run on our own machines. 


## Where to from here

I look forward to hearing some feedback, and of course please feel free to reach out for any questions or change requests.

