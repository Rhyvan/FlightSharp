# FlightSharp - ASP.NET Core MVC app with Web API
*Target framework: .Net Core 3.1*

#### What it does:
The app fetches flight data from a remote API hosted by RapidAPI, and Users can browse them via a UI, place orders into a cart.

#### Goal:
The project is a MVC practice, using plain JavaScript for AJAX calls on top of View renders.
It began as a school group task, then I forked it to conduct complete refactoring on the backend and to add missing features.
My main aspect was to create a well **decoupled, easily testable app**, and get to know **handling of HTTP requests and session** in ASP.NET Core.

Drawbacks: No EF Core InMemory-repository implemented, thus session keeps data.)


---

### Implemented features:
- Search and filter flight data fetched from https://travelpayouts.github.io/slate/#flight-data-access-api-v1
- Add selected flights to cart and notify user of its success/failure by a toast
- Modify/delete items in cart 
- Tests for web API and underlying business logic, handle possible errors.


#### NuGet Packages:
| Package name | version |
|---|---|
| NSubstitute | {4.2.2} |
| Microsoft.NET.Test.Sdk |{16.4.0} |                             
| Microsoft.AspNetCore.Session | {2.2.0}|
