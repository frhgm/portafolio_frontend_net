# portafolio_frontend_net
Nuestro proyecto de WPF para consumir API [AutoREST](https://g15f555dd431949-maipograndebdd.adb.sa-santiago-1.oraclecloudapps.com/ords/Portafolio/sign-in/?r=_sdw%2F%3Fnav%3Drest-workshop%26rest-workshop%3Dauto-rest%26auto-rest%3Dopenapi%26openapi%3D%257B%2522object_id%2522%253A10429%257D) de Oracle Autonomous Database realizada para este proyecto

## Estructura (inicial) de proyecto
### app
**Este proyecto contiene:**
- Todas las ventanas
- Cualquier utilidad adicional, relacionada directamente al WPF

### classLibrary (no se me ocurrio un mejor nombre)
**Este proyecto tiene:**
- Todos los Data Services
- Todos los DTOs

### components
Libreria de componentes, que pueden ser reutilizados (aun por trabajarl en ellos)

### pruebas (bastante explicativo)
Proyecto de pruebas unitarias, realizado con [NUnit](https://nunit.org/)
