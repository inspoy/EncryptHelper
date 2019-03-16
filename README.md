# Encrypt Helper

Encrypt algorithm utilities

Each algorithm one file, simply copy the srouce file into your project.

## Rc4

File: `Rc4.cs`

Usage: see `TestConsole/TestRc4.cs`

```csharp
var rc4 = new Rc4();
rc4.SetKeyAndInit("secret_key");
var encrypted = rc4.Encrypt(Encoding.UTF8.GetBytes("Raw texts..."));
rc4.SetKeyAndInit("secret_key"); // Reset
var decrypted = rc4.Encrypt(encrypted);
// decrypted = "Raw texts..."
```

## Aes

> TODO...