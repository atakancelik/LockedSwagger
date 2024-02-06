# Swagger Basic Authentication Middleware

## Türkçe

Bu proje, ASP.NET Core uygulamalarında Swagger arayüzüne basit bir kimlik doğrulama eklemek için bir ara yazılım sağlar.

### Kullanım

1. Middleware'i projeye eklemek için `Startup.cs` dosyasındaki `Configure` metoduna aşağıdaki satırı ekleyin:

```csharp
app.UseMiddleware<SwaggerBasicAuthMiddleware>();
```

2. appsettings.json dosyasına kimlik doğrulama bilgilerini ekleyin:
```json
{
  "Authentication": {
    "Users": [
      {
        "Username": "kullanici1",
        "Password": "sifre1"
      },
      {
        "Username": "kullanici2",
        "Password": "sifre2"
      }
    ]
  }
}
```
Swagger arayüzüne erişmek için tarayıcınızda http://localhost:<port>/swagger adresine gidin. Eğer doğrulama başarısız olursa, 401 Unauthorized hatası alacaksınız ve kimlik doğrulama bilgilerini girmeniz istenecektir.



## English

This project provides a middleware for ASP.NET Core applications to add basic authentication to the Swagger interface.

### Usage

1. Add the middleware to your project by adding the following line in the `Configure` method in `Startup.cs` file:

```csharp
app.UseMiddleware<SwaggerBasicAuthMiddleware>();
```
2. Add authentication credentials to the appsettings.json file:
```json
{
  "Authentication": {
    "Users": [
      {
        "Username": "user1",
        "Password": "password1"
      },
      {
        "Username": "user2",
        "Password": "password2"
      }
    ]
  }
}
```
Navigate to http://localhost:<port>/swagger in your browser to access the Swagger interface. If authentication fails, you will receive a 401 Unauthorized error and will be prompted to enter the authentication credentials.
