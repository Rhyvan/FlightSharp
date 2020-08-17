# FlightSharp - ASP.NET Core MVC app with Web API
*Target framework: .Net Core 3.1*

#### What it does:
The app fetches flight data from a remote API hosted by RapidAPI, and Users can browse them via a UI, place orders into a cart.

#### Goal:
The project is a MVC practice, using plain JavaScript for AJAX calls on top of View renders.
The main aspect was to create a well **decoupled, easily testable app**, and get to know **handling of HTTP requests and session** in ASP.NET Core.

App in current state is overheaded, classes and functions are built by taking into account the future features. 
(E.g. No InMemory-repository implemented, thus session holds data.)


---
### Implemented features:
- Search and filter flight data
- Add selected flights to cart
- Modify/delete items in cart
- Tests for web API and underlying business logic

#### Future implementations
- database and repositories.
- user registration/login and authentication

#### API:
*Home/api/search*
: based on request queries fetches the data and returns as list

*Home/api*
: loads cart's page

*Home/api/cart* [POST/DELETE]
: adds or deletes the specified item sent as JSON data and sets new session.

#### NuGet Packages:
| Package name | version |
|---|---|
| NSubstitute | {4.2.2} |
| Microsoft.NET.Test.Sdk |{16.4.0} |                             
| Microsoft.AspNetCore.Session | {2.2.0}|
