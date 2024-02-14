# Payments-ChallengeGeopagos2024

## Uso del proyecto
El proyecto corre en una imagen de docker. La misma se puede descargar del siguiente link: https://hub.docker.com/layers/ezemilone/paymentschallengegeopagos2024/latest/images/sha256:304e4a29599459ddd0a6d372ba8973e53d4c58f7e70d674359a96aad1ca137fe?uuid=3BAEA4D3-06D1-4231-873B-8A8C330821A0
Una vez descargada puede correrse con docker run, una vez hecho esto se abrirá un Swaagger en el cual pueden realizarse las pruebas de los endpoints.

### Para realizar las requests se aclaran algunos valores por defecto que se definieron con fines practicos:
Enumeraciones

1. AuthorizationType

Esta enumeración define los diferentes tipos de autorización de pago:

    Charge (1): Cargo realizado a un cliente.
    Refund (2): Reembolso a un cliente.
    Reversal (3): Anulación de un cargo previamente realizado.

2. ClientType

Esta enumeración define los diferentes tipos de cliente en relación a la autorización de pago:

    SimpleAuthorization (1): Cliente que solo requiere autorización simple.
    DoubleFactorAuthorization (2): Cliente que requiere autorización de doble factor (necesario confirmacion del pago).

3. AuthorizationState

Esta enumeración define los diferentes estados del proceso de autorización de pago:

    PendingAuthorization (1): La autorización está pendiente de la validación inicial.
    PendingConfirmation (2): La autorización está pendiente de confirmación manual.
    Authorized (3): La autorización ha sido aprobada.
    Denied (4): La autorización ha sido denegada.
    Expired (5): La solicitud de autorización ha expirado pasados los 5 minutos.

Además hay 4 clientes disponibles para realizar las pruebas:
1) ID: 1, Nombre: John Doe, Tipo: SimpleAuthorization (Valor: 1)
2) ID: 2, Nombre: David Johnson, Tipo: DoubleFactorAuthorization (Valor: 2)
3) ID: 3, Nombre: John Wick, Tipo: SimpleAuthorization (Valor: 1)
4) ID: 4, Nombre: Pete Mitchell, Tipo: DoubleFactorAuthorization (Valor: 2)

## Requerimientos del proyecto
Estos requerimientos fueron estraidos del documento compartido por la organización Geopagos:

Challenge Payments - Geopagos - 2024.

Se requiere construir una solución que dé soporte a la autorización de pagos para nuestros clientes.

La solución constará de una API pública que será consumida por nuestros clientes donde recibiremos la solicitud de autorización y responderemos de acuerdo a lo que nos responda el procesador del pago.

El procesador de pago será un servicio independiente a la solución que simplemente nos responderá como aprobada la autorización cuando el monto sea un número entero y denegado si el monto contiene decimales.

Las autorizaciones pueden ser de cobro, devolución o reversa.

De acuerdo al tipo de cliente, se puede tener distintos modelos de autorización.
- Primero: Solicitud de autorización
- Segundo: Solicitud de autorización más su posterior confirmación.

Para aquellos clientes que cuenten con el segundo modelo se debe realizar una acción que genere una autorización de reversa de la solicitud generada en el paso anterior si la misma no fue confirmada dentro de los primeros cinco minutos.

Se deberá llevar un registro de todas las solicitudes de autorizaciones con su respectivo estado e información asociada.

Adicionalmente, debemos llevar otro registro que solo incluya las autorizaciones aprobadas (necesario para alimentar un proceso interno de reportería), este registro podrá ser solo una tabla que indique fecha, monto y cliente. Este registro deberá ser completado de manera asíncrona para no interferir en el proceso de autorización.

A fines prácticos podemos asumir que los clientes ya se encuentran registrados y ya se encuentra resuelta la autenticación.

Aclaraciones:
La solución debe poder ejecutarse localmente en un ambiente sin conexión a internet, se sugiere el uso de docker. De ser necesario dar indicaciones para su instalación y uso, incluirlas en un archivo readme.

Se valoran buenas prácticas, código limpio.
