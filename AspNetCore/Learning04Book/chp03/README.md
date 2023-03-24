# Introduction 
This is chapter 03. We will create the first application in ASP.NET Core.

## The Scene

- A home page that shows information about the party
- A form that can be used to RSVP
- Validation for the RSVP form, which will display a thank-you page
- A summary page that shows who is coming to the party

## dotnet commands

- **Creating a new project**

```
dotnet new globaljson --sdk-version 6.0.403 --output PartyInvites
dotnet new mvc --no-https --output PartyInvites --framework net6.0
dotnet new sln -o PartyInvites
dotnet sln PartyInvites add PartyInvites
```
