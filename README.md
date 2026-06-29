# 🔳 ZxingBarcode

![.NET](https://img.shields.io/badge/.NET-10.0--windows-512BD4?style=flat-square&logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?style=flat-square&logo=csharp&logoColor=white)
![ZXing.Net](https://img.shields.io/badge/ZXing.Net-0.16-1f6feb?style=flat-square)
![Blog](https://img.shields.io/badge/blog-rsrsystem-FF5722?style=flat-square&logo=blogger&logoColor=white)

> Librería **.NET 10** para **crear y leer códigos QR y EAN-13** desde PowerBuilder, con [ZXing.Net](https://github.com/micjahn/ZXing.Net).

## 📋 ¿Qué es esto?

El motor de códigos de barras que uso en varios ejemplos de PowerBuilder. Genera y decodifica
**QR** y **EAN-13** (y otros formatos de ZXing) con muy poco código, y se consume desde PB como un
`dotnetobject`.

## 🔤 Cambio de nombre al migrar a .NET 10

La clase pública pasó de **`ZxingNet8` → `ZxingNet`** (el "8" sugería ".NET 8" y, ya en .NET 10,
despistaba). En consecuencia, el objeto proxy de PowerBuilder pasó de **`nvo_zxingnet8` → `nvo_zxingnet`**
en los ejemplos que la consumen (`qrcode`, `ean13code`, `TwoFactorAuthDemo`).

> ⚠️ Si actualizas: **recompila `ZxingBarcode` y vuelve a desplegar la DLL** en la carpeta `DotNet` de
> cada proyecto PB. El `is_classname` del proxy ya apunta a `ZxingBarcode.ZxingNet`.

## 🧩 Dependencias

| Paquete | Versión |
|---------|---------|
| [ZXing.Net](https://www.nuget.org/packages/ZXing.Net) | `0.16.11` |
| [ZXing.Net.Bindings.Windows.Compatibility](https://www.nuget.org/packages/ZXing.Net.Bindings.Windows.Compatibility) | `0.16.14` |

## 🛠️ Requisitos

- **.NET SDK 10.0** o superior
- **Windows** (`net10.0-windows`, usa System.Drawing para las imágenes)

## 🚀 Compilar

```bat
dotnet build ZxingBarcode.csproj -c Release
```

La DLL queda en `bin\Release\net10.0-windows\`.

## 🔗 Proyectos PowerBuilder relacionados

- **QR** 👉 https://github.com/rasanfe/qrcode
- **EAN-13** 👉 https://github.com/rasanfe/ean13code

---

📨 **Blog:** <https://rsrsystem.blogspot.com/>

> ¡Nos vemos en el próximo artículo! Y recuerda: en PowerBuilder, los límites solo están en nuestra imaginación. 🚀
