# ShopList

Aplicación .NET MAUI para administrar listas de compras con soporte para Android y Windows.

## Características
- Gestión de productos con categorías y seguimiento de estado de compra.
- CRUD de categorías con validaciones y protección ante eliminaciones con productos asociados.
- Plantillas reutilizables guardadas en SQLite y exportadas automáticamente a `templates.json`.
- Historial de compras con fecha y detalle de productos adquiridos.
- Configuración de orden, tema, tamaño de fuente y confirmaciones utilizando `Preferences`.
- Estadísticas de productos comprados por categoría.
- Pantalla "Acerca de" con créditos y versión.

## Tecnologías
- .NET MAUI (net8.0-android / net8.0-windows10.0.19041.0)
- CommunityToolkit.Mvvm
- sqlite-net-pcl
- SQLite Async para persistencia.

## Estructura principal
- `Models`: Entidades persistidas y auxiliares.
- `Data`: Inicialización y acceso a la base de datos.
- `Repositories`: Abstracciones y lógica de acceso a datos.
- `Services`: Preferencias y ajustes de la aplicación.
- `ViewModels`: Lógica de presentación siguiendo MVVM.
- `Views`: Páginas XAML enlazadas a sus ViewModels.

## Configuración inicial
Al ejecutar la aplicación por primera vez se crean las tablas necesarias y se insertan categorías por defecto (Alimentos, Limpieza y Otros). Las plantillas se sincronizan con el archivo `templates.json` en el directorio de datos de la app.

## Uso
1. Desde la pantalla principal agrega productos y márcalos como comprados con el checkbox ✔️.
2. Filtra por categoría y navega a categorías, plantillas, historial, configuración o estadísticas con los accesos directos.
3. Usa "Finalizar lista" para mover los productos comprados al historial.
4. Crea plantillas desde la lista actual y aplícalas cuando necesites recuperar una lista frecuente.
5. Ajusta tema, orden y tamaño de fuente en Configuración.

## Requisitos
- .NET 8 SDK con workloads de MAUI instalados.
- Plataforma de destino Android o Windows 10 build 19041 o superior.

## Ejecución
```bash
dotnet build
```

Para ejecutar en Android o Windows utiliza los targets MAUI disponibles en tu entorno (`dotnet build -t:Run -f net8.0-android`).
