# Authentication
Authentication is verifying an identity

# Authorization
Authorization is giving limited access to users and define what users can do (and can not do)

# Authentication and Authorization in ASP.NET Core
## Authentication (AuthN)
- Identity Cookies
- ASP.NET Core Identity
- Identity Provider

## Authorization (AuthZ)
- ASP.NET Core Authorization

## Authentication Scheme
Cookie is one way of authentication scheme
An Authentication Scheme is a way to authenticate

Example:
- Passport for airport
- Membership card for gym

# Claims-based Identity
In ASP.NET Core, the object that represents the user called the ***ClaimPrincipal***

A ClaimsPrincipal contains ClaimsIdentity objects, for each authenticated scheme.
If use only one scheme, then a ClaimPrincipal has only one ClaimsIdentity.

Each ClaimsIdentity object contains Claims.

All the Claims, from all ClaimsIdentity combined, are made available as Property in ClaimsPrincipal.

