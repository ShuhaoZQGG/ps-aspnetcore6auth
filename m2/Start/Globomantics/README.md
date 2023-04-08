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

# Identity Cookie
Identity cookie is used to know which identity came from which user
Once sign in, application puts all the user information in the cookie, including claims
Then encrypted using the key only the application/server has, it can only be decrypted using the same key
Application/Server sends the cookie to the Browser

## When the Browser receives an identity cookie:
The browser can only store the cookie, it doesn't have access to the key
It has no way to read the user data in the cookie
When a new request happened in the domain, all cookies in the domain including the claims are sent to the application/server

## When the Application receives an identity cookie:
Identity cookie is used to identify the user
- The user information is decrypted
- The ClaimsPrincipal object is reconstructed
- ClaimsPrincipal is made available
- Secured by ASP.NET Core Data Protection (a .net mechanism)

## ClaimsPrincipal and Claims
- Use Claims property to access all claims
- Could be used to do Authorization
- But use centralized authorization instead

# Problem with Identity Cookies:
- Persistent cookies lifetime
- User has access to application as long as cookie lives
Solution: handle event that fires upon each incoming request

# More about how Cookie wokrs
Browser save all cookies of all sites, but only the request made to a specific domain will receive the cookies stored in that specific domain(site)
issue: Cross-Site Request Forgery (CSRF)
Browser(example.com) => Server(attacker.com) => Server(example.com)
Cookie is sent along the sites. So attacker might be able to sign in using your cookie!

Solution: ***SameSite Cookies Options***
- Strict: Cookie will never be sent cross-site
- Lax (Default): Introduce one exception to strict rule. Only hyperlink in the html of other site will work (can be attacked in the example)
- None: Disable samesite cookies

# External Identity Providers
Example:
- Google
- Facebook
- Microsoft
- Twitter
...

Browser => Application Server = Client_id Client_secret (managed by google) => Google Server

if success: 
Google Server <= OpenID Connect (OAuth) => Application Server
Google = Cookie => Browser
Browser <= Cookie => Application Server

After application server and browser shared the cookie, we don't need to login through google again

This is called Claim Transfer

# Secrets
- Not in source code
- Use configuration object
- Not in JSON files
- Secret manager

# Scheme Actions
- Authenticate (How ClaimPrincipal gets reconstructed on every request)
- Challenge (What happens if a user tries to access a resource when an authentication is required)
- Forbid (What happens if a user tries to access a resource where they don't have sufficient right)

### SignInAysnc vs AuthenticateAsync in HttpContext
- SignInAysnc: Returns the ClaimsPrincipal and persists it
- AuthenticateAsync: Returns the ClaimPrincipal without persisting